using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TodoApi.Data;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class TodosController : ControllerBase
    {

        private readonly ILogger<TodosController> _logger;
        private readonly ITodoRepository<TodoModel> repository;

        public TodosController(ILogger<TodosController> logger, ITodoRepository<TodoModel> _repository)
        {
            _logger = logger;
            repository = _repository;
        }

        public ActionResult<TodoModel> GetAllTodos()
        {
            return Ok(repository.GetAll());
        }

        [HttpGet("{id}", Name = "GetTodoById")]
        public ActionResult<TodoModel> GetTodoById(int id)
        {
            var todoItem = repository.GetTodoById(id);
            if (todoItem == null)
            {
                return NotFound(id);
            }
            return Ok(todoItem);
        }
        [HttpPost]
        public async Task<ActionResult<TodoModel>> AddTodo(TodoModel todo)
        {
            repository.CreateTodo(todo);
            if(await repository.SaveChanges())
            {
                return Ok(todo);
            }
            return StatusCode(418); // I'm a teapot
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<TodoModel>> UpdateTodo(int id, TodoModel todo)
        {
            var todoToUpdate = repository.GetTodoById(id);
            if (todoToUpdate == null)
            {
                return NotFound();
            }
            todoToUpdate.isComplete = todo.isComplete;
            todoToUpdate.Text = todo.Text;
            repository.Update(todoToUpdate);
            if (await repository.SaveChanges())
            {
                return Ok(todoToUpdate);
            }
            return BadRequest("Update Failed");
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<TodoModel>> RemoveTodo(int id)
        {
            var todoToRemove = repository.GetTodoById(id);
            if (todoToRemove == null)
            {
                return NotFound();
            }
            repository.RemoveTodo(id);
            if (await repository.SaveChanges())
            {
                return Ok(todoToRemove);
            }
            return BadRequest("Remove Failed");
        }
       
    }
}
