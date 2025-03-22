namespace EletronicStore.Exceptions {
    public class PadraoSenhaException : Exception{
        public PadraoSenhaException() : base("A senha deve conter no mínimos 8 caracteres, uma letra maiúscula, uma minúscula e um caractere especial") { }
    }
}
