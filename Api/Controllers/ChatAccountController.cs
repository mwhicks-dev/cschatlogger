using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CSChatLogger.Api;
using CSChatLogger.Entity;

namespace Api.Controllers
{
    [Route("/cschat/1/chat/tmp2")]
    [ApiController]
    public class ChatAccountController(Context context) : ControllerBase
    {
        private readonly Context _context = context;

        // GET: api/ChatAccount
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ChatAccount>>> GetChatAccounts()
        {
            return await _context.ChatAccounts.ToListAsync();
        }

        // GET: api/ChatAccount/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ChatAccount>> GetChatAccount(long id)
        {
            var chatAccount = await _context.ChatAccounts.FindAsync(id);

            if (chatAccount == null)
            {
                return NotFound();
            }

            return chatAccount;
        }

        // PUT: api/ChatAccount/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutChatAccount(long id, ChatAccount chatAccount)
        {
            if (id != chatAccount.UserId)
            {
                return BadRequest();
            }

            _context.Entry(chatAccount).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChatAccountExists(id))
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

        // POST: api/ChatAccount
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ChatAccount>> PostChatAccount(ChatAccount chatAccount)
        {
            _context.ChatAccounts.Add(chatAccount);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ChatAccountExists(chatAccount.UserId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetChatAccount", new { id = chatAccount.UserId }, chatAccount);
        }

        // DELETE: api/ChatAccount/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChatAccount(long id)
        {
            var chatAccount = await _context.ChatAccounts.FindAsync(id);
            if (chatAccount == null)
            {
                return NotFound();
            }

            _context.ChatAccounts.Remove(chatAccount);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ChatAccountExists(long id)
        {
            return _context.ChatAccounts.Any(e => e.UserId == id);
        }
    }
}
