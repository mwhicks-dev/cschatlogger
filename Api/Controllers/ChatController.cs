using Microsoft.AspNetCore.Mvc;
using CSChatLogger.Persistence;
using CSChatLogger.Schema;

namespace CSChatLogger.Api
{
    [Route("/cschat/1/chat")]
    [ApiController]
    public class ChatController(Context context) : ControllerBase
    {
        private readonly ChatService service = new(context);

        [HttpPost]
        public async Task<IActionResult> CreateChat([FromHeader] Guid? token, CreateChatInput dto)
        {
            try
            {
                await service.CreateChat(token, dto);
            }
            catch (ContextService.UnauthorizedException)
            {
                return Unauthorized();
            }
            catch (ContextService.BadRequestException)
            {
                return BadRequest();
            }

            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<ChatsDto>> GetChats([FromHeader] Guid? token)
        {
            ChatsDto chats;
            try
            {
                chats = await service.GetChats(token);
            }
            catch (ContextService.UnauthorizedException)
            {
                return Unauthorized();
            }

            return chats;
        }
    }
}
