using AppUnipsico.Data.Context;
using AppUnipsico.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AppUnipsico.Areas.Admin.Repositories
{
    public class AlunoRepository
    {
        private readonly UserManager<Usuario> _userManager;

        public AlunoRepository(UserManager<Usuario> userManager)
        {
            _userManager = userManager;
        }

        public IEnumerable<Usuario> BuscaTodosAlunos()
        {
            return _userManager.Users.Where(c => c.TipoUsuario == Enums.TipoUsuarioEnum.Aluno);
        }
    }
}
