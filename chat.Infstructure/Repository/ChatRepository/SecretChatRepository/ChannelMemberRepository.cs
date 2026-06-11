using Microsoft.EntityFrameworkCore;
using secre_chat_api.chat.Domain.DTOS;
using secre_chat_api.chat.Domain.Entities;
using secre_chat_api.chat.Infstructure.Repository.ChatRepository.ISecretChatRepositries;

namespace secre_chat_api.chat.Infrastructure.Persistence.Repositories;

public class ChannelMemberRepository : IChannelMemberRepository
{
    private readonly ChatDbContext _context;

    public ChannelMemberRepository(ChatDbContext context)
    {
        _context = context;
    }

    public async Task<ChannelMemberResponse?> GetByIdAsync(Guid channelId, Guid userId, CancellationToken ct = default)
    {
        return await _context.ChannelMembers
            .Include(m => m.User)
            .Where(m => m.ChannelId == channelId && m.UserId == userId)
            .Select(m => new ChannelMemberResponse
            {
                ChannelId = m.ChannelId,
                UserId = m.UserId,
                UserName = m.User.UserName,
                IsAdmin = m.IsAdmin,
                JoinedAt = m.JoinedAt
            })
            .FirstOrDefaultAsync(ct);
    }

    public async Task<IEnumerable<ChannelMemberResponse>> GetByChannelIdAsync(Guid channelId, CancellationToken ct = default)
    {
        return await _context.ChannelMembers
            .Include(m => m.User)
            .Where(m => m.ChannelId == channelId)
            .Select(m => new ChannelMemberResponse
            {
                ChannelId = m.ChannelId,
                UserId = m.UserId,
                UserName = m.User.UserName,
                IsAdmin = m.IsAdmin,
                JoinedAt = m.JoinedAt
            })
            .ToListAsync(ct);
    }

    public async Task<IEnumerable<ChannelMemberResponse>> GetByUserIdAsync(Guid userId, CancellationToken ct = default)
    {
        return await _context.ChannelMembers
            .Include(m => m.User)
            .Where(m => m.UserId == userId)
            .Select(m => new ChannelMemberResponse
            {
                ChannelId = m.ChannelId,
                UserId = m.UserId,
                UserName = m.User.UserName,
                IsAdmin = m.IsAdmin,
                JoinedAt = m.JoinedAt
            })
            .ToListAsync(ct);
    }

    // userId comes from auth context, not from request body
    public async Task<ChannelMemberResponse> AddAsync(Guid userId, ChannelMemberRequest request, CancellationToken ct = default)
    {
        var member = new ChannelMemberEntity
        {
            ChannelId = request.ChannelId,
            UserId = userId,
            IsAdmin = request.IsAdmin,
            JoinedAt = DateTime.UtcNow
        };

        await _context.ChannelMembers.AddAsync(member, ct);
        await _context.SaveChangesAsync(ct);

        return await GetByIdAsync(request.ChannelId, userId, ct)
            ?? throw new InvalidOperationException("Failed to add member");
    }

    public async Task UpdateAdminStatusAsync(Guid channelId, Guid userId, bool isAdmin, CancellationToken ct = default)
    {
        var member = await _context.ChannelMembers
            .FirstOrDefaultAsync(m => m.ChannelId == channelId && m.UserId == userId, ct)
            ?? throw new KeyNotFoundException("Member not found");

        member.IsAdmin = isAdmin;
        await _context.SaveChangesAsync(ct);
    }

    public async Task RemoveAsync(Guid channelId, Guid userId, CancellationToken ct = default)
    {
        var member = await _context.ChannelMembers
            .FirstOrDefaultAsync(m => m.ChannelId == channelId && m.UserId == userId, ct);

        if (member != null)
        {
            _context.ChannelMembers.Remove(member);
            await _context.SaveChangesAsync(ct);
        }
    }
}
