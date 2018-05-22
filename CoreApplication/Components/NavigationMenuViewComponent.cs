using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreApplication.Models;


namespace CoreApplication.Components
{
    public class NavigationMenuViewComponent: ViewComponent
    {
        private IGenreRepository repository;

        public NavigationMenuViewComponent(IGenreRepository repo)
        {
            repository = repo;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedGenre = RouteData?.Values["genre"];
            return View(repository.Genres
                .Distinct()
                .OrderBy(b=>b.GenreName).AsEnumerable());
        }
    }
}
