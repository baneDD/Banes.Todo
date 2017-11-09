using Castle.Core.Logging;
using Banes.ToDo.Models;
using System.Collections.Generic;
using System.Linq;

namespace Banes.ToDo.LoggingExtensions
{
    public static class ToDoTaskControllerLoggingExtensions
    {
        public static ToDoTask LogResultAndReturnToDoTask(this ToDoTask todo, int id, ILogger logger)
        {
            if (string.IsNullOrWhiteSpace(todo?.TaskDescription))
                logger.Info($"Could not find task with ID {id} or the specified task doesn't have a valid description");
            else
                logger.Debug($"Successfully retrieved task '{todo.TaskDescription}' with ID {id}");

            return todo;
        }

        public static IEnumerable<ToDoTask> LogResultAndReturnToDoTaskCollection(this IEnumerable<ToDoTask> todos, ILogger logger)
        {
            var numToDoTasks = todos?.ToList().Count();

            if (numToDoTasks > 0)
                logger.Debug($"Retrieved {numToDoTasks} tasks");
            else
                logger.Info("No tasks found in the specified collection");

            return todos;
        }
        public static void LogCreateOrUpdateResult(this bool newToDoCreated, ToDoTask todo, ILogger logger)
        {
            if (newToDoCreated)
                logger.Debug($"Created a new task '{todo.TaskDescription}' at {todo.TaskCreationTime}");
            else
                logger.Debug($"Updated task '{todo.TaskDescription}'");
        }

        public static bool LogDeleteResult(this bool toDoTaskHasBeenDeleted, int id, ILogger logger)
        {
            if (toDoTaskHasBeenDeleted)
                logger.Debug($"Deleted todo with ID {id}");
            else
                logger.Info($"Could not delete todo with ID {id}");

            return toDoTaskHasBeenDeleted;
        }
    }
}