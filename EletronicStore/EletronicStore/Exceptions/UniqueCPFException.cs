namespace EletronicStore.Exceptions {
    public class UniqueCPFException : Exception{
        public UniqueCPFException() : base("CPF já cadastrado"){
            
        }
    }
}
