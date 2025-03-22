using EletronicStore.DAO;
using EletronicStore.Facade.Interface;
using EletronicStore.Models;

namespace EletronicStore.Facade {
    public class FacadeEnderecoCliente : FacadeEntidadeDominio, IFacadeGeneric {
        public bool Atualizar(EntidadeDominio entidade) {
            throw new NotImplementedException();
        }

        public bool Delete(EntidadeDominio entidade) {
            _dao = new DAOEnderecoCliente();
            return _dao.Delete(entidade);
        }

        public void Inserir(EntidadeDominio entidade) {
            _dao = new DAOEnderecoCliente();
            _dao.Insert(entidade);
        }

        public List<EntidadeDominio> Selecionar() {
            throw new NotImplementedException();
        }
    }
}
