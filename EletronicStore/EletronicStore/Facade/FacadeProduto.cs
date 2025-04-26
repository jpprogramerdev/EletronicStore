using EletronicStore.DAO;
using EletronicStore.Facade.Interface;
using EletronicStore.Models;

namespace EletronicStore.Facade {
    public class FacadeProduto : FacadeEntidadeDominio, IFacadeGeneric {
        public bool Atualizar(EntidadeDominio entidade) {
            _dao = new DAOProduto();
            return _dao.Update(entidade);
        }

        public bool Delete(EntidadeDominio entidade) {
            throw new NotImplementedException();
        }

        public void Inserir(EntidadeDominio entidade) {
            _dao = new DAOProduto();
            _dao.Insert(entidade);
        }

        public List<EntidadeDominio> Selecionar() {
            _dao = new DAOProduto();
            return _dao.Select();
        }
    }
}
