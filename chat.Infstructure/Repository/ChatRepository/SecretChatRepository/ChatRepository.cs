using secre_chat_api.chat.Domain.DTOS;
using secre_chat_api.chat.Domain.Entities;
using secre_chat_api.chat.Infstructure.Repository.ChatRepository.ISecretChatRepositries;

public class ChatRepository : IChatRepository
{
    private readonly ChatDbContext _context;

    public ChatRepository(ChatDbContext context)
    {
        _context = context;
    }

    public async Task<ChatResponse?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        return await _context.Chats
            .Include(c => c.Participants)
                .ThenInclude(p => p.User)
            .Where(c => c.ChatId == id)
            .Select(c => new ChatResponse
            {
                Id = c.ChatId,
                Type = c.Type,
                Title = c.Title,
                CreatedAt = c.CreatedAt,
                Participants = c.Participants.Select(p => new ChatParticipantResponse
                {
                    UserId = p.UserId,
                    UserName = p.User.UserName,
                    JoinedAt = p.JoinedAt
                }).ToList()
            })
            .FirstOrDefaultAsync(ct);
    }

    public async Task<IEnumerable<ChatResponse>> GetByUserIdAsync(Guid userId, CancellationToken ct = default)
    {
        return await _context.Chats
            .Include(c => c.Participants)
                .ThenInclude(p => p.User)
            .Where(c => c.Participants.Any(p => p.UserId == userId))
            .Select(c => new ChatResponse
            {
                Id = c.ChatId,
                Type = c.Type,
                Title = c.Title,
                CreatedAt = c.CreatedAt,
                Participants = c.Participants.Select(p => new ChatParticipantResponse
                {
                    UserId = p.UserId,
                    UserName = p.User.UserName,
                    JoinedAt = p.JoinedAt
                }).ToList()
            })
            .ToListAsync(ct);
    }

    public async Task<ChatResponse> AddAsync(ChatRequest request, CancellationToken ct = default)
    {
        var chat = new ChatDtos
        {
            Type = request.Type,
            Title = request.Title
        };

        await _context.Chats.AddAsync(chat, ct);
        await _context.SaveChangesAsync(ct);

        // Add participants
        foreach (var participantId in request.ParticipantIds)
        {
            _context.ChatParticipants.Add(new ChatParticipantDtos
            {
                ChatId = chat.ChatId,
                UserId = participantId
            });
        }
        await _context.SaveChangesAsync(ct);

        return await GetByIdAsync(chat.ChatId, ct)
            ?? throw new InvalidOperationException("Failed to create chat");
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct = default)
    {
        var chat = await _context.Chats.FindAsync(new object[] { id }, ct);
        if (chat != null)
        {
            _context.Chats.Remove(chat);
            await _context.SaveChangesAsync(ct);
        }
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken ct = default)
    {
        return await _context.Chats.AnyAsync(c => c.ChatId == id, ct);
    }
}