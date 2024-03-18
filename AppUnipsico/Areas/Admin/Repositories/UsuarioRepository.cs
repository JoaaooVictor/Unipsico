using AppUnipsico.Data.Context;
using AppUnipsico.Enums;
using AppUnipsico.Models;
using AppUnipsico.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;

namespace AppUnipsico.Areas.Admin.Repositories
{
    public class UsuarioRepository
    {
        private readonly AppUnipsicoDb _context;
        private readonly UserManager<Usuario> _userManager;

        public UsuarioRepository(AppUnipsicoDb context, UserManager<Usuario> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IEnumerable<Usuario>> BuscaTodosUsuarios()
        {
            return await _userManager.Users
                                .OrderBy(u => u.UserName)
                                .ToListAsync();
        }

        public async Task<Usuario> BuscaUsuarioPorId(string usuarioId)
        {
            return await _userManager.FindByIdAsync(usuarioId);
        }

        public async Task<List<Consulta>> BuscaConsultasPorUsuario(Usuario usuario)
        {
            return await _context.Consultas
                        .Where(c => c.UsuarioId == usuario.Id)
                        .Include(c => c.Usuario)
                        .Include(c => c.DataConsulta)
                        .OrderBy(c => c.DataConsulta.Data)
                        .ToListAsync();
        }

        public async Task<IdentityResult> CriaUsuario(RegisterViewModel registerViewModel)
        {
            var tipoUsuario = VerificaTipoUsuario(registerViewModel);

            var usuario = new Usuario
            {
                UserName = registerViewModel.Email,
                NomeCompleto = registerViewModel.NomeCompleto,
                Cpf = registerViewModel.Cpf,
                DataNascimento = registerViewModel.DataNascimento,
                DataRegistro = DateTime.Now,
                Email = registerViewModel.Email,
                TipoUsuario = tipoUsuario,
            };

            return await _userManager.CreateAsync(usuario, registerViewModel.Password);
        }

        public TipoUsuarioEnum VerificaTipoUsuario(RegisterViewModel registerViewModel)
        {
            TipoUsuarioEnum tipoUsuario;

            switch (registerViewModel.SelectedRole)
            {
                case "Aluno":
                    tipoUsuario = TipoUsuarioEnum.Aluno;
                    break;
                case "Professor":
                    tipoUsuario = TipoUsuarioEnum.Professor;
                    break;
                case "Admin":
                    tipoUsuario = TipoUsuarioEnum.Admin;
                    break;
                case "Paciente":
                    tipoUsuario = TipoUsuarioEnum.Paciente;
                    break;
                default: throw new NotImplementedException();
            }

            return tipoUsuario;
        }
    }
}
