using Microsoft.EntityFrameworkCore;
using secre_chat_api.chat.Domain.DTOS;
using secre_chat_api.chat.Domain.Entities;
using secre_chat_api.chat.Domain.Interfaces;

namespace secre_chat_api.chat.Infrastructure.Persistence.Repositories;

public class ChatParticipantRepository : IChatParticipantRepository
{
    private readonly ChatDbContext _context;

    public ChatParticipantRepository(ChatDbContext context)
    {
        _context = context;
    }

    public async Task<ChatParticipantResponse?> GetByIdAsync(Guid chatId, Guid userId, CancellationToken ct = default)
    {
        return await _context.ChatParticipants
            .Include(p => p.User)
            .Where(p => p.ChatId == chatId && p.UserId == userId)
            .Select(p => new ChatParticipantResponse
            {
                UserId = p.UserId,
                UserName = p.User.UserName,
                JoinedAt = p.JoinedAt
            })
            .FirstOrDefaultAsync(ct);
    }

    public async Task<IEnumerable<ChatParticipantResponse>> GetByChatIdAsync(Guid chatId, CancellationToken ct = default)
    {
        return await _context.ChatParticipants
            .Include(p => p.User)
            .Where(p => p.ChatId == chatId)
            .Select(p => new ChatParticipantResponse
            {
                UserId = p.UserId,
                UserName = p.User.UserName,
                JoinedAt = p.JoinedAt
            })
            .ToListAsync(ct);
    }

    public async Task<IEnumerable<ChatParticipantResponse>> GetByUserIdAsync(Guid userId, CancellationToken ct = default)
    {
        return await _context.ChatParticipants
            .Include(p => p.User)
            .Where(p => p.UserId == userId)
            .Select(p => new ChatParticipantResponse
            {
                UserId = p.UserId,
                UserName = p.User.UserName,
                JoinedAt = p.JoinedAt
            })
            .ToListAsync(ct);
    }

    public async Task AddAsync(Guid chatId, Guid userId, CancellationToken ct = default)
    {
        var participant = new ChatParticipantDtos
        {
            ChatId = chatId,
            UserId = userId
        };

        await _context.ChatParticipants.AddAsync(participant, ct);
        await _context.SaveChangesAsync(ct);
    }

    public async Task RemoveAsync(Guid chatId, Guid userId, CancellationToken ct = default)
    {
        var participant = await _context.ChatParticipants
            .FirstOrDefaultAsync(p => p.ChatId == chatId && p.UserId == userId, ct);

        if (participant != null)
        {
            _context.ChatParticipants.Remove(participant);
            await _context.SaveChangesAsync(ct);
        }
    }

    public async Task<bool> ExistsAsync(Guid chatId, Guid userId, CancellationToken ct = default)
    {
        return await _context.ChatParticipants
            .AnyAsync(p => p.ChatId == chatId && p.UserId == userId, ct);
    }
}