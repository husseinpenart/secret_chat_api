using Microsoft.EntityFrameworkCore;
using secre_chat_api.chat.Domain.DTOS;
using secre_chat_api.chat.Domain.Entities;
using secre_chat_api.chat.Domain.Interfaces; // Adjust namespace as needed

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
                OwnerName = c.Owner.UserName, // Assuming UserEntity has UserName
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

    public async Task<ChannelResponse> AddAsync(ChannelRequest request, CancellationToken ct = default)
    {
        var channel = new ChannelDtos
        {
            Title = request.Title,
            Description = request.Description,
            OwnerId = request.OwnerId
        };

        await _context.Channels.AddAsync(channel, ct);
        await _context.SaveChangesAsync(ct);

        return await GetByIdAsync(channel.ChannelId, ct)
            ?? throw new InvalidOperationException("Failed to create channel");
    }

    public async Task UpdateAsync(Guid id, ChannelRequest request, CancellationToken ct = default)
    {
        var channel = await _context.Channels.FindAsync(new object[] { id }, ct);
        if (channel == null) throw new KeyNotFoundException("Channel not found");

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
}