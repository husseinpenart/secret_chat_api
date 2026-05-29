using Microsoft.EntityFrameworkCore;
using secre_chat_api.chat.Application.MiddleWares;
using secre_chat_api.chat.Domain.Entities;

namespace secre_chat_api.chat.Infstructure.Repository.AuthRepository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly AppDbContext _context;
        public AuthRepository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        // Add user to by async
        public async Task AddUserAsync(UserEntity user)
        {
            await _context.userEntities.AddAsync(user);
            await _context.SaveChangesAsync();
        }
        //GewtUserByPhoneAsync
        public async Task<UserEntity?> GewtUserByPhoneAsync(string phone)
        {
            return await _context.userEntities.FirstOrDefaultAsync(e->e.phoneNumber = phone);
        }

        //UpdateUserAsync

        public async Task UpdateUserAsync(UserEntity user)
        {
            _context.userEntities.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}
