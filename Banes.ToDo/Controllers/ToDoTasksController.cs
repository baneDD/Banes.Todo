using Castle.Core.Logging;
using Banes.ToDo.Models;
using Banes.ToDo.Repositories;
using System.Collections.Generic;
using System.Web.Http;
using Banes.ToDo.LoggingExtensions;

namespace Banes.ToDo.Controllers
{
    // [Authorize]
    public class ToDoTasksController : ApiController
    {
        private readonly IToDoTaskRepository ToDoTaskRepository;
        private readonly ILogger Logger;

        public ToDoTasksController(IToDoTaskRepository toDoTaskRepository, ILogger logger)
        {
            ToDoTaskRepository = toDoTaskRepository;
            Logger = logger;
        }

        [HttpGet]
        [ActionName("todos")]
        public ToDoTask GetById(int id) => ToDoTaskRepository.GetById(id).LogResultAndReturnToDoTask(id, Logger);

        [HttpGet]
        [ActionName("todos")]
        public IEnumerable<ToDoTask> GetAll() => ToDoTaskRepository.GetAll().LogResultAndReturnToDoTaskCollection(Logger);

        [HttpPost]
        [ActionName("todos")]
        public IHttpActionResult Create([FromBody]ToDoTask toDoTask) => CreateOrUpdateToDoTask(toDoTask);

        [HttpPut]
        [ActionName("todos")]
        public IHttpActionResult Update([FromBody]ToDoTask toDoTask) => CreateOrUpdateToDoTask(toDoTask);

        [HttpDelete]
        [ActionName("todos")]
        public IHttpActionResult Delete(int id) => ToDoTaskRepository.Delete(id).LogDeleteResult(id, Logger) ? (IHttpActionResult)Ok() : NotFound();

        private IHttpActionResult CreateOrUpdateToDoTask(ToDoTask toDoTask)
        {
            if (toDoTask == null) return BadRequest();

            ToDoTaskRepository.Upsert(toDoTask).LogCreateOrUpdateResult(toDoTask, Logger);

            return Created("ToDoTasksAPI", toDoTask);
        }
    }
}
