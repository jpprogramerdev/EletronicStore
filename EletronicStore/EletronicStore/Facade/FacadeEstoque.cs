using EletronicStore.DAO;
using EletronicStore.Facade.Interface;
using EletronicStore.Models;

namespace EletronicStore.Facade {
    public class FacadeEstoque : FacadeEntidadeDominio, IFacadeGeneric {
        public bool Atualizar(EntidadeDominio entidade) {
            _dao = new DAOEstoque();
            return _dao.Update(entidade);
        }

        public bool Delete(EntidadeDominio entidade) {
            throw new NotImplementedException();
        }

        public void Inserir(EntidadeDominio entidade) {
            _dao = new DAOEstoque();
            _dao.Insert(entidade);
        }

        public List<EntidadeDominio> Selecionar() {
            throw new NotImplementedException();
        }
    }
}
