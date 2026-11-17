using Microsoft.EntityFrameworkCore;
using secre_chat_api.chat.Application.MiddleWares;
using secre_chat_api.chat.Domain.DTOS;
using secre_chat_api.chat.Domain.Entities;
using secre_chat_api.chat.Infstructure.Repository.ChatRepository.ISecretChatRepositries;

namespace secre_chat_api.chat.Infrastructure.Persistence.Repositories;

public class ChatParticipantRepository : IChatParticipantRepository
{
    private readonly AppDbContext _context;

    public ChatParticipantRepository(AppDbContext context)
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

    // userId comes from auth context
    public async Task<ChatParticipantResponse?> AddAsync(ChatParticipantRequest request, Guid requesterId, CancellationToken ct = default)
    {
        var requester = await _context.ChatParticipants.FirstOrDefaultAsync(p => p.ChatId == request.ChatId && p.UserId == requesterId, ct);
        if (requester is null || !requester.IsAdmin) return null;
        var participant = new ChatParticipantEntity
        {
            ChatId = request.ChatId,
            UserId = request.UserId,
            JoinedAt = DateTime.UtcNow
        };

        await _context.ChatParticipants.AddAsync(participant, ct);
        await _context.SaveChangesAsync(ct);
        return await GetByIdAsync(request.ChatId, request.UserId, ct);
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

    public async Task UpdateAdminStatusAsync(Guid chatId, Guid userId, bool isAdmin, Guid requesterId, CancellationToken ct = default)
    {
        var requester = await _context.ChatParticipants.FirstOrDefaultAsync(p => p.ChatId == chatId && p.UserId == requesterId, ct);
        var participant = await _context.ChatParticipants.FirstOrDefaultAsync(p => p.ChatId == chatId && p.UserId == userId, ct);
        if (requester is null || !requester.IsAdmin || participant is null) return;
        participant.IsAdmin = isAdmin;
        await _context.SaveChangesAsync(ct);
    }
}
