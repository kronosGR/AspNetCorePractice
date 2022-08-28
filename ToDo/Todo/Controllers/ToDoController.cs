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

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddItem(ToDoItem newItem)
        {
            if (!ModelState.IsValid) return RedirectToAction("Index");

            var successful = await _todoItemService.AddItemAsync(newItem);
            if (!successful) return BadRequest("Could not add item");

            return RedirectToAction("Index");
        }
    }
}
