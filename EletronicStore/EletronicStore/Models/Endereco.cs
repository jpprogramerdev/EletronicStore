namespace EletronicStore.Models {
    public class Endereco {
        public TipoResidencia TipoResidencia { get; set; }
        public TipoLogradouro TipoLogradouro { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string CEP { get; set; }
        public Cidade Cidade { get; set; }
        public string Observacao { get; set; }
    }
}
