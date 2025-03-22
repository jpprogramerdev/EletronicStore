using EletronicStore.Models;

namespace EletronicStore.ViewModel {
    public class FormCadCliente : EntidadeDominio{
        public FormCadCliente() {
            Cliente = new();
            Endereco = new();
            Cartao = new();
        }
        public Usuario Cliente { get; set; }
        public Endereco Endereco { get; set; }
        public Cartao Cartao { get; set; }
    }
}
