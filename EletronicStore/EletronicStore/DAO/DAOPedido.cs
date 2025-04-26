using EletronicStore.DAO.Interface;
using EletronicStore.Models;
using EletronicStore.ViewModel;
using Microsoft.Data.SqlClient;

namespace EletronicStore.DAO {
    public class DAOPedido : DAOEntidadeDominio, IDAOGeneric {
        public bool Delete(EntidadeDominio entidade) {
            throw new NotImplementedException();
        }

        public void Insert(EntidadeDominio entidade) {
            _database = new DAOSQLServer();

            Pedido Pedido = (Pedido)entidade;

            string Insert = @"INSERT INTO PEDIDOS(PDD_DATA, PDD_STP_ID, PDD_USU_ID,PDD_END_ID) VALUES (@Data, 1, @UsuarioId, @EnderecoId);
                              SELECT PDD_ID FROM PEDIDOS WHERE PDD_DATA = @Data AND PDD_USU_ID = @UsuarioId AND PDD_END_ID = @EnderecoId";

            using(SqlConnection conn = _database.OpenConnection()) {
                using (SqlCommand query = new(Insert, conn)) {
                    query.Parameters.AddWithValue("@Data", DateTime.Now);
                    query.Parameters.AddWithValue("@UsuarioId", Pedido.Usuario.Id);
                    query.Parameters.AddWithValue("@EnderecoId", Pedido.Endereco.Id);
                    using (SqlDataReader reader = query.ExecuteReader()) {
                        while (reader.Read()) {
                            Pedido.Id = reader.GetInt32(reader.GetOrdinal("PDD_ID"));
                        }
                    }    
                }
            }

            if(Pedido.Cupons.Count > 0) {
                Insert = "INSERT INTO CUPONS_PEDIDOS(CPPD_PDD_ID, CPPD_CPN_ID) VALUES (@PedidoId, @CupomId);";

                using (SqlConnection conn = _database.OpenConnection()) {
                    using (SqlCommand query = new(Insert, conn)) {
                        foreach (Cupom Cupom in Pedido.Cupons) {
                            query.Parameters.Clear();
                            query.Parameters.AddWithValue("@PedidoId", Pedido.Id);
                            query.Parameters.AddWithValue("@CupomId", Cupom.Id);
                            query.ExecuteNonQuery();
                        }
                    }
                }
            }
        }

