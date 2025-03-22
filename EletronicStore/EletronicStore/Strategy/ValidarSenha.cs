using EletronicStore.Exceptions;
using EletronicStore.Models;
using EletronicStore.Strategy.Interface;

namespace EletronicStore.Strategy {
    public class ValidarSenha : IStrategy {
        public void Executar(EntidadeDominio entidade) {
            Usuario Cliente = (Usuario)entidade;

            if(!(Cliente.Senha.Any(char.IsUpper) && 
               Cliente.Senha.Any(char.IsLower) && 
               Cliente.Senha.Any(c => !char.IsLetterOrDigit(c)) && 
               (Cliente.Senha.Length > 8))){
                throw new PadraoSenhaException();
            }
        }
    }
}
