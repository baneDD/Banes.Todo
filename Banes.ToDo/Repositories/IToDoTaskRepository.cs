using Banes.ToDo.Models;
using System.Collections.Generic;

namespace Banes.ToDo.Repositories
{
    public interface IToDoTaskRepository
    {
        IEnumerable<ToDoTask> GetAll();
        ToDoTask GetById(int taskId);
        bool Upsert(ToDoTask toDoTask);
        bool Delete(int taskId);
    }
}
