using Microsoft.AspNetCore.Mvc;
using TodoApp.Data.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TodoApp.Controllers
{
    [Route("api/todo")]
    [ApiController]
    public class TaskController : ControllerBase
    {

        private readonly ILogger<TaskController> _logger;
        private readonly TodoApplicationDbContext dbContext;
        private readonly ITaskService service;

        public TaskController(TodoApplicationDbContext db, ITaskService appService)
        {
            dbContext = db;
            //  _logger = logger;
            service = appService;

        }
        // GET: api/<TaskController>
        [HttpGet("all")]
        public async Task<ActionResult> GetAllItems()
        {
            var items = await service.GetAll();
            return Ok(items);
        }

        // GET api/<TaskController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetItem(int id)
        {
            var item = await service.GetById(id);
            if (item != null)
            {
                return Ok(item);
            }
            return NotFound();
        }

        // POST api/<TaskController>
        [HttpPost]
        public async Task<ActionResult> CreateItem([FromBody] Item item)
        {
            if (ModelState.IsValid)
            {
                var CreatedItem = await service.Create(item);
                return CreatedAtAction("GetItem", new { id = CreatedItem.Id }, CreatedItem);
            }

            return BadRequest(ModelState);
        }


        // PUT api/<TaskController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateItem(int id, [FromBody] Item value)
        {
            try
            {
                await service.Update(value);
            }
            catch (DbUpdateConcurrencyException ex)
            {

                if (!dbContext.Items.Any(p => p.Id == id))
                {
                    //    _logger.LogInformation("Item not found");
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
        public async Task<ActionResult> DeleteItem(int id)
        {
            var item = await service.GetById(id);
            if (item != null)
            {
                var deletedItem = await service.Delete(item);
                return Ok(deletedItem);
            }
            return NotFound();
        }
    }
}
