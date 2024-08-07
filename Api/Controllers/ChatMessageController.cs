using Microsoft.AspNetCore.Mvc;
using CSChatLogger.Schema;
using CSChatLogger.Persistence;

namespace CSChatLogger.Api
{
    [Route("/cschat/1/chat")]
    [ApiController]
    public class ChatMessageController(Context context) : ControllerBase
    {
        private readonly ChatMessageService service = new(context);

        [HttpPost("{chat_id}/message")]
        public IActionResult SendChatMessage(Guid? token, long chat_id, SendChatMessageInput dto)
        {
            try
            {
                service.CreateChatMessage(token, chat_id, dto);
            }
            catch (ContextService.UnauthorizedException)
            {
                return Unauthorized();
            }

            return NoContent();
        }

        [HttpGet("{chat_id}/message")]
        public async Task<ActionResult<ReadChatMessagesOutput>> ReadChatMessages(Guid? token, long chat_id)
        {
            ReadChatMessagesOutput? output;
            try
            {
                output = await service.ReadChatMessages(token, chat_id);
            }
            catch (ContextService.UnauthorizedException)
            {
                return Unauthorized();
            }

            return output;
        }

        [HttpPut("{chat_id}/message/{message_id}")]
        public IActionResult UpdateChatMessage(Guid? token, long chat_id, long message_id, UpdateChatMessageInput dto)
        {
            try
            {
                service.UpdateChatMessage(token, chat_id, message_id, dto);
            }
            catch (ContextService.UnauthorizedException)
            {
                return Unauthorized();
            }
            catch (ContextService.NotFoundException)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{chat_id}/message/{message_id}")]
        public IActionResult DeleteChatMessage(Guid? token, long chat_id, long message_id)
        {
            try
            {
                service.DeleteChatMessage(token, chat_id, message_id);
            }
            catch (ContextService.UnauthorizedException)
            {
                return Unauthorized();
            }
            catch (ContextService.NotFoundException)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
