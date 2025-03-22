namespace EletronicStore.Models {
    public class Cartao : EntidadeDominio {
        public string Numero { get; set; }
        public string DataValidade { get; set; }
        public string CVV {  get; set; }
        public BandeiraCartao Bandeira { get; set; }
        public string Funcao { get; set; }
        public string Apelido { get; set; }
    }
}
