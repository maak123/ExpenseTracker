using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ExpenseTracker.Business.Core;
using ExpenseTracker.Business.Resources;

namespace ExpenseTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserResource>>> GetUser()
        {
            return Ok(await _userService.GetAllAsync());
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserResource>> GetUser(int id)
        {
            var user = await _userService.GetByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, UserResource user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            try
            {
                var result = await _userService.EditAsync(user);

                return Ok(result);
            }
            catch (DbUpdateConcurrencyException)
            {

                throw;
            }

            return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<UserResource>> PostUser(UserResource user)
        {

            await _userService.CreateAsync(user);

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<UserResource>> DeleteUser(int id)
        {


            return Ok(await _userService.RemoveAsync(id));
        }

    }
}
