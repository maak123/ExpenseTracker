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
        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<Transactions>>> GetTransaction(int userId)
        {
            return Ok(await _transactionService.GetAllAsync(userId));
        }

        // PUT: api/Transactions/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTransaction(int id, TransactionAddUpdateResource transaction)
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
        public async Task<ActionResult<TransactionResource>> PostTransaction(TransactionAddUpdateResource transaction)
        {

            var result = await _transactionService.CreateAsync(transaction);

            return Ok(result);
        }

        // DELETE: api/Transactions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TransactionResource>> DeleteTransaction(int id)
        {


            return Ok(await _transactionService.RemoveAsync(id));
        }

    }
}
