using Todo.Models;

namespace Todo.Services
{
    public interface ITodoItemService
    {
        Task<ToDoItem[]> GetIncompleteItemAsync();
    }
}
