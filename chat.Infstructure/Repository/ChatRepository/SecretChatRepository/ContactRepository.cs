using Microsoft.EntityFrameworkCore;
using secre_chat_api.chat.Domain.DTOS;
using secre_chat_api.chat.Domain.Entities;
using secre_chat_api.chat.Domain.Interfaces;
using secre_chat_api.chat.Infstructure.Repository.ChatRepository.ISecretChatRepositries;

namespace secre_chat_api.chat.Infrastructure.Persistence.Repositories;

public class ContactRepository : IContactRepository
{
    private readonly ChatDbContext _context;

    public ContactRepository(ChatDbContext context)
    {
        _context = context;
    }

    public async Task<ContactResponse?> GetByIdAsync(Guid ownerId, Guid targetUserId, CancellationToken ct = default)
    {
        return await _context.Contacts
            .Include(c => c.TargetUser)
            .Where(c => c.OwnerId == ownerId && c.TargetUserId == targetUserId)
            .Select(c => new ContactResponse
            {
                OwnerId = c.OwnerId,
                TargetUserId = c.TargetUserId,
                TargetUserName = c.TargetUser.UserName,
                Nickname = c.Nickname,
                IsBlocked = c.IsBlocked,
                AddedAt = c.AddedAt
            })
            .FirstOrDefaultAsync(ct);
    }

    public async Task<IEnumerable<ContactResponse>> GetByOwnerIdAsync(Guid ownerId, CancellationToken ct = default)
    {
        return await _context.Contacts
            .Include(c => c.TargetUser)
            .Where(c => c.OwnerId == ownerId)
            .Select(c => new ContactResponse
            {
                OwnerId = c.OwnerId,
                TargetUserId = c.TargetUserId,
                TargetUserName = c.TargetUser.UserName,
                Nickname = c.Nickname,
                IsBlocked = c.IsBlocked,
                AddedAt = c.AddedAt
            })
            .ToListAsync(ct);
    }

    public async Task<ContactResponse> AddAsync(ContactRequest request, Guid ownerId, CancellationToken ct = default)
    {
        var contact = new ContactEntity   // was: ContactDtos
        {
            OwnerId = ownerId,
            TargetUserId = request.TargetUserId,
            Nickname = request.Nickname
        };

        await _context.Contacts.AddAsync(contact, ct);
        await _context.SaveChangesAsync(ct);

        return await GetByIdAsync(ownerId, request.TargetUserId, ct)
            ?? throw new InvalidOperationException("Failed to add contact");
    }

    public async Task UpdateNicknameAsync(Guid ownerId, Guid targetUserId, string? nickname, CancellationToken ct = default)
    {
        var contact = await _context.Contacts
            .FirstOrDefaultAsync(c => c.OwnerId == ownerId && c.TargetUserId == targetUserId, ct);

        if (contact == null) throw new KeyNotFoundException("Contact not found");

        contact.Nickname = nickname;
        await _context.SaveChangesAsync(ct);
    }

    public async Task BlockAsync(Guid ownerId, Guid targetUserId, CancellationToken ct = default)
    {
        var contact = await _context.Contacts
            .FirstOrDefaultAsync(c => c.OwnerId == ownerId && c.TargetUserId == targetUserId, ct);

        if (contact == null) throw new KeyNotFoundException("Contact not found");

        contact.IsBlocked = true;
        await _context.SaveChangesAsync(ct);
    }

    public async Task UnblockAsync(Guid ownerId, Guid targetUserId, CancellationToken ct = default)
    {
        var contact = await _context.Contacts
            .FirstOrDefaultAsync(c => c.OwnerId == ownerId && c.TargetUserId == targetUserId, ct);

        if (contact == null) throw new KeyNotFoundException("Contact not found");

        contact.IsBlocked = false;
        await _context.SaveChangesAsync(ct);
    }

    public async Task RemoveAsync(Guid ownerId, Guid targetUserId, CancellationToken ct = default)
    {
        var contact = await _context.Contacts
            .FirstOrDefaultAsync(c => c.OwnerId == ownerId && c.TargetUserId == targetUserId, ct);

        if (contact != null)
        {
            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync(ct);
        }
    }
}
