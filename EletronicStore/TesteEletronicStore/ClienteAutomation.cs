using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Bogus;

namespace SeleniumBot {
    class ClienteAutomation {
        public IWebDriver driver;

        public ClienteAutomation() {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--disable-blink-features=AutomationControlled"); // Oculta a automação
            options.AddExcludedArgument("enable-automation"); // Remove a flag de automação
            options.AddArgument("--start-maximized"); // Abre maximizado (opcional)

            driver = new ChromeDriver(options);

            

            driver.Navigate().GoToUrl("https://localhost:7055");

            CriarNovoUsuario();
            CadastrarSenhaForaDoPadrao();
            Aguardar3Segundos();
            CadastrarSenhaCorreto();
            Aguardar3Segundos();
            ClicarExibirClientes();
            Aguardar3Segundos();
            FiltrarUusarios();
            int ClienteSelecionado = SelecionarIdCliente();
            Aguardar3Segundos();
            VisualizarCliente(ClienteSelecionado);
            Aguardar3Segundos();
            ClicarExibirClientes();
            Aguardar3Segundos();
            ClicarAlterarStatusCliente(ClienteSelecionado);
            Aguardar3Segundos();
            ClicarAlterarStatusCliente(ClienteSelecionado);
            Aguardar3Segundos();
            ClicarExibirClientes();
            Aguardar3Segundos();
            CriarNovoEndereco(ClienteSelecionado);
            Aguardar3Segundos();
            CriarNovoCartao(ClienteSelecionado);
            Aguardar3Segundos();
            DeletarCartao();
            Aguardar3Segundos();
            DeletarEndereco();
            Aguardar3Segundos();
            EditarCartao();
            Aguardar3Segundos();
            EditarEndereco();
            Aguardar3Segundos();
            ClicarExibirClientes();
            Aguardar3Segundos();
            EditarCliente(ClienteSelecionado);
        }

        public void CadastrarSenhaForaDoPadrao() {
            /* SENHA */
            driver.FindElement(By.Id("pwd-cliente")).SendKeys("1234*");
            driver.FindElement(By.Id("pwd-confirm-cliente")).SendKeys("1234*");

            Aguardar3Segundos();

            /* BOTÃO DE CADASTRO */
            var btnSubmitCadastro = driver.FindElement(By.Id("submit-cadastro-cliente"));
            btnSubmitCadastro.Click();

        }


        public void CadastrarSenhaCorreto() {
            RolarPaginaParaFinal();
            Aguardar3Segundos();

            /* SENHA */
            driver.FindElement(By.Id("pwd-cliente")).SendKeys("Jp12042004*");
            driver.FindElement(By.Id("pwd-confirm-cliente")).SendKeys("Jp12042004*");

            Aguardar3Segundos();

            /* BOTÃO DE CADASTRO */
            var btnSubmitCadastro = driver.FindElement(By.Id("submit-cadastro-cliente"));
            btnSubmitCadastro.Click();

        }

        public void FiltrarUusarios() {
            driver.FindElement(By.Id("filter-email")).SendKeys("@live");

            var dropdownStatus = driver.FindElement(By.Id("filter-status"));
            var selectOptionStatus = new SelectElement(dropdownStatus);
            selectOptionStatus.SelectByText("Ativo");

            Aguardar3Segundos();

            var btnSubmitFiltrar = driver.FindElement(By.Id("submit-filter"));
            btnSubmitFiltrar.Click();

            Aguardar3Segundos();

            btnSubmitFiltrar = driver.FindElement(By.Id("submit-filter"));
            btnSubmitFiltrar.Click();
        }

