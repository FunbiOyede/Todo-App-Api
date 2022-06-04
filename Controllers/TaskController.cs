using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TodoApp.Controllers
{
    [Route("api/todo")]
    [ApiController]
    public class TaskController : ControllerBase
    {

        private readonly TodoApplicationDbContext dbContext;

        public TaskController(TodoApplicationDbContext db)
        {
            this.dbContext = db;

        }
        // GET: api/<TaskController>
        [HttpGet("all")]
        public async Task<ActionResult> GetAllItems()
        {
            var items = await dbContext.Items.ToListAsync();
            return Ok(items);
        }

        // GET api/<TaskController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetItem(int id)
        {
            var item = await dbContext.Items.FindAsync(id);
            if (item != null)
            {
                return Ok(item);
            }
            return NotFound();
        }

        // POST api/<TaskController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Item item)
        {
            if (ModelState.IsValid)
            {
                dbContext.Items.Add(item);
                await dbContext.SaveChangesAsync();
                return CreatedAtAction("GetItem", new { id = item.Id }, item);
            }

            return BadRequest();
        }

        // PUT api/<TaskController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Item value)
        {

            dbContext.Entry(value).State = EntityState.Modified;


            try
            {
                await dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {

                if (!dbContext.Items.Any(p => p.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw ex;
                }


            }


            return NoContent();



        }




        // DELETE api/<TaskController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Item>> Delete(int id)
        {
            var item = await dbContext.Items.FindAsync(id);
            if (item != null)
            {
                dbContext.Items.Remove(item);
                await dbContext.SaveChangesAsync();
                return item;
            }
            return NotFound();
        }
    }
}
