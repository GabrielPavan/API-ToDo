using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyToDo.Data;
using MyToDo.Models;
using MyToDo.ViewModels;

namespace MyToDo.Controllers
{
    [ApiController]
    [Route("v1")]
    public class ToDoController : ControllerBase
    {
        #region Get
        [HttpGet("todos")]
        public async Task<IActionResult> GetAsync([FromServices] AppDbContext context)
        {
            var todos = await context
                                .ToDos
                                .AsNoTracking()
                                .ToArrayAsync();
            return Ok(todos);
        }
        [HttpGet("todos/{id}")]
        public async Task<IActionResult> GetByIdAsync([FromServices] AppDbContext context, 
                                                      [FromRoute] int id)
        {
            var todo = await context
                                .ToDos
                                .AsNoTracking()
                                .FirstOrDefaultAsync(x => x.id == id);
            return todo == null ? NotFound() : Ok(todo);
        }
        #endregion

        #region Post
        [HttpPost("todos")]
        public async Task<IActionResult> PostAsync([FromServices]AppDbContext context, 
                                                   [FromBody]CreateToDoViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var todo = new ToDo
            {
                Date = DateTime.Now,
                Done = false,
                Title = model.Title
            };

            try
            {
                await context.ToDos.AddAsync(todo);
                await context.SaveChangesAsync();
                return Created($"v1/todos/{todo.id}", todo);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        #endregion

        #region Put
        [HttpPut("todos/{id}")]
        public async Task<IActionResult> PutAsync([FromServices] AppDbContext context, 
                                                  [FromBody] CreateToDoViewModel model,
                                                  [FromRoute]int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var todo = await context
                                .ToDos
                                .FirstOrDefaultAsync(x => x.id == id);

            if(todo == null) 
                NotFound();
         
            try
            {
                todo.Title = model.Title;
                context.ToDos.Update(todo);
                await context.SaveChangesAsync();
                return Ok(todo);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        #endregion

        #region Delete
        [HttpDelete("todos/{id}")]
        public async Task<IActionResult> DeleteAsync([FromServices]AppDbContext context,
                                                     [FromRoute]int id)
        {
            var todo = await context
                                .ToDos
                                .FirstOrDefaultAsync(x => id == x.id);

            try
            {
                context.Remove(todo);
                await context.SaveChangesAsync();
                return Ok();
            } catch (Exception ex)
            {
                return BadRequest();
            }
            
        }
        #endregion
    }
}