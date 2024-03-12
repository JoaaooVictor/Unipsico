using AppUnipsico.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace AppUnipsico.Services.Impl
{
    public class CriaUsuarioRoleInicialService : ICriaUsuarioRoleInicialService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public CriaUsuarioRoleInicialService(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public void CriaRoles()
        {
            if (!_roleManager.RoleExistsAsync("Paciente").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Paciente";
                role.NormalizedName = "PACIENTE";
                IdentityResult roleResult = _roleManager.CreateAsync(role).Result;
            }

            if (!_roleManager.RoleExistsAsync("Professor").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Professor";
                role.NormalizedName = "PROFESSOR";
                IdentityResult roleResult = _roleManager.CreateAsync(role).Result;
            }

            if (!_roleManager.RoleExistsAsync("Aluno").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Aluno";
                role.NormalizedName = "ALUNO";
                IdentityResult roleResult = _roleManager.CreateAsync(role).Result;
            }

            if (!_roleManager.RoleExistsAsync("Admin").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Admin";
                role.NormalizedName = "ADMIN";
                IdentityResult roleResult = _roleManager.CreateAsync(role).Result;
            }
        }

        public void CriaUsuarios()
        {
            if (_userManager.FindByEmailAsync("paciente@localhost").Result == null)
            {
                IdentityUser user = new IdentityUser();

                user.UserName = "paciente@localhost";
                user.Email = "paciente@localhost";
                user.NormalizedUserName = "PACIENTE@LOCALHOST";
                user.EmailConfirmed = true;
                user.LockoutEnabled = false;
                user.SecurityStamp = Guid.NewGuid().ToString();

                IdentityResult result = _userManager.CreateAsync(user, "Paciente123#").Result;

                if (result.Succeeded)
                {
                    _userManager.AddToRoleAsync(user, "Paciente").Wait();
                }
            }

            if (_userManager.FindByEmailAsync("professor@localhost").Result == null)
            {
                IdentityUser user = new IdentityUser();

                user.UserName = "professor@localhost";
                user.Email = "professor@localhost";
                user.NormalizedUserName = "PROFESSOR@LOCALHOST";
                user.EmailConfirmed = true;
                user.LockoutEnabled = false;
                user.SecurityStamp = Guid.NewGuid().ToString();

                IdentityResult result = _userManager.CreateAsync(user, "Professor123#").Result;

                if (result.Succeeded)
                {
                    _userManager.AddToRoleAsync(user, "Professor").Wait();
                }
            }

            if (_userManager.FindByEmailAsync("aluno@localhost").Result == null)
            {
                IdentityUser user = new IdentityUser();

                user.UserName = "aluno@localhost";
                user.Email = "aluno@localhost";
                user.NormalizedUserName = "ALUNO@LOCALHOST";
                user.EmailConfirmed = true;
                user.LockoutEnabled = false;
                user.SecurityStamp = Guid.NewGuid().ToString();

                IdentityResult result = _userManager.CreateAsync(user, "Aluno123#").Result;

                if (result.Succeeded)
                {
                    _userManager.AddToRoleAsync(user, "Aluno").Wait();
                }
            }

            if (_userManager.FindByEmailAsync("admin@localhost").Result == null)
            {
                IdentityUser user = new IdentityUser();

                user.UserName = "admin@localhost";
                user.Email = "admin@localhost";
                user.NormalizedUserName = "ADMIN@LOCALHOST";
                user.EmailConfirmed = true;
                user.LockoutEnabled = false;
                user.SecurityStamp = Guid.NewGuid().ToString();

                IdentityResult result = _userManager.CreateAsync(user, "Admin123#").Result;

                if (result.Succeeded)
                {
                    _userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }
        }
    }
}
