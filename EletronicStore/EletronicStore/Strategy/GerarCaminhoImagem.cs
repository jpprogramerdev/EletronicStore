using EletronicStore.Models;
using EletronicStore.Strategy.Interface;

namespace EletronicStore.Strategy {
    public class GerarCaminhoImagem : IStrategy {
        public void Executar(EntidadeDominio entidade) {
            Produto Produto = (Produto)entidade;

            if(Produto.Imagem.Length > 0) {
                var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imagens");

                if (!Directory.Exists(uploadPath)) {
                    Directory.CreateDirectory(uploadPath);
                }

                var nomeArquivo = Guid.NewGuid().ToString() + Path.GetExtension(Produto.Imagem.FileName);

                var caminhoArquivos = Path.Combine(uploadPath, nomeArquivo);

                using(var fileStream = new FileStream(caminhoArquivos, FileMode.Create)) {
                    Produto.Imagem.CopyTo(fileStream);
                }

                Produto.URL_Imagem = $"/imagens/{nomeArquivo}";

            }
        }
    }
}
