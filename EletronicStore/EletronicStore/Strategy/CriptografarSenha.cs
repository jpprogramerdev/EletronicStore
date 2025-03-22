using EletronicStore.Models;
using EletronicStore.Strategy.Interface;
using System.Security.Cryptography;
using System.Text;

namespace EletronicStore.Strategy {
    public class CriptografarSenha : IStrategy {
        public void Executar(EntidadeDominio entidade) {
            Usuario Cliente = (Usuario)entidade;

            var hash = SHA256.Create();
            var codificacao = new ASCIIEncoding();
            var array = codificacao.GetBytes(Cliente.Senha);

            array = hash.ComputeHash(array);

            var stringSenha = new StringBuilder();
            
            foreach(var c in array) {
                stringSenha.Append(c.ToString("x2"));
            }

            Cliente.Senha = stringSenha.ToString();
        }
    }
}
