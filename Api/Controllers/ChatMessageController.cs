using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CSChatLogger.Api;
using CSChatLogger.Entity;

namespace Api.Controllers
{
    [Route("/cschat/1/chat")]
    [ApiController]
    public class ChatMessageController(Context context) : ControllerBase
    {
        private readonly Context _context = context;

        // GET: api/ChatMessage
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ChatMessage>>> GetChatMessages()
        {
            return await _context.ChatMessages.ToListAsync();
        }

        // GET: api/ChatMessage/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ChatMessage>> GetChatMessage(long id)
        {
            var chatMessage = await _context.ChatMessages.FindAsync(id);

            if (chatMessage == null)
            {
                return NotFound();
            }

            return chatMessage;
        }

        // PUT: api/ChatMessage/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutChatMessage(long id, ChatMessage chatMessage)
        {
            if (id != chatMessage.ChatId)
            {
                return BadRequest();
            }

            _context.Entry(chatMessage).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChatMessageExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ChatMessage
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ChatMessage>> PostChatMessage(ChatMessage chatMessage)
        {
            _context.ChatMessages.Add(chatMessage);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ChatMessageExists(chatMessage.ChatId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetChatMessage", new { id = chatMessage.ChatId }, chatMessage);
        }

        // DELETE: api/ChatMessage/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChatMessage(long id)
        {
            var chatMessage = await _context.ChatMessages.FindAsync(id);
            if (chatMessage == null)
            {
                return NotFound();
            }

            _context.ChatMessages.Remove(chatMessage);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ChatMessageExists(long id)
        {
            return _context.ChatMessages.Any(e => e.ChatId == id);
        }
    }
}
