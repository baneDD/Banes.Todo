using System;

namespace Banes.ToDo.Models
{
    public class ToDoTask
    {
        public int Id { get; set; }
        public DateTime TaskCreationTime { get; set; }
        public string TaskDescription { get; set; }
        public bool TaskCompleted { get; set; }
    }
}