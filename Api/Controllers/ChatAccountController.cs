using Microsoft.AspNetCore.Mvc;
using CSChatLogger.Schema;
using CSChatLogger.Persistence;

namespace CSChatLogger.Api
{
    [Route("/cschat/1/chat")]
    [ApiController]
    public class ChatAccountController(Context context) : ControllerBase
    {
        private readonly ChatAccountService service = new(context);

        [HttpGet("{chat_id}/account")]
        public async Task<ActionResult<ReadChatAccountsOutput>> ReadChatAccounts([FromHeader] Guid? token, long chat_id)
        {
            ReadChatAccountsOutput? output;
            try
            {
                output = await service.ReadChatAccounts(token, chat_id);
            }
            catch (ContextService.UnauthorizedException)
            {
                return Unauthorized();
            }

            return output;
        }

        [HttpPut("{chat_id}/account")]
        public IActionResult AddNewUserToChat([FromHeader] Guid? token, long chat_id, UpdateChatAccountInput dto)
        {
            try
            {
                service.CreateChatAccount(token, chat_id, dto);
            }
            catch (ContextService.UnauthorizedException)
            {
                return Unauthorized();
            }
            catch (ContextService.ConflictException)
            {
                return Conflict();
            }

            return NoContent();
        }

        [HttpDelete("{chat_id}/account")]
        public IActionResult LeaveChat([FromHeader] Guid? token, long chat_id)
        {
            try
            {
                service.DeleteChatAccount(token, chat_id);
            }
            catch (ContextService.UnauthorizedException)
            {
                return Unauthorized();
            }

            return NoContent();
        }
    }
}
