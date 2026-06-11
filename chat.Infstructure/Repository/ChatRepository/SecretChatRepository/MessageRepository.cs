using Microsoft.EntityFrameworkCore;
using secre_chat_api.chat.Domain.DTOS;
using secre_chat_api.chat.Domain.Entities;
using secre_chat_api.chat.Domain.Interfaces;
using secre_chat_api.chat.Infstructure.Repository.ChatRepository.ISecretChatRepositries;

namespace secre_chat_api.chat.Infrastructure.Persistence.Repositories;

public class MessageRepository : IMessageRepository
{
    private readonly ChatDbContext _context;

    public MessageRepository(ChatDbContext context)
    {
        _context = context;
    }

    public async Task<MessageResponse?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        return await _context.Messages
            .Include(m => m.Sender)
            .Where(m => m.MessageId == id)
            .Select(m => new MessageResponse
            {
                Id = m.MessageId,
                SenderId = m.SenderId,
                SenderName = m.Sender.UserName,
                ChatId = m.ChatId,
                ChannelId = m.ChannelId,
                Content = m.Content,
                IsDeleted = m.IsDeleted,
                SentAt = m.SentAt
            })
            .FirstOrDefaultAsync(ct);
    }

    public async Task<IEnumerable<MessageResponse>> GetByChatIdAsync(Guid chatId, int skip = 0, int take = 50, CancellationToken ct = default)
    {
        return await _context.Messages
            .Include(m => m.Sender)
            .Where(m => m.ChatId == chatId && !m.IsDeleted)
            .OrderByDescending(m => m.SentAt)
            .Skip(skip)
            .Take(take)
            .Select(m => new MessageResponse
            {
                Id = m.MessageId,
                SenderId = m.SenderId,
                SenderName = m.Sender.UserName,
                ChatId = m.ChatId,
                ChannelId = m.ChannelId,
                Content = m.Content,
                IsDeleted = m.IsDeleted,
                SentAt = m.SentAt
            })
            .ToListAsync(ct);
    }

    public async Task<IEnumerable<MessageResponse>> GetByChannelIdAsync(Guid channelId, int skip = 0, int take = 50, CancellationToken ct = default)
    {
        return await _context.Messages
            .Include(m => m.Sender)
            .Where(m => m.ChannelId == channelId && !m.IsDeleted)
            .OrderByDescending(m => m.SentAt)
            .Skip(skip)
            .Take(take)
            .Select(m => new MessageResponse
            {
                Id = m.MessageId,
                SenderId = m.SenderId,
                SenderName = m.Sender.UserName,
                ChatId = m.ChatId,
                ChannelId = m.ChannelId,
                Content = m.Content,
                IsDeleted = m.IsDeleted,
                SentAt = m.SentAt
            })
            .ToListAsync(ct);
    }

    public async Task<MessageResponse> AddAsync(MessageRequest request, Guid senderId, CancellationToken ct = default)
    {
        var message = new MessageEntity   // was: MessageDtos
        {
            SenderId = senderId,          // was: request.SenderId
            ChatId = request.ChatId,
            ChannelId = request.ChannelId,
            Content = request.Content
        };

        await _context.Messages.AddAsync(message, ct);
        await _context.SaveChangesAsync(ct);

        return await GetByIdAsync(message.MessageId, ct)
            ?? throw new InvalidOperationException("Failed to send message");
    }

    public async Task SoftDeleteAsync(Guid id, CancellationToken ct = default)
    {
        var message = await _context.Messages.FindAsync(new object[] { id }, ct);
        if (message != null)
        {
            message.IsDeleted = true;
            await _context.SaveChangesAsync(ct);
        }
    }
}
