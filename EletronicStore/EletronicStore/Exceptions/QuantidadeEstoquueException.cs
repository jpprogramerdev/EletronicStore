namespace EletronicStore.Exceptions {
    public class QuantidadeEstoquueException : Exception{
        public QuantidadeEstoquueException() : base("Quantidade solicitado superior a quantidade do estoque"){
            
        }
    }
}
