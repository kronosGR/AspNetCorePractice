using Todo.Models;

namespace Todo.Services
{
    public interface ITodoItemService
    {
        Task<ToDoItem[]> GetIncompleteItemAsync();
        Task<bool> AddItemAsync(ToDoItem newItem);

        Task<bool> MarkDoneAsync(Guid id);
    }
}
