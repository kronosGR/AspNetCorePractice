using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;

namespace SportsStore.Controllers
{
    public class HomeController:Controller
    {
        private IStoreRepository repository;

        public HomeController(IStoreRepository repo)
        {
            this.repository = repo;
        }
        public IActionResult Index() => View(repository.Products);
    }
}
