using Castle.Core.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Banes.ToDo.Controllers;
using Banes.ToDo.Models;
using Banes.ToDo.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Results;

namespace Banes.ToDo.Tests.Controllers
{
    [TestClass]
    public class ToDoTasksControllerTest
    {
        private static readonly Mock<IToDoTaskRepository> MockToDoTaskRepository = new Mock<IToDoTaskRepository>();
        private static readonly Mock<ILogger> MockLogger = new Mock<ILogger>();

        private static List<ToDoTask> MockTodosList => new List<ToDoTask>
        {
            new ToDoTask
            {
                Id = 1,
                TaskDescription = "Go grocery shopping",
                TaskCreationTime = Convert.ToDateTime("2/27/2017 3:52:16 AM"),
                TaskCompleted = false
            },
            new ToDoTask
            {
                Id = 2,
                TaskDescription = "Pack for the trip",
                TaskCreationTime = Convert.ToDateTime("2/27/2017 3:52:19 AM"),
                TaskCompleted = false
            },
        };

        private static ToDoTask MockTodo => new ToDoTask
        {
            Id = 1,
            TaskDescription = "Do the laundry",
            TaskCreationTime = Convert.ToDateTime("2/27/2017 3:49:11 AM"),
            TaskCompleted = false
        };

        [TestMethod]
        public void GetAll_WhenTasksFound_ShouldReturnTasks()
        {
            // Arrange
            MockToDoTaskRepository.Setup(r => r.GetAll()).Returns(MockTodosList);
            var controller = new ToDoTasksController(MockToDoTaskRepository.Object, MockLogger.Object);

            // Act
            var result = controller.GetAll().ToList();

            // Assert
            Assert.IsNotNull(result);

            Assert.AreEqual(2, result.Count());

            Assert.AreEqual(MockTodosList[0].Id, result[0].Id);
            Assert.AreEqual(MockTodosList[0].TaskDescription, result[0].TaskDescription);
            Assert.AreEqual(MockTodosList[0].TaskCreationTime, result[0].TaskCreationTime);
            Assert.AreEqual(MockTodosList[0].TaskCompleted, result[0].TaskCompleted);

            Assert.AreEqual(MockTodosList[1].Id, result[1].Id);
            Assert.AreEqual(MockTodosList[1].TaskDescription, result[1].TaskDescription);
            Assert.AreEqual(MockTodosList[1].TaskCreationTime, result[1].TaskCreationTime);
            Assert.AreEqual(MockTodosList[1].TaskCompleted, result[1].TaskCompleted);
        }

        [TestMethod]
        public void GetAll_WhenNoneFound_ShouldReturnAnEmptyCollection()
        {
            // Arrange
            MockToDoTaskRepository.Setup(r => r.GetAll()).Returns(new List<ToDoTask>());
            var controller = new ToDoTasksController(MockToDoTaskRepository.Object, MockLogger.Object);

            // Act
            var result = controller.GetAll().ToList();

            // Assert
            Assert.IsNotNull(result);

            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        public void GetById_WhenTaskFound_ShouldReturnTask()
        {
            // Arrange
            MockToDoTaskRepository.Setup(r => r.GetById(It.IsAny<int>())).Returns(MockTodo);
            var controller = new ToDoTasksController(MockToDoTaskRepository.Object, MockLogger.Object);

            // Act
            var result = controller.GetById(1);

            // Assert
            Assert.IsNotNull(result);

            Assert.AreEqual(MockTodo.Id, result.Id);
            Assert.AreEqual(MockTodo.TaskDescription, result.TaskDescription);
            Assert.AreEqual(MockTodo.TaskCreationTime, result.TaskCreationTime);
            Assert.AreEqual(MockTodo.TaskCompleted, result.TaskCompleted);
        }

        [TestMethod]
        public void GetById_WhenTaskNotFound_ShouldReturnNull()
        {
            // Arrange
            MockToDoTaskRepository.Setup(r => r.GetById(It.IsAny<int>())).Returns((ToDoTask)null);
            var controller = new ToDoTasksController(MockToDoTaskRepository.Object, MockLogger.Object);

            // Act
            var result = controller.GetById(1);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void Create_WhenTaskNotNull_ShouldCreateNewTask()
        {
            // Arrange
            MockToDoTaskRepository.Setup(r => r.Upsert(It.IsAny<ToDoTask>())).Returns(true);
            var controller = new ToDoTasksController(MockToDoTaskRepository.Object, MockLogger.Object);

            // Act
            var actionResult = controller.Create(MockTodo);
            var createdResult = actionResult as CreatedNegotiatedContentResult<ToDoTask>;

            // Assert
            Assert.IsNotNull(createdResult);
            Assert.IsNotNull(createdResult.Content.Id);
        }

        [TestMethod]
        public void Create_WhenTaskNull_ShouldReturnBadRequest()
        {
            // Arrange
            var controller = new ToDoTasksController(MockToDoTaskRepository.Object, MockLogger.Object);

            // Act
            var actionResult = controller.Create(null);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult));
        }

        [TestMethod]
        public void Update_WhenTaskNotNull_ShouldUpdateTask()
        {
            // Arrange
            MockToDoTaskRepository.Setup(r => r.Upsert(It.IsAny<ToDoTask>())).Returns(true);
            var controller = new ToDoTasksController(MockToDoTaskRepository.Object, MockLogger.Object);

            // Act
            var actionResult = controller.Update(MockTodo);
            var createdResult = actionResult as CreatedNegotiatedContentResult<ToDoTask>;

            // Assert
            Assert.IsNotNull(createdResult);
            Assert.IsNotNull(createdResult.Content.Id);
        }

        [TestMethod]
        public void Update_WhenTaskNull_ShouldReturnBadRequest()
        {
            // Arrange
            var controller = new ToDoTasksController(MockToDoTaskRepository.Object, MockLogger.Object);

            // Act
            var actionResult = controller.Update(null);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult));
        }

        [TestMethod]
        public void Delete_WhenTaskFound_ShouldDeleteTask()
        {
            // Arrange
            MockToDoTaskRepository.Setup(r => r.Delete(It.IsAny<int>())).Returns(true);
            var controller = new ToDoTasksController(MockToDoTaskRepository.Object, MockLogger.Object);

            // Act
            var actionResult = controller.Delete(1);
            var createdResult = actionResult as CreatedNegotiatedContentResult<ToDoTask>;

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkResult));
        }

        [TestMethod]
        public void Delete_WhenTaskNotFound_ShouldReturnNotFound()
        {
            // Arrange
            MockToDoTaskRepository.Setup(r => r.Delete(It.IsAny<int>())).Returns(false);
            var controller = new ToDoTasksController(MockToDoTaskRepository.Object, MockLogger.Object);

            // Act
            var actionResult = controller.Delete(1);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }
    }
}
