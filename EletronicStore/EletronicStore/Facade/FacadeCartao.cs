using EletronicStore.DAO;
using EletronicStore.DAO.Interface;
using EletronicStore.Facade.Interface;
using EletronicStore.Models;

namespace EletronicStore.Facade {
    public class FacadeCartao : FacadeEntidadeDominio, IFacadeGeneric {
        public bool Atualizar(EntidadeDominio entidade) {
            throw new NotImplementedException();
        }

        public bool Delete(EntidadeDominio entidade) {
            _dao = new DAOCartao();
            return _dao.Delete(entidade);
        }

        public void Inserir(EntidadeDominio entidade) {
            _dao = new DAOCartao();
            _dao.Insert(entidade);
        }

        public List<EntidadeDominio> Selecionar() {
            _dao = new DAOCartao();
            return _dao.Select();
        }
    }
}
