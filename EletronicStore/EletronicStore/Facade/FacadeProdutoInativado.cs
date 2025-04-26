using EletronicStore.DAO;
using EletronicStore.Facade.Interface;
using EletronicStore.Models;

namespace EletronicStore.Facade {
    public class FacadeProdutoInativado : FacadeEntidadeDominio, IFacadeGeneric {
        public bool Atualizar(EntidadeDominio entidade) {
            throw new NotImplementedException();
        }

        public bool Delete(EntidadeDominio entidade) {
            _dao = new DAOProdutoInativado();
            return _dao.Delete(entidade);
        }

        public void Inserir(EntidadeDominio entidade) {
            _dao = new DAOProdutoInativado();
            _dao.Insert(entidade);
        }

        public List<EntidadeDominio> Selecionar() {
            throw new NotImplementedException();
        }
    }
}
