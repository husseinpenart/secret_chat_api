using Microsoft.EntityFrameworkCore;
using secre_chat_api.chat.Application.MiddleWares;
using secre_chat_api.chat.Domain.DTOS.UserDtos;
using secre_chat_api.chat.Domain.Entities;
using secre_chat_api.chat.Infstructure.Repository.ChatRepository.ISecretChatRepositries;

namespace secre_chat_api.chat.Infrastructure.Persistence.Repositories;

public class AuthRepository : IAuthRepository
{
    private readonly AppDbContext _context;

    public AuthRepository(AppDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task AddUserAsync(UserRegisterDto dto, CancellationToken ct = default)
    {
        var user = new UserEntity
        {
            UserId = Guid.NewGuid(),
            UserName = dto.userName,
            PhoneNumber = dto.phoneNumber,
            Password = dto.Password, // hash before calling this
            RegisteredDate = DateTime.UtcNow
        };

        await _context.Users.AddAsync(user, ct);
        await _context.SaveChangesAsync(ct);
    }

    public async Task<UserEntity?> GetUserByPhoneAsync(string phone, CancellationToken ct = default)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.PhoneNumber == phone, ct);
    }

    public async Task SetProfileAsync(Guid userId, UserProfileRequest request, CancellationToken ct = default)
    {
        var user = await _context.Users
            .Include(u => u.Profiles)
            .FirstOrDefaultAsync(u => u.UserId == userId, ct)
            ?? throw new KeyNotFoundException("User not found");

        // Deactivate existing active profile
        foreach (var p in user.Profiles.Where(p => p.IsActive))
            p.IsActive = false;

        user.Profiles.Add(new UserProfile
        {
            ProfileId = Guid.NewGuid(),
            UserId = userId,
            DisplayName = request.DisplayName,
            Bio = request.Bio,
            AvatarUrl = request.AvatarUrl,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        });

        await _context.SaveChangesAsync(ct);
    }
}