        public void CriarNovoUsuario() {
            var Faker = new Faker("pt_BR");
            string nomeFake = Faker.Name.FullName();
            string emailFake = Faker.Internet.Email();

            driver.FindElement(By.Id("nome-cliente")).SendKeys(nomeFake);
            driver.FindElement(By.Id("email-cliente")).SendKeys(emailFake);

            string cpfGerado = GerarCPF();
            driver.FindElement(By.Id("cpf-cliente")).SendKeys(cpfGerado);

            driver.FindElement(By.Id("data-nascimento-cliente")).SendKeys("12/04/2004");

            // Caixa de seleção de genero
            var dropdownGenero = driver.FindElement(By.Id("genero-cliente"));
            var selectOptionGenero = new SelectElement(dropdownGenero);
            selectOptionGenero.SelectByText("Feminino");

            /* TELEFONE */
            driver.FindElement(By.Id("ddd-telefone-cliente")).SendKeys("11");
            driver.FindElement(By.Id("numero-telefone-cliente")).SendKeys("12344321");

            // Caixa de seleção de Tipo de telefone
            var dropdownTipoTelefone = driver.FindElement(By.Id("tipo-teletone-cliente"));
            var selectOptionTipoTelefone = new SelectElement(dropdownTipoTelefone);
            selectOptionTipoTelefone.SelectByText("Movel");

            /* CARTÃO */
            driver.FindElement(By.Id("apelido-cartao-cliente")).SendKeys("CARTÃO NUBANK");
            driver.FindElement(By.Id("numero-cartao-cliente")).SendKeys("1234432109877890");
            driver.FindElement(By.Id("dtValidade-cartao-cliente")).SendKeys("11/32");
            driver.FindElement(By.Id("cvv-cartao-cliente")).SendKeys("123");

            // Caixa de seleção Bandeira
            var dropdownBandeiraCartao = driver.FindElement(By.Id("bandeira-cartao-cliente"));
            var selectOptionBandeiraCartao = new SelectElement(dropdownBandeiraCartao);
            selectOptionBandeiraCartao.SelectByText("MASTERCARD");

            // Caixa de seleção função do cartão
            var dropdownFuncaoCartao = driver.FindElement(By.Id("funcao-cartao-cliente"));
            var selectOptionFuncaoCartao = new SelectElement(dropdownFuncaoCartao);
            selectOptionFuncaoCartao.SelectByText("Crédito");

            /* ENDEREÇO */
            driver.FindElement(By.Id("apelido-endereco-cliente")).SendKeys("TRABALHO");
            driver.FindElement(By.Id("cep-endereco-cliente")).SendKeys("08773100");
            driver.FindElement(By.Id("logradouro-endereco-cliente")).SendKeys("DR. RENATO GRANADEIRO GUIMARÃES");
            driver.FindElement(By.Id("numero-endereco-cliente")).SendKeys("17");
            // Começar a rolar suavemente logo após o carregamento da página
            RolarPaginaParaFinal();
            Aguardar3Segundos();
            driver.FindElement(By.Id("bairro-endereco-cliente")).SendKeys("MOGILAR");
            driver.FindElement(By.Id("pais-endereco-cliente")).SendKeys("BRASIL");

            driver.FindElement(By.Id("estado-endereco-cliente")).SendKeys("SÃO PAULO");
            driver.FindElement(By.Id("cidade-endereco-cliente")).SendKeys("MOGI DAS CRUZES");
            driver.FindElement(By.Id("observacao-endereco-cliente")).SendKeys("RFA ADVOGADOS");


            // Caixa de seleção Tipo de Logradouro
            var dropdownTipoLogradouro = driver.FindElement(By.Id("tipoLogradouro-endereco-cliente"));
            var selectOptionTipoLogradouro = new SelectElement(dropdownTipoLogradouro);
            selectOptionTipoLogradouro.SelectByText("RUA");

            // Caixa de seleção Tipo de residencia
            var dropdownTipoResidencia = driver.FindElement(By.Id("tipo-residencia-endereco-cliente"));
            var selectOptionTipoResidencia = new SelectElement(dropdownTipoResidencia);
            selectOptionTipoResidencia.SelectByText("STUDIO");

            RolarPaginaParaFinal();
            Aguardar3Segundos();

            // Checkbox de cobrança/entrega
            var checkboxCobranca = driver.FindElement(By.Id("cobranca-endereco-cliente"));
            if (!checkboxCobranca.Selected) {
                checkboxCobranca.Click();
            }

            var checkboxEntrega = driver.FindElement(By.Id("entrega-endereco-cliente"));
            if (!checkboxEntrega.Selected) {
                checkboxEntrega.Click();
            }
        }



        public void ClicarAlterarStatusCliente(int IdCliente) {
            var linkAtivarClientes = driver.FindElement(By.XPath($"//a[@href='/Cliente/AlterarStatusCliente/{IdCliente}']"));
            linkAtivarClientes.Click();
        }

        public void ClicarExibirClientes() {
            var LinkExibirCliente = driver.FindElement(By.XPath("/html/body/header/nav/div/div/ul/li[2]/a"));
            LinkExibirCliente.Click();
        }

        public void Aguardar3Segundos() {
            Thread.Sleep(3000);
        }

        public void VisualizarCliente(int IdCliente) {
            var linkVisualizar = driver.FindElement(By.XPath($"//a[@href='/Cliente/ExibirDadosCliente/{IdCliente}']"));
            linkVisualizar.Click();
        }

