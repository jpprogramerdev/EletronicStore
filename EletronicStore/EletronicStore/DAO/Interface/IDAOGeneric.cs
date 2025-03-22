using EletronicStore.Models;

namespace EletronicStore.DAO.Interface {
    public interface IDAOGeneric {
        public void Insert(EntidadeDominio entidade);
        public List<EntidadeDominio> Select();
        public bool Update(EntidadeDominio entidade);
        public bool Delete(EntidadeDominio entidade);
    }
}
