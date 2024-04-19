using AppUnipsico.Data.Context;
using AppUnipsico.Enums;
using AppUnipsico.Models;
using AppUnipsico.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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

        public async Task<IdentityResult> CriaUsuario(RegistraUsuarioViewModel registraUsuarioViewModel)
        {
            var tipoUsuario = VerificaTipoUsuario(registraUsuarioViewModel);

            var usuario = new Usuario
            {
                UserName = registraUsuarioViewModel.Email,
                NomeCompleto = registraUsuarioViewModel.NomeCompleto,
                Cpf = registraUsuarioViewModel.Cpf,
                DataNascimento = registraUsuarioViewModel.DataNascimento,
                DataRegistro = DateTime.Now,
                Email = registraUsuarioViewModel.Email,
                TipoUsuario = tipoUsuario,
            };

            if (registraUsuarioViewModel.SelectedRole == "Aluno")
            {
                usuario.RA = registraUsuarioViewModel.RA;
            }

            var usuarioCriado = await _userManager.CreateAsync(usuario, registraUsuarioViewModel.Password);

            if (usuarioCriado.Succeeded)
            {
                await AdicionaRoleUsuario(tipoUsuario, usuario);
            }

            return usuarioCriado;
        }

        public TipoUsuarioEnum VerificaTipoUsuario(RegistraUsuarioViewModel registraUsuarioViewModel)
        {
            TipoUsuarioEnum tipoUsuario;

            switch (registraUsuarioViewModel.SelectedRole)
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

        public async Task AdicionaRoleUsuario(TipoUsuarioEnum tipoUsuario, Usuario usuario)
        {
            switch (tipoUsuario)
            {
                case TipoUsuarioEnum.Admin:
                    await _userManager.AddToRoleAsync(usuario, "Admin");
                    break;
                case TipoUsuarioEnum.Aluno:
                    await _userManager.AddToRoleAsync(usuario, "Aluno");
                    break;
                case TipoUsuarioEnum.Professor:
                    await _userManager.AddToRoleAsync(usuario, "Professor");
                    break;
                case TipoUsuarioEnum.Paciente:
                    await _userManager.AddToRoleAsync(usuario, "Paciente");
                    break;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
