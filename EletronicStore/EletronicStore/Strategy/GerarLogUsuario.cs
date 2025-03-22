using EletronicStore.Models;
using EletronicStore.Strategy.Interface;

namespace EletronicStore.Strategy {
    public class GerarLogUsuario : IStrategy {
        public void Executar(EntidadeDominio entidade) {
            Usuario Usuario = (Usuario)entidade;

            DateTime dataGeracaoLog = DateTime.Now;

            string path = @$"D:\Arquivos\Projetos\EletronicStore\EletronicStore\EletronicStore\LOGS\ {Usuario.Id}  - {Usuario.Nome} - {dataGeracaoLog.ToString("dd-MM-yyyy HH-mm-ss")}.txt";

            FileInfo fileInfo = new(path);

            FileStream filestream = fileInfo.Create();
            filestream.Close();

            using (StreamWriter sw = fileInfo.CreateText()) {
                sw.WriteLine($"Log Cliente - Id {Usuario.Id}");
                sw.WriteLine($"Data de criação: {dataGeracaoLog.ToString("dd/MM/yyyy")}");
                sw.WriteLine("Dados Cliente:");
                sw.WriteLine($"Nome: {Usuario.Nome}");
                sw.WriteLine($"CPF: {Usuario.CPF}");
                sw.WriteLine($"E-mail: {Usuario.Email}");
                sw.WriteLine($"Genero: {Usuario.Genero}");
                sw.WriteLine($"Data Nascimento: {Usuario.DataNascimento.ToString("dd/MM/yyyy")}");
                sw.WriteLine($"Telefone: ({Usuario.Telefone.DDD}){Usuario.Telefone.Numero} - {Usuario.Telefone.TipoTelefone.Tipo}");
                sw.WriteLine($"\n\n-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------\n\n");

                sw.WriteLine("Dados Enderecos:");
                foreach (Endereco Endereco in Usuario.Enderecos) {
                    sw.WriteLine($"Apelido: {Endereco.Apelido}");
                    sw.WriteLine($"CEP: {Endereco.CEP}");
                    sw.WriteLine($"Endereços: {Endereco.TipoLogradouro.Tipo} {Endereco.Logradouro}, {Endereco.Numero}");
                    sw.WriteLine($"Bairro: {Endereco.Bairro}");
                    sw.WriteLine($"Municipio: {Endereco.Municipio.Nome}");
                    sw.WriteLine($"Estado: {Endereco.Municipio.Estado.Nome}");
                    sw.WriteLine($"Pais: {Endereco.Municipio.Estado.Pais.Nome}");
                    sw.WriteLine("Observacao:" + (string.IsNullOrEmpty(Endereco.Observacao) ? "N/A" : Endereco.Observacao));
                    sw.WriteLine($"Tipo Residencia: {Endereco.TipoResidencia.Tipo}");


                    string enderecoEntregaCobranca = "";

                    if (Endereco.Cobranca) {
                        enderecoEntregaCobranca = "Cobrança";
                        if (Endereco.Entrega) {
                            enderecoEntregaCobranca += " Entrega";

                        }
                        sw.WriteLine("Endereco de :" + enderecoEntregaCobranca);
                    }
                    sw.WriteLine("\n");

                }
                sw.WriteLine($"\n-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------\n\n");


                foreach(Cartao Cartao in Usuario.Cartoes) {
                    sw.WriteLine($"Apelido: {Cartao.Apelido}");
                    sw.WriteLine($"Numero: {Cartao.Numero}");
                    sw.WriteLine($"CVV: {Cartao.CVV}");
                    sw.WriteLine($"Data Validade: {Cartao.DataValidade}");
                    sw.WriteLine($"Função: {Cartao.Funcao}");
                    sw.WriteLine($"Bandeira: {Cartao.Bandeira.Nome}");
                    sw.WriteLine("\n");
                    sw.WriteLine($"\n-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------\n\n");
                }
            }
        }
    }
}
