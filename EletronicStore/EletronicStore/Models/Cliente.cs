namespace EletronicStore.Models {
    public class Cliente {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Genero { get; set; }
        public string CPF { get; set; }
        public DateTime DataNascimento { get; set; }
        public Telefone Telefone { get; set; }
    }
}
