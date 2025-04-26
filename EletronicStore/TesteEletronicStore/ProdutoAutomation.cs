using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;

namespace TesteEletronicStore {
    class ProdutoAutomation {
        public IWebDriver driver;


        public ProdutoAutomation() {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--disable-blink-features=AutomationControlled"); // Oculta a automação
            options.AddExcludedArgument("enable-automation"); // Remove a flag de automação
            options.AddArgument("--start-maximized"); // Abre maximizado (opcional)

            driver = new ChromeDriver(options);

            driver.Navigate().GoToUrl("https://localhost:7055");

            Aguardar3Segundos();
            ExibirTodosProdutos();
            Aguardar3Segundos();
            CadastrarNovoProduto();
            Aguardar3Segundos();
            ExibirTodosProdutos();
            int Id = SelecionarIdProduto();
            VisualizarProduto(Id);
            Aguardar3Segundos();
            ExibirTodosProdutos();
            Aguardar3Segundos();
            EditarProduto(Id);
            Aguardar3Segundos();
            ExibirTodosProdutos();
            Aguardar3Segundos();
            FiltrarPesquisa();
            Aguardar3Segundos();
            InativarProduto(Id);
            Aguardar3Segundos();
            ExibirTodosProdutos();
            Aguardar3Segundos();
            FiltrarProdutosInativados();
            Id = SelecionarIdProduto();
            AtivarProduto(Id);
        }

        public void ExibirTodosProdutos() {
           var linkExibirProdutos = driver.FindElement(By.Id("link-exibir-todos-produto"));
            linkExibirProdutos.Click();
        }


        public void CadastrarNovoProduto() {
            var linkCadastrarProduto = driver.FindElement(By.Id("link-cadastrar-novo-produto"));
            linkCadastrarProduto.Click();

            Aguardar3Segundos();

            driver.FindElement(By.Id("nome-produto")).SendKeys("Fone Bluetooth Redmi Buds 6");
            driver.FindElement(By.Id("marca-produto")).SendKeys("Marca");
            driver.FindElement(By.Id("valor-bruto-produto")).SendKeys("12,30");
            driver.FindElement(By.Id("descricao-produto")).SendKeys("Som excepcional para todos os momentos Sistema de drivers duplos Hi-Fi para uma experiência sonora impressionante, com agudos suaves, médios ricos, graves sólidos e 4 opções de equalização diferentes.");

            // Caixa de seleção Grupo de Precificacao
            var dropdownGrupoPrecificacao = driver.FindElement(By.Id("grupo-precificacao-produto"));
            var selectOptionGrupoPrecificacao = new SelectElement(dropdownGrupoPrecificacao);
            selectOptionGrupoPrecificacao.SelectByText("PRATA");

            // Caixa de seleção Tipo Produto
            var dropdownTipoProduto = driver.FindElement(By.Id("tipo-produto"));
            var selectOptionTipoProduto = new SelectElement(dropdownTipoProduto);
            selectOptionTipoProduto.SelectByText("PERIFÉRICO");


            string caminho = @"C:\Users\jp120\Downloads\8601_fone-bluetooth-redmi-buds-6-xiaomi-prin_z7_638737685463273779.webp";
            var inputFile = driver.FindElement(By.Id("img-produto"));
            inputFile.SendKeys(caminho);


            var Faker = new Faker("pt_BR");
            string nomeFake = Faker.Name.FullName();
            driver.FindElement(By.Id("fornecedor-produto")).SendKeys(nomeFake);

            driver.FindElement(By.Id("quantidade-estoque-produto")).SendKeys("10");

            var btnSubmitCadastro = driver.FindElement(By.Id("submit-cadastro-produto"));
            btnSubmitCadastro.Click();
        }

        public void Aguardar3Segundos() {
            Thread.Sleep(3000);
        }

        public int SelecionarIdProduto() {
            var linksProdutos = driver.FindElements(By.Id("editar-produto")).Take(5).ToList();

            int i = 0;
            int IdProduto = 0;
            if (linksProdutos.Count > 0) {
                Random random = new();
                i = random.Next(linksProdutos.Count);

                IWebElement linkSelecionado = linksProdutos[i];
                string href = linkSelecionado.GetAttribute("href");
                IdProduto = int.Parse(href.Split("/").Last());
            }

            return IdProduto;
        }

        public void VisualizarProduto(int IdProduto) {
            var linkVisualizar = driver.FindElement(By.XPath($"//a[@href='/Produto/ExibirProduto/{IdProduto}']"));
            linkVisualizar.Click();
        }

        public void EditarProduto(int IdProduto) {
            var linkEditar = driver.FindElement(By.XPath($"//a[@href='/Produto/EditarProduto/{IdProduto}']"));
            linkEditar.Click();

            driver.FindElement(By.Id("marca-produto")).Clear();
            driver.FindElement(By.Id("marca-produto")).SendKeys("XIAOMI");

            var Faker = new Faker("pt_BR");
            string nomeFake = Faker.Name.FullName();
            driver.FindElement(By.Id("fornecedor-produto")).Clear();
            driver.FindElement(By.Id("fornecedor-produto")).SendKeys(nomeFake);

            // Caixa de seleção Tipo Produto
            var dropdownTipoProduto = driver.FindElement(By.Id("tipo-produto"));
            var selectOptionTipoProduto = new SelectElement(dropdownTipoProduto);
            selectOptionTipoProduto.SelectByText("ACESSÓRIO");

            var btnSubmitEdicao = driver.FindElement(By.Id("submit-edicao-produto"));
            btnSubmitEdicao.Click();
        }

        public void FiltrarPesquisa() {
            driver.FindElement(By.Id("filter-marca-produto")).SendKeys("XIAOMI");

            // Caixa de seleção Tipo Produto
            var dropdownTipoProduto = driver.FindElement(By.Id("filter-tipo-produto"));
            var selectOptionTipoProduto = new SelectElement(dropdownTipoProduto);
            selectOptionTipoProduto.SelectByText("ACESSÓRIO");

            Aguardar3Segundos();

            var btnSubmitFiltro = driver.FindElement(By.Id("submit-filter"));
            btnSubmitFiltro.Click();

            Aguardar3Segundos();

            btnSubmitFiltro = driver.FindElement(By.Id("submit-filter"));
            btnSubmitFiltro.Click();
        }

        public void InativarProduto(int IdProduto) {
            var linkInativar = driver.FindElement(By.XPath($"//a[@href='/Produto/InativarProduto/{IdProduto}']"));
            linkInativar.Click();

            Aguardar3Segundos();
            driver.FindElement(By.Id("motivo-inativacao-produto")).SendKeys("Temporada inadequada");
            Aguardar3Segundos();

            var btnSubmitInativacao = driver.FindElement(By.Id("submit-inativacao-produto"));
            btnSubmitInativacao.Click();

        }

        public void FiltrarProdutosInativados() {
            // Caixa de seleção Tipo Produto
            var dropdownTipoProduto = driver.FindElement(By.Id("filter-status"));
            var selectOptionTipoProduto = new SelectElement(dropdownTipoProduto);
            selectOptionTipoProduto.SelectByText("Desativado");

            Aguardar3Segundos();

            var btnSubmitFiltro = driver.FindElement(By.Id("submit-filter"));
            btnSubmitFiltro.Click();
        }

        public void AtivarProduto(int IdProduto) {
            Aguardar3Segundos();
            var linkInativar = driver.FindElement(By.XPath($"//a[@href='/Produto/InativarProduto/{IdProduto}']"));
            linkInativar.Click();
        }
    }
}
