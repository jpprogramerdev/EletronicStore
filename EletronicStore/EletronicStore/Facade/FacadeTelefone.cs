using EletronicStore.DAO;
using EletronicStore.Facade.Interface;
using EletronicStore.Models;

namespace EletronicStore.Facade {
    public class FacadeTelefone : FacadeEntidadeDominio, IFacadeGeneric {
        public bool Atualizar(EntidadeDominio entidade) {
            throw new NotImplementedException();
        }

        public bool Delete(EntidadeDominio entidade) {
            throw new NotImplementedException();
        }

        public void Inserir(EntidadeDominio entidade) {
            _dao = new DAOTelefone();
            _dao.Insert(entidade);
        }

        public List<EntidadeDominio> Selecionar() {
            throw new NotImplementedException();
        }
    }
}
