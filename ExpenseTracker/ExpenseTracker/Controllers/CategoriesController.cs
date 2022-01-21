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
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryResource>>> GetCategory()
        {
            return Ok(await _categoryService.GetAllAsync());
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryResource>> GetCategory(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return category;
        }

        // GET: api/UserCategoryDetails/5
        [HttpGet("GetUserCategoryDetails/{userId}")]
        public async Task<ActionResult<IEnumerable<CategoryTransactionResource>>> GetUserCategoryDetails(int userId)
        {
            var category = await _categoryService.GetUserCategoryDetailsAsync(userId);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        // PUT: api/Categories/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(int id, CategoryResource category)
        {
            if (id != category.Id)
            {
                return BadRequest();
            }

            try
            {
                var result = await _categoryService.EditAsync(category);

                return Ok(result);
            }
            catch (DbUpdateConcurrencyException)
            {
              
                    throw;
            }

            return NoContent();
        }

        // POST: api/Categories
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<CategoryResource>> PostCategory(CategoryResource category)
        {
          
            await _categoryService.CreateAsync(category);

            return CreatedAtAction("GetCategory", new { id = category.Id }, category);
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CategoryResource>> DeleteCategory(int id)
        {

           
            return Ok(await _categoryService.RemoveAsync(id));
        }

    }
}
