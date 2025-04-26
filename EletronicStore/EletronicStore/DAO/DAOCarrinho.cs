using EletronicStore.DAO.Interface;
using EletronicStore.Models;
using EletronicStore.ViewModel;
using Microsoft.Data.SqlClient;

namespace EletronicStore.DAO {
    public class DAOCarrinho : DAOEntidadeDominio, IDAOGeneric {
        public bool Delete(EntidadeDominio entidade) {
            _database = new DAOSQLServer();

            string Insert = "DELETE CARRINHO WHERE CRN_USU_ID = @UsuarioId AND CRN_PRD_ID = @ProdutoId";

            Carrinho Carrinho = (Carrinho)entidade;

            try {
                using (SqlConnection conn = _database.OpenConnection()) {
                    using (SqlCommand query = new(Insert, conn)) {
                        foreach (ItemPedido item in Carrinho.ItensCarrinho.Where(i => i.ExclusaoCarrinho).ToList()) {
                            query.Parameters.Clear();
                            query.Parameters.AddWithValue("@UsuarioId", Carrinho.Usuario.Id);
                            query.Parameters.AddWithValue("@ProdutoId", item.Id);
                            query.ExecuteNonQuery();
                        }
                    }
                }
                return true;
            }catch (Exception ex) {
                return false;
            }

        }

        public void Insert(EntidadeDominio entidade) {
            _database = new DAOSQLServer();

            string Insert = "INSERT INTO CARRINHO (CRN_QUANTIDADE, CRN_CRIADO, CRN_USU_ID, CRN_PRD_ID) VALUES (@Quantidade, @DataCriado, 1, @ProdutoId);";

            ItemPedido ItemCarrinho = (ItemPedido)entidade;

            using (SqlConnection conn = _database.OpenConnection()) {
                using (SqlCommand query = new(Insert, conn)) {
                    query.Parameters.AddWithValue("@Quantidade", ItemCarrinho.Quantidade);
                    query.Parameters.AddWithValue("@DataCriado", DateTime.Now);
                    query.Parameters.AddWithValue("@ProdutoId", ItemCarrinho.Produto.Id);
                    query.ExecuteNonQuery();
                }
            }
        }

        public List<EntidadeDominio> Select() {
            _database = new DAOSQLServer();

            string Select = "SELECT * FROM Vw_Carrinho";

            List<EntidadeDominio> ListCarrinho = new();
            Dictionary<int, Carrinho> MapCliente = new Dictionary<int, Carrinho>();

            using (SqlConnection conn = _database.OpenConnection()) {
                using (SqlCommand query = new(Select, conn)) {
                    using (SqlDataReader reader = query.ExecuteReader()) {
                        while (reader.Read()) {
                            Carrinho Carrinho = new Carrinho();
                            Usuario Cliente = new Usuario();
                            Produto Produto = new Produto();

                            Cliente.Id = reader.GetInt32(reader.GetOrdinal("USU_ID"));
                            Produto.Id = reader.GetInt32(reader.GetOrdinal("PRD_ID"));
    

                            if (!MapCliente.ContainsKey(Cliente.Id)) {
                                Cliente.Nome = reader.GetString(reader.GetOrdinal("USU_NOME"));
                                Carrinho.Usuario = Cliente;

                                MapCliente[Cliente.Id] = Carrinho;
                            }

                            Carrinho = MapCliente[Cliente.Id];
                            Carrinho.DataCriado = reader.GetDateTime(reader.GetOrdinal("CRN_CRIADO"));

                            if (!Carrinho.ItensCarrinho.Any(p => p.Produto.Id == Produto.Id)) {
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
                                };

                                Carrinho.ItensCarrinho.Add(new ItemPedido {
                                    Id = reader.GetInt32(reader.GetOrdinal("PRD_ID")),
                                    Produto = Produto,
                                    Quantidade = reader.GetInt32(reader.GetOrdinal("CRN_QUANTIDADE")),
                                });
                            } else {
                                Carrinho.ItensCarrinho.Where((p => p.Produto.Id == Produto.Id)).FirstOrDefault().Quantidade +=  reader.GetInt32(reader.GetOrdinal("CRN_QUANTIDADE"));
                            }
                            ListCarrinho.Add(Carrinho);
                            
                        }

                    }
                }
            }
            return ListCarrinho;
        }

        public bool Update(EntidadeDominio entidade) {
            throw new NotImplementedException();
        }
    }
}
