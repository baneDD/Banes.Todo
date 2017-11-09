using LiteDB;
using Banes.ToDo.Models;
using System.Collections.Generic;
using System.Linq;

namespace Banes.ToDo.Repositories
{
    public class ToDoTaskRepository : IToDoTaskRepository
    {
        private readonly LiteDatabase Database;
        private readonly string CollectionName;

        public ToDoTaskRepository(string databaseName, string collectionName)
        {
            Database = new LiteDatabase(databaseName);
            CollectionName = collectionName;
        }

        /// <summary>
        /// Gets all todo tasks from the specified database collection
        /// </summary>
        /// <returns>Collection of todo tasks</returns>
        public IEnumerable<ToDoTask> GetAll() => GetToDoTaskCollection().FindAll().AsEnumerable();

        /// <summary>
        /// Gets a todo task by provided id. If the task is not found returns null
        /// </summary>
        /// <param name="taskId">Id of the todo task to get</param>
        /// <returns></returns>
        public ToDoTask GetById(int taskId) => GetToDoTaskCollection().FindById(new BsonValue(taskId));

        /// <summary>
        /// Creates or updates the todo task in the database collection. If a new task
        /// has been created returns true, if an existing one was updated returns false
        /// </summary>
        /// <param name="toDoTask">Todo task to insert or update</param>
        /// <returns></returns>
        public bool Upsert(ToDoTask toDoTask) => GetToDoTaskCollection().Upsert(toDoTask);

        /// <summary>
        /// Deletes the todo task from the collection. Returns true if task was found 
        /// and deleted, false otherwise
        /// </summary>
        /// <param name="taskId">Task id of the task to remove</param>
        /// <returns>True if task found and deleted, false otherwise</returns>
        public bool Delete(int taskId) => GetToDoTaskCollection().Delete(new BsonValue(taskId));

        public void Dispose() => Database.Dispose();

        private LiteCollection<ToDoTask> GetToDoTaskCollection() => Database.GetCollection<ToDoTask>(CollectionName);
    }
}