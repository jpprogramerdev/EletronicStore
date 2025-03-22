using EletronicStore.Models;

namespace EletronicStore.Facade.Interface {
    public interface IFacadeGeneric {
        public void Inserir(EntidadeDominio entidade);
        public List<EntidadeDominio> Selecionar();
        public bool Atualizar(EntidadeDominio entidade);
        public bool Delete(EntidadeDominio entidade);
    }
}
