using RestSharp;
using CSChatLogger.Api;
using CSChatLogger.Entity;
using CSChatLogger.Schema;

namespace CSChatLogger.Persistence
{
    public abstract class ContextService(Context context)
    {

        protected readonly Context _context = context;

        protected async Task<bool> GetUserIsInChat(long userId, long chatId)
        {
            var chatAccount = await _context.FindAsync<ChatAccount>(userId, chatId);

            return chatAccount != null;
        }

        protected async Task<bool> GetUserSentMessage(long accountId, long chatId,
            long messageId)
        {
            var chatMessage = await _context.ChatMessages.FindAsync(chatId, messageId);
            return chatMessage != null && chatMessage.UserId == accountId;
        }

        protected long ValidateAuthorization(Guid? token)
        {
            if (token == null)
                throw new UnauthorizedException();

            return GetUserId((Guid) token);
        }

        protected long ValidateAuthorization(Guid? token, long chatId)
        {
            long accountId = ValidateAuthorization(token);

            if (!GetUserIsInChat(accountId, chatId).Result)
                throw new UnauthorizedException();

            return accountId;
        }

        protected long ValidateAuthorization(Guid? token, long chatId, long messageId)
        {
            long accountId = ValidateAuthorization(token, chatId);

            if (!GetUserSentMessage(accountId, chatId, messageId).Result)
                throw new UnauthorizedException();

            return accountId;
        }

        protected static long GetUserId(Guid token)
        {
            // Input
            string ipTag = "PYACCT_URI", portTag = "PYACCT_PORT";

            string? ip = Environment.GetEnvironmentVariable(ipTag);
            string? port = Environment.GetEnvironmentVariable(portTag);

            // Input validation
            if (ip == null)
            {
                throw new ArgumentException($"Missing environment variable {ipTag}");
            }
            if (port == null)
            {
                throw new ArgumentException($"Missing environment variable {portTag}");
            }

            // Implementation
            var options = new RestClientOptions($"{ip}:{port}/pyacct/1");
            var client = new RestClient(options);

            var request = new RestRequest("/account");
            request.AddHeader("token", token.ToString());

            var accountDto = client.Get<AccountDto>(request) ?? throw new UnauthorizedException();
            
            return accountDto.id;
        }

        public class UnauthorizedException() : Exception { }

        public class BadRequestException() : Exception { }

        public class ConflictException() : Exception { }

        public class NotFoundException() : Exception { }
    }
}
