﻿using AppUnipsico.Enums;
using AppUnipsico.Models;
using AppUnipsico.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace AppUnipsico.Services.Impl
{
    public class CriaUsuarioRoleInicialService : ICriaUsuarioRoleInicialService
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public CriaUsuarioRoleInicialService(RoleManager<IdentityRole> roleManager, UserManager<Usuario> userManager)
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
                Usuario user = new Usuario();

                user.NomeCompleto = "Paciente TESTE";
                user.UserName = "paciente@localhost";
                user.Email = "paciente@localhost";
                user.NormalizedUserName = "PACIENTE@LOCALHOST";
                user.Cpf = "11111111111";
                user.EmailConfirmed = true;
                user.LockoutEnabled = false;
                user.DataRegistro = DateTime.Now;
                user.TipoUsuario = TipoUsuarioEnum.Paciente;
                user.SecurityStamp = Guid.NewGuid().ToString();

                IdentityResult result = _userManager.CreateAsync(user, "123#Paciente").Result;

                if (result.Succeeded)
                {
                    _userManager.AddToRoleAsync(user, "Paciente").Wait();
                }
            }

            if (_userManager.FindByEmailAsync("professor@localhost").Result == null)
            {
                Usuario user = new Usuario();

                user.NomeCompleto = "Professor TESTE";
                user.UserName = "professor@localhost";
                user.Email = "professor@localhost";
                user.NormalizedUserName = "PROFESSOR@LOCALHOST";
                user.Cpf = "22222222222";
                user.EmailConfirmed = true;
                user.LockoutEnabled = false;
                user.DataRegistro = DateTime.Now;
                user.TipoUsuario = TipoUsuarioEnum.Professor;
                user.SecurityStamp = Guid.NewGuid().ToString();

                IdentityResult result = _userManager.CreateAsync(user, "123#Professor").Result;

                if (result.Succeeded)
                {
                    _userManager.AddToRoleAsync(user, "Professor").Wait();
                }
            }

            if (_userManager.FindByEmailAsync("aluno@localhost").Result == null)
            {
                Usuario user = new Usuario();

                user.NomeCompleto = "Aluno TESTE";
                user.UserName = "aluno@localhost";
                user.Email = "aluno@localhost";
                user.NormalizedUserName = "ALUNO@LOCALHOST";
                user.Cpf = "33333333333";
                user.EmailConfirmed = true;
                user.RA = "6320031";
                user.LockoutEnabled = false;
                user.DataRegistro = DateTime.Now;
                user.TipoUsuario = TipoUsuarioEnum.Aluno;
                user.SecurityStamp = Guid.NewGuid().ToString();

                IdentityResult result = _userManager.CreateAsync(user, "123#Aluno").Result;

                if (result.Succeeded)
                {
                    _userManager.AddToRoleAsync(user, "Aluno").Wait();
                }
            }

            if (_userManager.FindByEmailAsync("admin@localhost").Result == null)
            {
                Usuario user = new Usuario();

                user.NomeCompleto = "Admin TESTE";
                user.UserName = "admin@localhost";
                user.Email = "admin@localhost";
                user.NormalizedUserName = "ADMIN@LOCALHOST";
                user.Cpf = "55555555555";
                user.EmailConfirmed = true;
                user.LockoutEnabled = false;
                user.DataRegistro = DateTime.Now;
                user.TipoUsuario = TipoUsuarioEnum.Admin;
                user.SecurityStamp = Guid.NewGuid().ToString();

                IdentityResult result = _userManager.CreateAsync(user, "123#Admin").Result;

                if (result.Succeeded)
                {
                    _userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }
        }
    }
}
