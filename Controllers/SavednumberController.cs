using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Savednumbers.Data;
using Savednumbers.Models;

namespace Savednumbers.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SavednumberController : Controller
    {
        private readonly SavednumbersDbContext dbContext;

        public SavednumberController(SavednumbersDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        [HttpGet]
        public async Task<IActionResult> GetSavednumber()
        {
            return Ok(await dbContext.Phonebook.ToListAsync());

        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetSavednumber([FromRoute] Guid id)
        {
            var number = await dbContext.Phonebook.FindAsync(id);
            if (number == null)
            {
                return NotFound();
            }

            return Ok(number);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewnumber(AddnumberRequest addnumberRequest)
        {
            var number = new Details()
            {
                Id = Guid.NewGuid(),
                Fullname = addnumberRequest.Fullname,
                Email = addnumberRequest.Email,
                Number = addnumberRequest.Number,
                Address = addnumberRequest.Address,

            };
            await dbContext.Phonebook.AddAsync(number);
            await dbContext.SaveChangesAsync();

            return Ok(number);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> Updatenumber([FromRoute] Guid id, UpdatenumberRequest updatenumberRequest)
        {
            var number = await dbContext.Phonebook.FindAsync(id);

            if (number != null)
            {

                number.Fullname = updatenumberRequest.Fullname;
                number.Email = updatenumberRequest.Email;
                number.Number = updatenumberRequest.Number;
                number.Address = updatenumberRequest.Address;

                await dbContext.SaveChangesAsync();

                return Ok(number);
            }
            return NotFound();
        }


        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteNumber([FromRoute] Guid id)
        {
            var number = await dbContext.Phonebook.FindAsync(id);
            if (number != null)
            {
                dbContext.Remove(number);
                await dbContext.SaveChangesAsync();
                return Ok(number);
            }
            return NotFound();
        }
    }
}
