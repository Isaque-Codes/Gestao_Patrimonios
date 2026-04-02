using Gestao_Patrimonios.Contexts;
using Gestao_Patrimonios.Domains;
using Gestao_Patrimonios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Gestao_Patrimonios.Repositories
{
    public class EnderecoRepository : IEnderecoRepository
    {
        private readonly Gestao_PatrimoniosContext _context;

        public EnderecoRepository(Gestao_PatrimoniosContext context)
        {
            _context = context;
        }

        public List<Endereco> Listar()
        {
            return _context.Endereco.AsNoTracking()
                .OrderBy(e => e.Logradouro)
                .ToList();
        }

        public Endereco BuscarPorId(Guid enderecoId)
        {
            return _context.Endereco.AsNoTracking()
                .FirstOrDefault(e => e.EnderecoID == enderecoId);
        }

        public Endereco BuscarPorLogradouroENumero(string logradouro, int? numero, Guid bairroId, Guid? enderecoId = null)
        {
            var consulta = _context.Endereco.AsNoTracking();

            if (enderecoId.HasValue)
            {
                consulta = consulta.Where(e => e.EnderecoID != enderecoId.Value);
            }

            return consulta.FirstOrDefault(e =>
                e.Logradouro == logradouro &&
                e.Numero == numero &&
                e.BairroID == bairroId);
        }

        public bool BairroExistente(Guid bairroId)
        {
            return _context.Bairro.Any(b => b.BairroID == bairroId);
        }

        public void Adicionar(Endereco endereco)
        {
            _context.Endereco.Add(endereco);

            _context.SaveChanges();
        }

        public void Atualizar(Endereco endereco)
        {
            if (endereco == null)
            {
                return;
            }

            Endereco enderecoBanco = _context.Endereco.Find(endereco.EnderecoID);

            if (enderecoBanco == null)
            {
                return;
            }

            enderecoBanco.Logradouro = endereco.Logradouro;
            enderecoBanco.Numero = endereco.Numero;
            enderecoBanco.Complemento = endereco.Complemento;
            enderecoBanco.CEP = endereco.CEP;
            enderecoBanco.BairroID = endereco.BairroID;

            _context.SaveChanges();
        }
    }
}