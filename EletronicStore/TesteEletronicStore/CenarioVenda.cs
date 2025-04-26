using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesteEletronicStore {
    public class CenarioVenda {
        public IWebDriver driver;

        public CenarioVenda() {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--disable-blink-features=AutomationControlled"); // Oculta a automação
            options.AddExcludedArgument("enable-automation"); // Remove a flag de automação
            options.AddArgument("--start-maximized"); // Abre maximizado (opcional)

            driver = new ChromeDriver(options);

            driver.Navigate().GoToUrl("https://localhost:7055");


            Aguardar3Segundos();
            SelecionarProduto();
            AdicionarProdutoCarrinho();
            Aguardar3Segundos();
            SelecionarProduto();
            AdicionarProdutoCarrinho();
            Aguardar3Segundos();
            ClicarCarrinho();
            Aguardar3Segundos();
            ExcluirItem();
            Aguardar3Segundos();
            SelecionarProdutosCarrinho();
            Aguardar3Segundos();
            CriarNovoEndereco();
            AdicionarCupom();
            FinalizarCompra();

        }

        public void ExcluirItem() {
            var produtos = driver.FindElements(By.Id("excluir-item"));
            if (produtos.Count != 1) {
                var produtoSelecionado = produtos.ElementAt(new Random().Next(produtos.Count));
                produtoSelecionado.Click();
                Aguardar3Segundos();
            }
        }

        public void ClicarCarrinho() {
            var linkCarriho = driver.FindElement(By.Id("link-carrinho"));
            linkCarriho.Click();
        }

        public void SelecionarProdutosCarrinho() {
            var produtos = driver.FindElements(By.ClassName("checkbox-item"));

            RolarMetadeDaPagina();
            if (produtos.Count != 1) {
                Random rando = new Random();
                int quantidade = rando.Next(1, produtos.Count);

                for (int i = 0; i <= quantidade; i++) {
                    var produtoSelecionado = produtos.ElementAt(new Random().Next(produtos.Count));
                    produtoSelecionado.Click();
                    Aguardar3Segundos();
                }
            } else {
                var produtoSelecionado = produtos.ElementAt(new Random().Next(produtos.Count));
                produtoSelecionado.Click();
                Aguardar3Segundos();
            }
        }
        public void FinalizarCompra() {
            RolarPaginaParaFinal();
            Aguardar3Segundos();

            var btnFinalizarCompra = driver.FindElement(By.Id("btnConfirmarCompra"));
            btnFinalizarCompra.Click();

            Aguardar3Segundos();

            // Verifica se houve erro na finalização
            bool houveErro = false;

            try {
                var erro = driver.FindElement(By.ClassName("alert-danger"));
                if (erro.Text.Contains("Erro") || erro.Text.Contains("Falha") || erro.Text.Contains("pedido")) {
                    Console.WriteLine("Falha ao finalizar pedido detectada: " + erro.Text);
                    houveErro = true;
                }
            } catch (NoSuchElementException) {
                // Nenhum erro detectado
            }

            if (houveErro) {


                Aguardar3Segundos();
                SelecionarProduto();
                AdicionarProdutoCarrinho();
                Aguardar3Segundos();
                SelecionarProduto();
                AdicionarProdutoCarrinho();
                Aguardar3Segundos();
                ClicarCarrinho();
                Aguardar3Segundos();
                ExcluirItem();
                Aguardar3Segundos();
                SelecionarProdutosCarrinho();
                Aguardar3Segundos();
                CriarNovoEndereco();
                AdicionarCupom();

                FinalizarCompra();
            }
        }


        public void AdicionarCupom() {
            string[] Cupom = ["10OFF", "25OFF", "INVALIDO"];

            RolarMetadeDaPagina();
            Aguardar3Segundos();

            Random rando = new Random();
            int posicao = rando.Next(0, 2);

            driver.FindElement(By.Id("codigoCupom")).SendKeys(Cupom[posicao]);
            Aguardar3Segundos();
            var btnCupom = driver.FindElement(By.Id("btnAplicarCupom"));
            btnCupom.Click();
            driver.FindElement(By.Id("codigoCupom")).Clear();

            posicao = rando.Next(0, 2);


            driver.FindElement(By.Id("codigoCupom")).SendKeys(Cupom[posicao]);
            Aguardar3Segundos();
            btnCupom = driver.FindElement(By.Id("btnAplicarCupom"));
            btnCupom.Click();
        }
            
        
        public void CriarNovoEndereco() {
            RolarPaginaParaFinal();
            Aguardar3Segundos();

            var btnNovoEndereco = driver.FindElement(By.Id("btnAdicionarEndereco"));
            btnNovoEndereco.Click();

            driver.FindElement(By.Id("apelido-endereco-cliente")).SendKeys("CASA");
            driver.FindElement(By.Id("cep-endereco-cliente")).SendKeys("08820434");
            driver.FindElement(By.Id("logradouro-endereco-cliente")).SendKeys("DOUTOR. RICARDO SOFT ");
            driver.FindElement(By.Id("numero-endereco-cliente")).SendKeys("24");
            driver.FindElement(By.Id("bairro-endereco-cliente")).SendKeys("JARDIM ORQUIDEA");
            driver.FindElement(By.Id("pais-endereco-cliente")).SendKeys("BRASIL"); 
            driver.FindElement(By.Id("estado-endereco-cliente")).SendKeys("SÃO PAULO");
            driver.FindElement(By.Id("cidade-endereco-cliente")).SendKeys("MOGI DAS CRUZES");

            RolarPaginaParaFinal();
            Aguardar3Segundos();

            // Caixa de seleção Tipo de Logradouro
            var dropdownTipoLogradouro = driver.FindElement(By.Id("tipoLogradouro-endereco-cliente"));
            var selectOptionTipoLogradouro = new SelectElement(dropdownTipoLogradouro);
            selectOptionTipoLogradouro.SelectByText("RUA");

            // Caixa de seleção Tipo de residencia
            var dropdownTipoResidencia = driver.FindElement(By.Id("tipo-residencia-endereco-cliente"));
            var selectOptionTipoResidencia = new SelectElement(dropdownTipoResidencia);
            selectOptionTipoResidencia.SelectByText("CASA");
        }

        public void AdicionarProdutoCarrinho() {
            Aguardar3Segundos();

            var campoQuantidade = driver.FindElement(By.Id("quantidade-adicionar-carrinho"));
            campoQuantidade.Click();

            Random rando = new Random();
            int quantidade = rando.Next(1, 10);

            for (int i = 0; i <= quantidade; i++) {
                campoQuantidade.SendKeys(Keys.ArrowUp);
            }


            Aguardar3Segundos();

            var btnAddCarriho = driver.FindElement(By.Id("btn-adicionar-carrinho"));
            btnAddCarriho.Click();
        }

        public void SelecionarProduto() {
            var produtos = driver.FindElements(By.CssSelector(".painel-card .card"));

            var produtoSelecionado = produtos.ElementAt(new Random().Next(produtos.Count));

            var linkProduto = produtoSelecionado.FindElement(By.Id("link-produto"));
            linkProduto.Click();

        }

        public void Aguardar3Segundos() {
            Thread.Sleep(2200);
        }

        private void RolarPaginaParaFinal() {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollBy(0, document.body.scrollHeight);");
        }

        private void RolarMetadeDaPagina() {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;

            long alturaTotal = (long)js.ExecuteScript("return document.body.scrollHeight");

            long deslocamento = alturaTotal / 4;

            js.ExecuteScript($"window.scrollBy({0},{deslocamento})");
        }
    }
}
