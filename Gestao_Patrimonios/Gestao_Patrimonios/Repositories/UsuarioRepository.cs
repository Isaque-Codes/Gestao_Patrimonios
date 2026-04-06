using Gestao_Patrimonios.Contexts;
using Gestao_Patrimonios.Domains;
using Gestao_Patrimonios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Gestao_Patrimonios.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly Gestao_PatrimoniosContext _context;

        public UsuarioRepository(Gestao_PatrimoniosContext context)
        {
            _context = context;
        }

        public List<Usuario> Listar()
        {
            return _context.Usuario.AsNoTracking()
                .OrderBy(u => u.Nome).ToList();
        }

        public Usuario BuscarPorId(Guid usuarioId)
        {
            return _context.Usuario.AsNoTracking()
                .FirstOrDefault(u => u.UsuarioID == usuarioId);
        }

        public Usuario BuscarPorNIFComTipoUsuario(string nif)
        {
            return _context.Usuario.AsNoTracking()
                .Include(u => u.TipoUsuario)
                .FirstOrDefault(u => u.NIF == nif);
        }

        public Usuario BuscarDuplicata(string nif, string cpf, string email, Guid? usuarioId = null)
        {
            var consulta = _context.Usuario.AsQueryable();

            if (usuarioId.HasValue)
            {
                consulta = consulta.Where(u => u.UsuarioID != usuarioId.Value);
            }

            return consulta.AsNoTracking().FirstOrDefault(u =>
                   u.NIF == nif
                || u.CPF == cpf
                || u.Email.ToLower() == email.ToLower());
        }

        public bool EnderecoExistente(Guid enderecoId)
        {
            return _context.Endereco.Any(e => e.EnderecoID == enderecoId);
        }

        public bool CargoExistente(Guid cargoId)
        {
            return _context.Cargo.Any(c => c.CargoID == cargoId);
        }

        public bool TipoUsuarioExistente(Guid tipoUsuarioId)
        {
            return _context.TipoUsuario.Any(t => t.TipoUsuarioID == tipoUsuarioId);
        }

        public void Adicionar(Usuario usuario)
        {
            _context.Usuario.Add(usuario);
            _context.SaveChanges();
        }

        public void Atualizar(Usuario usuario)
        {
            if (usuario == null)
            {
                return;
            }

            Usuario usuarioBanco = _context.Usuario.Find(usuario.UsuarioID);

            if (usuarioBanco == null)
            {
                return;
            }

            usuarioBanco.NIF = usuario.NIF;
            usuarioBanco.Nome = usuario.Nome;
            usuarioBanco.RG = usuario.RG;
            usuarioBanco.CPF = usuario.CPF;
            usuarioBanco.CarteiraTrabalho = usuario.CarteiraTrabalho;
            usuarioBanco.Email = usuario.Email;
            usuarioBanco.EnderecoID = usuario.EnderecoID;
            usuarioBanco.CargoID = usuario.CargoID;
            usuarioBanco.TipoUsuarioID = usuario.TipoUsuarioID;

            _context.SaveChanges();
        }

        public void AtualizarSenha(Usuario usuario)
        {
            if (usuario == null)
            {
                return;
            }

            Usuario usuarioBanco = _context.Usuario.Find(usuario.UsuarioID);

            if (usuarioBanco == null)
            {
                return;
            }

            usuarioBanco.Senha = usuario.Senha;

            _context.SaveChanges();
        }

        public void AtualizarStatus(Usuario usuario)
        {
            Usuario usuarioBanco = _context.Usuario.Find(usuario.UsuarioID);

            if (usuarioBanco == null)
            {
                return;
            }

            usuarioBanco.Ativo = usuario.Ativo;

            _context.SaveChanges();
        }

        public void AtualizarPrimeiroAcesso(Usuario usuario)
        {
            if (usuario == null)
            {
                return;
            }

            Usuario usuarioBanco = _context.Usuario.Find(usuario.UsuarioID);

            if (usuarioBanco == null)
            {
                return;
            }

            usuarioBanco.PrimeiroAcesso = usuario.PrimeiroAcesso;
            _context.SaveChanges();
        }
    }
}