using Microsoft.EntityFrameworkCore;
using secre_chat_api.chat.Domain.DTOs;
using secre_chat_api.chat.Domain.DTOS;
using secre_chat_api.chat.Domain.Entities;
using secre_chat_api.chat.Infstructure.Repository.ChatRepository.ISecretChatRepositries;

namespace secre_chat_api.chat.Infrastructure.Persistence.Repositories;

public class ChannelRepository : IChannelRepository
{
    private readonly ChatDbContext _context;

    public ChannelRepository(ChatDbContext context)
    {
        _context = context;
    }

    public async Task<ChannelResponse?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        return await _context.Channels
            .Include(c => c.Owner)
            .Include(c => c.Members)
            .Where(c => c.ChannelId == id)
            .Select(c => new ChannelResponse
            {
                Id = c.ChannelId,
                Title = c.Title,
                Description = c.Description,
                OwnerId = c.OwnerId,
                OwnerName = c.Owner.UserName,
                CreatedAt = c.CreatedAt,
                MemberCount = c.Members.Count()
            })
            .FirstOrDefaultAsync(ct);
    }

    public async Task<IEnumerable<ChannelResponse>> GetByOwnerIdAsync(Guid ownerId, CancellationToken ct = default)
    {
        return await _context.Channels
            .Include(c => c.Owner)
            .Include(c => c.Members)
            .Where(c => c.OwnerId == ownerId)
            .Select(c => new ChannelResponse
            {
                Id = c.ChannelId,
                Title = c.Title,
                Description = c.Description,
                OwnerId = c.OwnerId,
                OwnerName = c.Owner.UserName,
                CreatedAt = c.CreatedAt,
                MemberCount = c.Members.Count()
            })
            .ToListAsync(ct);
    }

    public async Task<IEnumerable<ChannelResponse>> GetAllAsync(CancellationToken ct = default)
    {
        return await _context.Channels
            .Include(c => c.Owner)
            .Include(c => c.Members)
            .Select(c => new ChannelResponse
            {
                Id = c.ChannelId,
                Title = c.Title,
                Description = c.Description,
                OwnerId = c.OwnerId,
                OwnerName = c.Owner.UserName,
                CreatedAt = c.CreatedAt,
                MemberCount = c.Members.Count()
            })
            .ToListAsync(ct);
    }

    // creatorId comes from auth context (HttpContext / JWT claim)
    public async Task<ChannelResponse> AddAsync(Guid creatorId, ChannelRequest request, CancellationToken ct = default)
    {
        var channel = new ChannelEntity
        {
            ChannelId = Guid.NewGuid(),
            Title = request.Title,
            Description = request.Description,
            OwnerId = creatorId,
            CreatedAt = DateTime.UtcNow
        };

        await _context.Channels.AddAsync(channel, ct);
        await _context.SaveChangesAsync(ct);

        return await GetByIdAsync(channel.ChannelId, ct)
            ?? throw new InvalidOperationException("Failed to create channel");
    }

    public async Task UpdateAsync(Guid id, ChannelRequest request, CancellationToken ct = default)
    {
        var channel = await _context.Channels.FindAsync(new object[] { id }, ct)
            ?? throw new KeyNotFoundException("Channel not found");

        channel.Title = request.Title;
        channel.Description = request.Description;

        await _context.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct = default)
    {
        var channel = await _context.Channels.FindAsync(new object[] { id }, ct);
        if (channel != null)
        {
            _context.Channels.Remove(channel);
            await _context.SaveChangesAsync(ct);
        }
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken ct = default)
    {
        return await _context.Channels.AnyAsync(c => c.ChannelId == id, ct);
    }

    public async Task SetProfileAsync(Guid channelId, Guid ownerId, ChannelProfileRequest request, CancellationToken ct = default)
    {
        var channel = await _context.Channels
            .Include(c => c.Profiles)
            .FirstOrDefaultAsync(c => c.ChannelId == channelId, ct)
            ?? throw new KeyNotFoundException("Channel not found");

        foreach (var p in channel.Profiles.Where(p => p.IsActive))
            p.IsActive = false;

        channel.Profiles.Add(new ChannelProfile
        {
            ChannelProfileId = Guid.NewGuid(),
            ChannelId = channelId,
            DisplayName = request.DisplayName,
            Description = request.Description,
            AvatarUrl = request.AvatarUrl,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        });

        await _context.SaveChangesAsync(ct);
    }
}
