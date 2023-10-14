using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Bank_Data_DLL;
using Bank_Data_Web_Service.Data;

namespace Bank_Data_Web_Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly DBManager _context;

        public AccountsController(DBManager context)
        {
            _context = context;
        }

        // GET: api/Accounts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Account>>> GetAccount()
        {
          if (_context.Account == null)
          {
              return NotFound();
          }
            return await _context.Account
                .Include(a => a.Transactions)
                .ToListAsync();
        }

        // GET: api/Accounts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Account>> GetAccount(int id)
        {
            if (_context.Account == null)
            {
                return NotFound();
            }
            var account = await _context.Account
                .Include(a => a.Transactions)
                .FirstOrDefaultAsync(a => a.AccountId == id);

            if (account == null)
            {
                return NotFound();
            }

            return account;
        }

        // GET: api/Accounts/no/45
        [HttpGet("no/{accNo}")]
        public async Task<ActionResult<Account>> GetAccountByNo(int accNo)
        {
            if (_context.Account == null)
            {
                return NotFound();
            }
            var account = await _context.Account
                .Include(a => a.Transactions)
                .FirstOrDefaultAsync(a => a.AccountNo == accNo);

            if (account == null)
            {
                return NotFound();
            }

            return account;
        }

        // GET: api/Accounts/holder/1
        [HttpGet("holder/{holderId}")]
        public async Task<ActionResult<Account>> GetAccountByHolder(int holderId)
        {
            if (_context.Account == null)
            {
                return NotFound();
            }
            var account = await _context.Account
                .Include (a => a.Transactions)
                .FirstOrDefaultAsync(a => a.UserId == holderId);

            if (account == null)
            {
                return NotFound();
            }

            return account;
        }

        // PUT: api/Accounts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccount(int id, Account account)
        {
            if (id != account.AccountId)
            {
                return BadRequest();
            }

            _context.Entry(account).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountExists(id))
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

        // POST: api/Accounts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Account>> PostAccount(Account account)
        {
          if (_context.Account == null)
          {
              return Problem("Entity set 'DBManager.Account'  is null.");
          }
            _context.Account.Add(account);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAccount", new { id = account.AccountId }, account);
        }

        // DELETE: api/Accounts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount(int id)
        {
            if (_context.Account == null)
            {
                return NotFound();
            }
            var account = await _context.Account.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }

            _context.Account.Remove(account);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AccountExists(int id)
        {
            return (_context.Account?.Any(e => e.AccountId == id)).GetValueOrDefault();
        }
    }
}
