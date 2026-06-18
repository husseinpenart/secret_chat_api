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

    public async Task<UserResponse?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.UserId == id, ct);

        return user is null ? null : MapToUserResponse(user);
    }

    public async Task<UserResponse?> GetByPhoneNumberAsync(string phoneNumber, CancellationToken ct = default)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.PhoneNumber == phoneNumber, ct);

        return user is null ? null : MapToUserResponse(user);
    }

    public async Task<UserResponse> RegisterAsync(UserRegisterDto request, CancellationToken ct = default)
    {
        var user = new UserEntity
        {
            UserId = Guid.NewGuid(),
            UserName = request.userName,
            PhoneNumber = request.phoneNumber,
            Password = request.Password, // hash before calling this
            RegisteredDate = DateTime.UtcNow
        };

        await _context.Users.AddAsync(user, ct);
        await _context.SaveChangesAsync(ct);

        return MapToUserResponse(user);
    }

    public async Task<UserProfileResponse> SetProfileAsync(Guid userId, UserProfileRequest request, CancellationToken ct = default)
    {
        var user = await _context.Users
            .Include(u => u.Profiles)
            .FirstOrDefaultAsync(u => u.UserId == userId, ct)
            ?? throw new KeyNotFoundException("User not found");

        // Deactivate existing active profile
        foreach (var p in user.Profiles.Where(p => p.IsActive))
            p.IsActive = false;

        var newProfile = new UserProfile
        {
            ProfileId = Guid.NewGuid(),
            UserId = userId,
            DisplayName = request.DisplayName,
            Bio = request.Bio,
            AvatarUrl = request.AvatarUrl,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        user.Profiles.Add(newProfile);
        await _context.SaveChangesAsync(ct);

        return MapToUserProfileResponse(newProfile);
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken ct = default)
    {
        return await _context.Users.AnyAsync(u => u.UserId == id, ct);
    }

    private static UserResponse MapToUserResponse(UserEntity user)
    {
        return new UserResponse
        {
            UserId = user.UserId,
            UserName = user.UserName,
            PhoneNumber = user.PhoneNumber,
            RegisteredDate = user.RegisteredDate
        };
    }

    private static UserProfileResponse MapToUserProfileResponse(UserProfile profile)
    {
        return new UserProfileResponse
        {
            ProfileId = profile.ProfileId,
            UserId = profile.UserId,
            DisplayName = profile.DisplayName,
            Bio = profile.Bio,
            AvatarUrl = profile.AvatarUrl,
            IsActive = profile.IsActive,
            CreatedAt = profile.CreatedAt
        };
    }
}