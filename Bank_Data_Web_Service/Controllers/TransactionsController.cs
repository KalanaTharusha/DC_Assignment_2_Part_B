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
    public class TransactionsController : ControllerBase
    {
        private readonly DBManager _context;

        public TransactionsController(DBManager context)
        {
            _context = context;
        }

        // GET: api/Transactions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetTransaction()
        {
          if (_context.Transaction == null)
          {
              return NotFound();
          }
            return await _context.Transaction.ToListAsync();
        }

        // GET: api/Transactions/ get by acc No
        [HttpGet("no/{no}")]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetTransactionsByAccount(int no)
        {
            if (_context.Transaction == null)
            {
                return NotFound();
            }

            Account account = await _context.Account.Where(a => a.AccountNo == no).FirstOrDefaultAsync();

            if (account == null)
            {
                return NotFound();
            }

            return await _context.Transaction.Where(t => t.AccountId == account.AccountId).ToListAsync();
        }

        // GET: api/Transactions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Transaction>> GetTransaction(int id)
        {
          if (_context.Transaction == null)
          {
              return NotFound();
          }
            var transaction = await _context.Transaction.FindAsync(id);

            if (transaction == null)
            {
                return NotFound();
            }

            return transaction;
        }

        // PUT: api/Transactions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTransaction(int id, Transaction transaction)
        {
            if (id != transaction.TransactionId)
            {
                return BadRequest();
            }

            _context.Entry(transaction).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransactionExists(id))
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

        // POST: api/Transactions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Transaction>> PostTransaction(Transaction transaction)
        {
            if (_context.Transaction == null)
            {
                return Problem("Entity set 'DBManager.Transaction'  is null.");
            }
            _context.Transaction.Add(transaction);
            var account = await _context.Account.FindAsync(transaction.AccountId);
            if (account == null)
            {
                return NotFound(transaction.AccountId);
            }

            double balance = account.Balance;

            if (transaction.Type == Transaction.TransactionType.Deposit)
            {
                account.Balance = balance + transaction.Amount;
            }
            else
            {
                if (balance > 0 && transaction.Amount < balance)
                {
                    account.Balance = balance - transaction.Amount;
                    transaction.Amount = 0 - transaction.Amount;
                }
                else
                {
                    return Problem("Not enough balance");
                }
            }

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTransaction", new { id = transaction.TransactionId }, transaction);
        }

        // DELETE: api/Transactions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransaction(int id)
        {
            if (_context.Transaction == null)
            {
                return NotFound();
            }
            var transaction = await _context.Transaction.FindAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }

            _context.Transaction.Remove(transaction);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TransactionExists(int id)
        {
            return (_context.Transaction?.Any(e => e.TransactionId == id)).GetValueOrDefault();
        }
    }
}
