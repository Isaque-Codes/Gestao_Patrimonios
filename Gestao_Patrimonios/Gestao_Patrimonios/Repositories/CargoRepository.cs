using Gestao_Patrimonios.Contexts;
using Gestao_Patrimonios.Domains;
using Gestao_Patrimonios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Gestao_Patrimonios.Repositories
{
    public class CargoRepository : ICargoRepository
    {
        private readonly Gestao_PatrimoniosContext _context;

        public CargoRepository(Gestao_PatrimoniosContext context)
        {
            _context = context;
        }

        public List<Cargo> Listar()
        {
            return _context.Cargo.AsNoTracking().ToList();
        }

        public Cargo BuscarPorId(Guid cargoId)
        {
            return _context.Cargo.AsNoTracking()
                .FirstOrDefault(c => c.CargoID == cargoId);
        }

        public Cargo BuscarPorNome(string nomeCargo)
        {
            return _context.Cargo.AsNoTracking()
                .FirstOrDefault(c => c.NomeCargo == nomeCargo);
        }

        public void Adicionar(Cargo cargo)
        {
            _context.Cargo.Add(cargo);

            _context.SaveChanges();
        }

        public void Atualizar(Cargo cargo)
        {
            if (cargo == null)
            {
                return;
            }

            Cargo cargoBanco = _context.Cargo.FirstOrDefault(c => c.CargoID == cargo.CargoID);

            if (cargoBanco == null)
            {
                return;
            }

            cargoBanco.NomeCargo = cargo.NomeCargo;

            _context.SaveChanges();
        }
    }
}