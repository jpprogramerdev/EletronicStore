namespace EletronicStore.Models {
    public class Cupom : EntidadeDominio {
        public string Nome { get; set; }
        public double Desconto { get; set; }
        public Usuario? Usuario { get; set; }
    }
}
