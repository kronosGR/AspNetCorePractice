using System.ComponentModel.DataAnnotations;

namespace Todo.Models
{
    public class ToDoItem
    {

        public Guid Id { get; set; }
        public bool IsDone { get; set; }

        [Required] 
        public string Title { get; set; }

        public DateTimeOffset? DueAt { get; set; }

    }
}
