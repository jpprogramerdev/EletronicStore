using EletronicStore.DAO;
using EletronicStore.Facade.Interface;
using EletronicStore.Models;

namespace EletronicStore.Facade {
    public class FacadeCupom : FacadeEntidadeDominio, IFacadeGeneric {
        public bool Atualizar(EntidadeDominio entidade) {
            throw new NotImplementedException();
        }

        public bool Delete(EntidadeDominio entidade) {
            _dao = new DAOCupom();
            return _dao.Delete(entidade);
        }

        public void Inserir(EntidadeDominio entidade) {
            _dao = new DAOCupom();
            _dao.Insert(entidade);
        }

        public List<EntidadeDominio> Selecionar() {
            _dao = new DAOCupom();
            return _dao.Select();
        }
    }
}
