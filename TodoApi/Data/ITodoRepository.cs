using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApi.Models;

namespace TodoApi.Data
{
    public interface ITodoRepository<TEntity> where TEntity: class
    {
        void CreateTodo(TEntity entity);
        IEnumerable<TEntity> GetAll();
        TEntity GetTodoById(int id);
        void RemoveTodo(int id);
        Task<bool> SaveChanges();
        void Update(TEntity entity);
    }
}