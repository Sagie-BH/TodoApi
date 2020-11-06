using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Models;

namespace TodoApi.Data
{
    public class TodoRepository: ITodoRepository<TodoModel>
    {
        private readonly TodoContext context;

        public TodoRepository(TodoContext _context)
        {
            context = _context;
        }
        public async Task<bool> SaveChanges()
        {
            return await context.SaveChangesAsync() >= 0;
        }
        public void CreateTodo(TodoModel todo)
        {
            if (todo == null)
            {
                throw new ArgumentNullException(nameof(todo));
            }
            context.Todos.Add(todo);
        }
        public IEnumerable<TodoModel> GetAll()
        {
            return context.Todos;
        }
        public TodoModel GetTodoById(int id)
        {
            return context.Todos.FirstOrDefault(todo => todo.Id == id);
        }
        public void RemoveTodo(int id)
        {
            context.Todos.Remove(GetTodoById(id));
        }
        public void Update(TodoModel todo)
        {
            context.Todos.Update(todo);
        }
    }
}
