using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Todo.Models;
using Todo.Services;

namespace Todo.Controllers
{
    [Authorize]
    public class ToDoController : Controller
    {
        private readonly ITodoItemService _todoItemService;
        private readonly UserManager<ApplicationUser> _userManager;

        // must provide an object mathing ITodoITemService interface
        public ToDoController(ITodoItemService todoItemService, UserManager<ApplicationUser> userManager)
        {
            _todoItemService = todoItemService;
        }

        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();


            var items = await _todoItemService.GetIncompleteItemAsync(currentUser);
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

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MarkDone(Guid id)
        {
            if (id == Guid.Empty) return RedirectToAction("Index");

            var successful = await _todoItemService.MarkDoneAsync(id);
            if (!successful) return BadRequest("Could not mark item as done");

            return RedirectToAction("Index");
        }


    }


}
