using CoreApplication.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApplication.Infrastructure
{
    public static class MenuItems
    {
        static List<RoleAccessViewModel> roleAccess = new List<RoleAccessViewModel>
            {
                new RoleAccessViewModel { ControllerName = "Book", ActionName = "BooksList", Title = "Библиотека" },
                new RoleAccessViewModel { ControllerName = "Order", ActionName = "UserOrderList", Title = "Мои книги" },
                new RoleAccessViewModel { ControllerName = "Book", ActionName = "BooksEditPage", Title = "Редактирование" },
                new RoleAccessViewModel { ControllerName = "Order", ActionName = "LibrarianList", Title = "Прием / Выдача" }
            };

        public static List<RoleAccessViewModel> GetMenuByTitles(IEnumerable<string> titles)
        {
            List<RoleAccessViewModel> menu = new List<RoleAccessViewModel>();
            foreach(var t in titles)
            {
                var item = roleAccess.Where(a => a.Title.Equals(t)).FirstOrDefault();
                if (item != null)
                {
                    menu.Add(item);
                }
            }
            return menu;
        }
    }
}