        public List<EntidadeDominio> Select() {
            _database = new DAOSQLServer();

            string Select = "SELECT * FROM Vw_Pedido";

            List<EntidadeDominio> ListPedidos = new List<EntidadeDominio>();
            Dictionary<int, Usuario> MapCliente = new Dictionary<int, Usuario>();

            using (SqlConnection conn = _database.OpenConnection()) {
                using (SqlCommand query = new(Select, conn)) {
                    using (SqlDataReader reader = query.ExecuteReader()) {
                        while (reader.Read()) {
                            Usuario Cliente = new Usuario();
                            Pedido Pedido = new Pedido();
                            Endereco Endereco = new Endereco();
                            Cartao Cartao = new Cartao();

                            int PedidoId = reader.GetInt32(reader.GetOrdinal("PDD_ID"));

                            if (ListPedidos.Cast<Pedido>().Where(p => p.Id == PedidoId).FirstOrDefault() != null) {
                                Pedido = ListPedidos.Cast<Pedido>().Where(p => p.Id == PedidoId).FirstOrDefault();
                            } else {
                                Pedido.Id = PedidoId;
                            }

                            Endereco.Id = reader.GetInt32(reader.GetOrdinal("END_ID"));
                            Cliente.Id = reader.GetInt32(reader.GetOrdinal("USU_ID"));
                            Cartao.Id = reader.GetInt32(reader.GetOrdinal("CRT_ID"));

                            if (!MapCliente.ContainsKey(Cliente.Id)) {
                                Cliente.Nome = reader.GetString(reader.GetOrdinal("USU_NOME"));
                                Cliente.CPF = reader.GetString(reader.GetOrdinal("USU_CPF"));
                                Cliente.Email = reader.GetString(reader.GetOrdinal("USU_EMAIL"));
                                Cliente.Genero = reader.GetString(reader.GetOrdinal("GNR_GENERO"));
                                Cliente.Status = reader.GetBoolean(reader.GetOrdinal("USU_STATUS"));
                                Cliente.Senha = reader.GetString(reader.GetOrdinal("USU_SENHA"));
                                Cliente.Telefone = new Telefone() {
                                    Id = reader.GetInt32(reader.GetOrdinal("TLF_ID")),
                                    DDD = reader.GetString(reader.GetOrdinal("TLF_DDD")),
                                    Numero = reader.GetString(reader.GetOrdinal("TLF_NUMERO")),
                                    TipoTelefone = new TipoTelefone() {
                                        Id = reader.GetInt32(reader.GetOrdinal("TPT_ID")),
                                        Tipo = reader.GetString(reader.GetOrdinal("TPT_TIPO"))
                                    }
                                };
                                Cliente.DataNascimento = reader.GetDateTime(reader.GetOrdinal("USU_DATA_NASCIMENTO"));
                                Cliente.TipoUsuario = new TipoUsuario() {
                                    Id = reader.GetInt32(reader.GetOrdinal("TPU_ID")),
                                    Tipo = reader.GetString(reader.GetOrdinal("TPU_TIPO"))
                                };

                               

                                MapCliente[Cliente.Id] = Cliente;
                            }

                            Pedido.Usuario = MapCliente[Cliente.Id];

                            if (!Pedido.Usuario.Enderecos.Any(e => e.Id == Endereco.Id)) {
                                Endereco.CEP = reader.GetString(reader.GetOrdinal("END_CEP"));
                                Endereco.Logradouro = reader.GetString(reader.GetOrdinal("END_LOGRADOURO"));
                                Endereco.Numero = reader.GetString(reader.GetOrdinal("END_NUMERO"));
                                Endereco.Bairro = reader.GetString(reader.GetOrdinal("END_BAIRRO"));
                                Endereco.Apelido = reader.GetString(reader.GetOrdinal("END_APELIDO"));

                                Endereco.TipoLogradouro = new TipoLogradouro() {
                                    Id = reader.GetInt32(reader.GetOrdinal("TPL_ID")),
                                    Tipo = reader.GetString(reader.GetOrdinal("TPL_TIPO"))
                                };
                                Endereco.TipoResidencia = new TipoResidencia() {
                                    Id = reader.GetInt32(reader.GetOrdinal("TPR_ID")),
                                    Tipo = reader.GetString(reader.GetOrdinal("TPR_TIPO"))
                                };
                                Endereco.Municipio = new Municipio() {
                                    Id = reader.GetInt32(reader.GetOrdinal("MUC_ID")),
                                    Nome = reader.GetString(reader.GetOrdinal("MUC_NOME")),
                                    Estado = new Estado() {
                                        Id = reader.GetInt32(reader.GetOrdinal("EST_ID")),
                                        Nome = reader.GetString(reader.GetOrdinal("EST_NOME")),
                                        Pais = new Pais() {
                                            Id = reader.GetInt32(reader.GetOrdinal("PAS_ID")),
                                            Nome = reader.GetString(reader.GetOrdinal("PAS_NOME"))
                                        }
                                    }
                                };

                                if (!reader.IsDBNull(reader.GetOrdinal("END_OBSERVACAO"))) {
                                    Endereco.Observacao = reader.GetString(reader.GetOrdinal("END_OBSERVACAO"));
                                } else {
                                    Endereco.Observacao = "";
                                }

                                Pedido.Endereco = Endereco;
                            }


                            if (!Pedido.Usuario.Cartoes.Any(c => c.Id == Cartao.Id)) {
                                Cartao.Numero = reader.GetString(reader.GetOrdinal("CRT_NUMERO"));
                                Cartao.CVV = reader.GetString(reader.GetOrdinal("CRT_CVV"));
                                Cartao.Apelido = reader.GetString(reader.GetOrdinal("CRT_APELIDO"));
                                Cartao.Funcao = reader.GetString(reader.GetOrdinal("CRT_FUNCAO"));
                                Cartao.Bandeira = new BandeiraCartao() {
                                    Id = reader.GetInt32(reader.GetOrdinal("BDC_ID")),
                                    Nome = reader.GetString(reader.GetOrdinal("BDC_NOME"))
                                };

                                Pedido.Usuario.Cartoes.Add(Cartao);
                            }

                            Produto Produto = new();
                            Produto.Id = reader.GetInt32(reader.GetOrdinal("PRD_ID"));
                            if (!Pedido.Produtos.Any(p => p.Produto.Id == Produto.Id)) {
                                Pedido.Produtos.Add(new ItemPedido {
                                    Produto = new Produto {
                                        Id = reader.GetInt32(reader.GetOrdinal("PRD_ID")),
                                        Nome = reader.GetString(reader.GetOrdinal("PRD_NOME")),
                                        Preco = (double)reader.GetDecimal(reader.GetOrdinal("PRD_PRECO")),
                                        Descricao = reader.GetString(reader.GetOrdinal("PRD_DESCRICAO")),
                                        URL_Imagem = reader.GetString(reader.GetOrdinal("PRD_URL_IMG")),
                                        Status = reader.GetBoolean(reader.GetOrdinal("PRD_STATUS")),
                                        Marca = new Marca {
                                            Id = reader.GetInt32(reader.GetOrdinal("MRC_ID")),
                                            Nome = reader.GetString(reader.GetOrdinal("MRC_NOME"))
                                        },
                                        Fornecedor = new Fornecedor {
                                            Id = reader.GetInt32(reader.GetOrdinal("FRD_ID")),
                                            Nome = reader.GetString(reader.GetOrdinal("FRD_NOME"))
                                        },
                                        QuantidadeEstoque = reader.GetInt32(reader.GetOrdinal("ETQ_QUANTIDADE")),
                                        TipoProduto = new TipoProduto {
                                            Id = reader.GetInt32(reader.GetOrdinal("TPP_ID")),
                                            Tipo = reader.GetString(reader.GetOrdinal("TPP_TIPO"))
                                        },
                                        GrupoPrecificacao = new GrupoPrecificacao {
                                            Id = reader.GetInt32(reader.GetOrdinal("GRP_ID")),
                                            GrupoPrecificacaao = reader.GetString(reader.GetOrdinal("GRP_PRECIFICACAO")),
                                            Margem = (double)reader.GetDecimal(reader.GetOrdinal("GRP_MARGEM"))
                                        }
                                    },
                                    Quantidade = reader.GetInt32(reader.GetOrdinal("IPD_QUANTIDADE"))
                                });
                            }

                            Pedido.Status = reader.GetString(reader.GetOrdinal("STP_STATUS"));
                            Pedido.UltimaAtualizacao = reader.GetDateTime(reader.GetOrdinal("PDD_DATA"));

                            Cupom Cupom = new Cupom();

                            int Id = reader.GetOrdinal("CPN_ID");

                            if (!reader.IsDBNull(Id)) {
                                Cupom.Id = reader.GetInt32(reader.GetOrdinal("CPN_ID"));
                                if (!Pedido.Cupons.Any(c => c.Id == Cupom.Id)) {
                                    Pedido.Cupons.Add(new Cupom {
                                        Id = reader.GetInt32(reader.GetOrdinal("CPN_ID")),
                                        Nome = reader.GetString(reader.GetOrdinal("CPN_NOME")),
                                        Desconto = (double)reader.GetDecimal(reader.GetOrdinal("CPN_DESCONTO"))
                                    });
                                }
                            }

                            

                            if (!ListPedidos.Cast<Pedido>().Any(p => p.Id == Pedido.Id)) {
                                ListPedidos.Add(Pedido);
                            }
                        }
                    }
                }
            }
            return ListPedidos;
        }

        public bool Update(EntidadeDominio entidade) {
            Pedido Pedido = (Pedido)entidade;

            _database = new DAOSQLServer();

            string Update = @"UPDATE PEDIDOS SET PDD_STP_ID = (SELECT STP_ID FROM STATUS_PEDIDO WHERE STP_STATUS = @Status) WHERE PDD_ID = @PedidoId;
                              UPDATE PEDIDOS SET PDD_DATA = @Data WHERE PDD_ID = @PedidoId;";

            using (SqlConnection conn = _database.OpenConnection()) {
                using (SqlCommand query = new SqlCommand(Update, conn)) {
                    query.Parameters.AddWithValue("@Status", Pedido.Status);
                    query.Parameters.AddWithValue("@PedidoId", Pedido.Id);
                    query.Parameters.AddWithValue("@Data", DateTime.Now);
                    query.ExecuteNonQuery();
                }
            }

            return true;
        }
    }
}