        public void CriarNovoEndereco(int IdCliente) {
            VisualizarCliente(IdCliente);
            RolarPaginaParaFinal();
            Aguardar3Segundos();

            var linkNovoEndereco = driver.FindElement(By.XPath($"//a[@href='/Endereco/CadastrarEndereco/{IdCliente}']"));
            linkNovoEndereco.Click();

            driver.FindElement(By.Id("apelido-endereco-cliente")).SendKeys("CASA");
            driver.FindElement(By.Id("cep-endereco-cliente")).SendKeys("08773100");
            driver.FindElement(By.Id("logradouro-endereco-cliente")).SendKeys("DR. JOSÉ DA SILVA ");
            driver.FindElement(By.Id("numero-endereco-cliente")).SendKeys("24");
            // Começar a rolar suavemente logo após o carregamento da página
            RolarPaginaParaFinal();
            driver.FindElement(By.Id("bairro-endereco-cliente")).SendKeys("JARDIM MOGI");
            driver.FindElement(By.Id("pais-endereco-cliente")).SendKeys("BRASIL");

            driver.FindElement(By.Id("estado-endereco-cliente")).SendKeys("SÃO PAULO");
            driver.FindElement(By.Id("cidade-endereco-cliente")).SendKeys("MOGI DAS CRUZES");

            // Caixa de seleção Tipo de Logradouro
            var dropdownTipoLogradouro = driver.FindElement(By.Id("tipoLogradouro-endereco-cliente"));
            var selectOptionTipoLogradouro = new SelectElement(dropdownTipoLogradouro);
            selectOptionTipoLogradouro.SelectByText("AVENIDA");

            // Caixa de seleção Tipo de residencia
            var dropdownTipoResidencia = driver.FindElement(By.Id("tipo-residencia-endereco-cliente"));
            var selectOptionTipoResidencia = new SelectElement(dropdownTipoResidencia);
            selectOptionTipoResidencia.SelectByText("CASA");

            // Checkbox de cobrança/entrega

            var listCheckBoxesCobranca = driver.FindElements(By.Id("cobranca-endereco-cliente"));

            if (listCheckBoxesCobranca.Count > 0) {
                var checkboxCobranca = driver.FindElement(By.Id("cobranca-endereco-cliente"));
                if (!checkboxCobranca.Selected) {
                    checkboxCobranca.Click();
                }
            }

            var listCheckBoxesEntrega = driver.FindElements(By.Id("entrega-endereco-cliente"));

            if (listCheckBoxesEntrega.Count > 0) {
                var checkboxEntrega = driver.FindElement(By.Id("entrega-endereco-cliente"));
                if (!checkboxEntrega.Selected) {
                    checkboxEntrega.Click();
                }
            }

            var btnSubmitEndereco = driver.FindElement(By.Id("cadastrar-novo-endereco"));
            btnSubmitEndereco.Click();

        }

        public void CriarNovoCartao(int IdCliente) {
            RolarMetadeDaPagina();
            Aguardar3Segundos();

            var linkNovoCartao = driver.FindElement(By.XPath($"//a[@href='/Cartao/CadastrarCartao/{IdCliente}']"));
            linkNovoCartao.Click();

            driver.FindElement(By.Id("apelido-cartao-cliente")).SendKeys("CARTÃO PICPAY");
            driver.FindElement(By.Id("numero-cartao-cliente")).SendKeys("1234234534564567");
            driver.FindElement(By.Id("dtValidade-cartao-cliente")).SendKeys("02/36");
            driver.FindElement(By.Id("cvv-cartao-cliente")).SendKeys("120");

            // Caixa de seleção Bandeira
            var dropdownBandeiraCartao = driver.FindElement(By.Id("bandeira-cartao-cliente"));
            var selectOptionBandeiraCartao = new SelectElement(dropdownBandeiraCartao);
            selectOptionBandeiraCartao.SelectByText("ELO");

            // Caixa de seleção função do cartão
            var dropdownFuncaoCartao = driver.FindElement(By.Id("funcao-cartao-cliente"));
            var selectOptionFuncaoCartao = new SelectElement(dropdownFuncaoCartao);
            selectOptionFuncaoCartao.SelectByText("Débito");

            var btnSubmitCartao = driver.FindElement(By.Id("cadastrar-novo-cartao"));
            btnSubmitCartao.Click();
        }

        public void EditarCartao() {
            var linksCartoes = driver.FindElements(By.Id("editar-cartao"));
            if (linksCartoes.Count > 0) {
                Random random = new();
                int i = random.Next(linksCartoes.Count);

                linksCartoes[i].Click();
            }
            driver.FindElement(By.Id("apelido-cartao-cliente")).Clear();
            driver.FindElement(By.Id("apelido-cartao-cliente")).SendKeys("CARTÃO BRASIL");

            var dropdownBandeiraCartao = driver.FindElement(By.Id("bandeira-cartao-cliente"));
            var selectOptionBandeiraCartao = new SelectElement(dropdownBandeiraCartao);
            selectOptionBandeiraCartao.SelectByText("VISA");

            var btnSalvarCartao = driver.FindElement(By.Id("salvar-cartao"));
            btnSalvarCartao.Click();

        }

