using Microsoft.AspNetCore.Mvc;
using Todo.Models;
using Todo.Services;

namespace Todo.Controllers
{
    public class ToDoController : Controller
    {
        private readonly ITodoItemService _todoItemService;

        // must provide an object mathing ITodoITemService interface
        public ToDoController(ITodoItemService todoItemService)
        {
            _todoItemService = todoItemService;
        }

        public async Task<IActionResult> Index()
        {
            var items = await _todoItemService.GetIncompleteItemAsync();
            var model = new ToDoViewModel()
            {
                Items = items
            };

            return View(model);
        }
    }
}
