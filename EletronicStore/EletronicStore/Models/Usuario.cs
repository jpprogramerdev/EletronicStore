namespace EletronicStore.Models {
    public class Usuario : EntidadeDominio {
        public Usuario() {
            Cartoes = new();
            Enderecos = new();
        }

        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Genero { get; set; }
        public string CPF { get; set; }
        public bool Status { get; set; }
        public DateTime DataNascimento { get; set; }
        public Telefone Telefone { get; set; }
        public List<Cartao> Cartoes { get; set; }
        public List<Endereco> Enderecos { get; set; }
        public TipoUsuario TipoUsuario { get; set; }
    }
}
