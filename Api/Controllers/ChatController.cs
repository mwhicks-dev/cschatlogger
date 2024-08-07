using Microsoft.AspNetCore.Mvc;
using CSChatLogger.Api;
using CSChatLogger.Entity;
using CSChatLogger.Schema;

namespace Api.Controllers
{
    [Route("/cschat/1/chat")]
    [ApiController]
    public class ChatController(Context context) : ControllerBase
    {
        private readonly Context _context = context;

        [HttpPost]
        public async Task<IActionResult> CreateChat(Guid? id, CreateChatInput dto)
        {
            if (id == null)
                return Unauthorized();

            if (dto.accounts == null)
            {
                return BadRequest();
            }

            Chat chat = new();
            _context.Chats.Add(chat);
            await _context.SaveChangesAsync();

            foreach (long account in dto.accounts)
            {
                ChatAccount chatAccount = new();
                chatAccount.ChatId = chat.Id;
                chatAccount.UserId = account;
                _context.ChatAccounts.Add(chatAccount);
                await _context.SaveChangesAsync();
            }

            return NoContent();
        }
    }
}
