namespace EletronicStore.Models {
    public class Endereco : EntidadeDominio {
        public TipoResidencia TipoResidencia { get; set; }
        public TipoLogradouro TipoLogradouro { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string CEP { get; set; }
        public Municipio Municipio { get; set; }
        public string Bairro { get; set; }
        public string Observacao { get; set; }
        public string Apelido { get; set; }
        public bool Cobranca { get; set; }
        public bool Entrega { get; set; }
    }
}
