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
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<TaskController>/5
        [HttpGet("{id}")]
        public string GetItem(int id)
        {
            return "value";
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
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TaskController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