        public void EditarEndereco() {
            RolarPaginaParaFinal();

            Aguardar3Segundos();

            var linksEndereco = driver.FindElements(By.Id("editar-endereco"));
            if (linksEndereco.Count > 0) {
                Random random = new();
                int i = random.Next(linksEndereco.Count);

                linksEndereco[i].Click();
            }
            driver.FindElement(By.Id("apelido-endereco-cliente")).Clear();
            driver.FindElement(By.Id("apelido-endereco-cliente")).SendKeys("CONDOMINIO");
            

            driver.FindElement(By.Id("cep-endereco-cliente")).Clear();
            driver.FindElement(By.Id("cep-endereco-cliente")).SendKeys("08717400");

            driver.FindElement(By.Id("logradouro-endereco-cliente")).Clear();
            driver.FindElement(By.Id("logradouro-endereco-cliente")).SendKeys("JOSÉ DA SILVA BLA");

            RolarPaginaParaFinal();
            Aguardar3Segundos();


            var btnSalvarEndereco = driver.FindElement(By.Id("salvar-endereco"));
            btnSalvarEndereco.Click();
        }

        public void DeletarCartao() {
            var linksCartoes = driver.FindElements(By.Id("excluir-cartao"));
            if(linksCartoes.Count > 0) {
                Random random = new();
                int i = random.Next(linksCartoes.Count);

                linksCartoes[i].Click();
            }

            Aguardar3Segundos();

            var btnConfirmExclusao = driver.FindElement(By.Id("confirmar-exclusao-cartao"));
            btnConfirmExclusao.Click();

        }

        public void EditarCliente(int IdCliente) {
            var linkVisualizar = driver.FindElement(By.XPath($"//a[@href='/Cliente/AtualizarCliente/{IdCliente}']"));
            linkVisualizar.Click();

            var Faker = new Faker("pt_BR");
            string nomeFake = Faker.Name.FullName();
            string emailFake = Faker.Internet.Email();

            driver.FindElement(By.Id("nome-cliente")).Clear();
            driver.FindElement(By.Id("email-cliente")).Clear();

            driver.FindElement(By.Id("nome-cliente")).SendKeys(nomeFake);
            driver.FindElement(By.Id("email-cliente")).SendKeys(emailFake);


            string cpfGerado = GerarCPF();
            driver.FindElement(By.Id("cpf-cliente")).Clear();
            driver.FindElement(By.Id("cpf-cliente")).SendKeys(cpfGerado);

            RolarPaginaParaFinal();
            Aguardar3Segundos();

            /* SENHA */
            driver.FindElement(By.Id("pwd-cliente")).SendKeys("Jp12042004*");
            driver.FindElement(By.Id("pwd-confirm-cliente")).SendKeys("Jp12042004*");

            var btnAtualizarCliente = driver.FindElement(By.Id("atualizar-cliente"));
            btnAtualizarCliente.Click();
        }

        public int SelecionarIdCliente() {
            var linksClientes = driver.FindElements(By.Id("usuario-ativo")).Take(5).ToList();

            int i = 0;
            int IdCliente = 0;
            if (linksClientes.Count > 0) {
                Random random = new();
                i = random.Next(linksClientes.Count);

                IWebElement linkSelecionado = linksClientes[i];
                string href = linkSelecionado.GetAttribute("href");
                IdCliente = int.Parse(href.Split("/").Last());
            }

            return IdCliente;
        }

        public void DeletarEndereco() {
            RolarPaginaParaFinal();

            Aguardar3Segundos();

            var linksEndereco = driver.FindElements(By.Id("excluir-endereco"));
            if (linksEndereco.Count > 0) {
                Random random = new();
                int i = random.Next(linksEndereco.Count);

                linksEndereco[i].Click();
            }

            Aguardar3Segundos();
            RolarPaginaParaFinal();
            Aguardar3Segundos();
            
            var btnConfirmExclusao = driver.FindElement(By.Id("confirmar-exclusao-endereco"));
            btnConfirmExclusao.Click();

        }

        private void RolarPaginaParaFinal() {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollBy(0, document.body.scrollHeight);");
        }

        private string GerarCPF() {
            Random rand = new Random();
            return new string(Enumerable.Range(0, 11).Select(c => (char)('0' + rand.Next(10))).ToArray());
        }
        private void RolarMetadeDaPagina() {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;

            long alturaTotal = (long)js.ExecuteScript("return document.body.scrollHeight");

            long deslocamento = alturaTotal / 4;

            js.ExecuteScript($"window.scrollBy({0},{deslocamento})");
        }

    }
}
