using Microsoft.AspNetCore.DataProtection;

namespace secre_chat_api.chat.Application.Services;

public interface IMessageCryptoService
{
    string Protect(string value);
    string Unprotect(string value);
}

public sealed class MessageCryptoService : IMessageCryptoService
{
    private readonly IDataProtector _protector;
    public MessageCryptoService(IDataProtectionProvider provider) => _protector = provider.CreateProtector("secret-chat.message.v1");
    public string Protect(string value) => _protector.Protect(value);
    public string Unprotect(string value) { try { return _protector.Unprotect(value); } catch { return "[encrypted message]"; } }
}
