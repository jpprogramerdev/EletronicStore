namespace EletronicStore.Models {
    public class Cartao {
        public string Numero { get; set; }
        public string DataValidade { get; set; }
        public string CVV {  get; set; }
        public BandeiraCartao Bandeira { get; set; }
    }
}
