using EletronicStore.Models;

namespace EletronicStore.Strategy.Interface {
    public interface IStrategy {
        public void Executar(EntidadeDominio entidade);
    }
}
