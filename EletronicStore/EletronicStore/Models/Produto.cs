namespace EletronicStore.Models {
    public class Produto : EntidadeDominio{
        public string Nome { get; set; }
        public double Preco { get; set; }
        public string Descricao { get; set; }
        public bool Status { get; set; }
        public string URL_Imagem { get; set; }
        public Marca Marca { get; set; }
        public TipoProduto TipoProduto { get; set; }
        public GrupoPrecificacao GrupoPrecificacao { get; set; }
        public Fornecedor Fornecedor { get; set; }
        public int QuantidadeEstoque { get; set; }
        public IFormFile Imagem { get; set; }
    }
}
