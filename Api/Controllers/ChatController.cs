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
        public IActionResult CreateChat([FromHeader] Guid? token, CreateChatInput dto)
        {
            try
            {
                service.CreateChat(token, dto);
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
    }
}
