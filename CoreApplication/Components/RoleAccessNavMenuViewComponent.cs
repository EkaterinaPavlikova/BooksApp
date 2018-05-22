using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreApplication.Models;
using CoreApplication.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using CoreApplication.Infrastructure;

namespace CoreApplication.Components
{
    public class RoleAccessNavMenuViewComponent : ViewComponent
    {
        private UserManager<UserIdentity> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private IAccountRepository repository;

        public RoleAccessNavMenuViewComponent(IAccountRepository repo, UserManager<UserIdentity> userManager, RoleManager<IdentityRole> roleManager)
        {
            repository = repo;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<string> titles = new List<string>();
            if (User.Identity.IsAuthenticated)
            {
                var currentUserName = User.Identity.Name;
                var user = await _userManager.FindByNameAsync(currentUserName);
                var rolesNames = await _userManager.GetRolesAsync(user);

                foreach (var roleName in rolesNames)
                {
                    var role = await _roleManager.FindByNameAsync(roleName);

                    var roleIdentityAccess = repository.getUserMenu(role.Id);
                    foreach (var access in roleIdentityAccess)
                    {
                        var title = access.ApplicationMenu.ApplicationMenuTitle;
                        if (!titles.Contains(title))
                            titles.Add(title);
                    }

                }
            }

            return View(MenuItems.GetMenuByTitles(titles));
        }
    }
}
