using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ExpenseTracker.Domain.Models;
using ExpenseTracker.Business.Core;
using ExpenseTracker.Business.Resources;

namespace ExpenseTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionsController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        // GET: api/Transactions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Transactions>>> GetTransaction()
        {
            return Ok(await _transactionService.GetAllAsync());
        }

       
        // GET: api/Transactions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TransactionResource>> GetTransaction(int id)
        {
            var transaction = await _transactionService.GetByIdAsync(id);

            if (transaction == null)
            {
                return NotFound();
            }

            return transaction;
        }

        // PUT: api/Transactions/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTransaction(int id, TransactionResource transaction)
        {
            if (id != transaction.Id)
            {
                return BadRequest();
            }

            try
            {
                var result = await _transactionService.EditAsync(transaction);

                return Ok(result);
            }
            catch (DbUpdateConcurrencyException)
            {

                throw;
            }

            return NoContent();
        }

        // POST: api/Transactions
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<TransactionResource>> PostTransaction(TransactionResource transaction)
        {

            await _transactionService.CreateAsync(transaction);

            return CreatedAtAction("GetTransaction", new { id = transaction.Id }, transaction);
        }

        // DELETE: api/Transactions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TransactionResource>> DeleteTransaction(int id)
        {


            return Ok(await _transactionService.RemoveAsync(id));
        }

    }
}
