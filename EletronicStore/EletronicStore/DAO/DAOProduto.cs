using EletronicStore.DAO.Interface;
using EletronicStore.Models;
using Microsoft.Data.SqlClient;

namespace EletronicStore.DAO {
    public class DAOProduto : DAOEntidadeDominio, IDAOGeneric {
        public bool Delete(EntidadeDominio entidade) {
            throw new NotImplementedException();
        }

        public void Insert(EntidadeDominio entidade) {
            _database = new DAOSQLServer();

            string Insert = @"INSERT INTO PRODUTOS (PRD_NOME, PRD_PRECO, PRD_DESCRICAO, PRD_STATUS, PRD_URL_IMG, PRD_MRC_ID, PRD_TPP_ID, PRD_GRP_ID, PRD_FRD_ID) VALUES
                            (@Nome, @Preco, @Descricao, 1, @URLImg, @MarcaId, @TipoProdutoId, @GrupoPrecificacaoId, @FornecedorId);

                            SELECT PRD_ID FROM PRODUTOS WHERE 
                            PRD_NOME = @Nome AND
                            PRD_PRECO = @Preco AND 
                            PRD_DESCRICAO = @Descricao AND
                            PRD_STATUS = 1 AND
                            PRD_URL_IMG = @URLImg AND
                            PRD_MRC_ID = @MarcaId AND 
                            PRD_TPP_ID = @TipoProdutoId AND
                            PRD_GRP_ID = @GrupoPrecificacaoId AND
                            PRD_FRD_ID = @FornecedorId";

            Produto Produto = (Produto)entidade;

            using(SqlConnection conn = _database.OpenConnection()) {
                using (SqlCommand query = new(Insert, conn)) {
                    query.Parameters.AddWithValue("@Id", Produto.Id);
                    query.Parameters.AddWithValue("@Nome", Produto.Nome);
                    query.Parameters.AddWithValue("@Preco", Produto.Preco);
                    query.Parameters.AddWithValue("@Descricao", Produto.Descricao);
                    query.Parameters.AddWithValue("@URLImg", Produto.URL_Imagem);
                    query.Parameters.AddWithValue("@MarcaId", Produto.Marca.Id);
                    query.Parameters.AddWithValue("@TipoProdutoId", Produto.TipoProduto.Id);
                    query.Parameters.AddWithValue("@GrupoPrecificacaoId", Produto.GrupoPrecificacao.Id);
                    query.Parameters.AddWithValue("@FornecedorId", Produto.Fornecedor.Id);
                    using(SqlDataReader reader = query.ExecuteReader()) {
                        while (reader.Read()) {
                            Produto.Id = reader.GetInt32(reader.GetOrdinal("PRD_ID"));
                        }
                    }
                }
            }
        }

        public List<EntidadeDominio> Select() {
            _database = new DAOSQLServer();

            string Select = "SELECT * FROM Vw_Produto;";

            List<EntidadeDominio> ListProduto = new();

            using (SqlConnection conn = _database.OpenConnection()) {
                using (SqlCommand query = new(Select, conn)) {
                    using (SqlDataReader reader = query.ExecuteReader()) {
                        while (reader.Read()) {
                            ListProduto.Add(new Produto {
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
                            });
                        }
                    }
                }
            }

            return ListProduto;
        }

        public bool Update(EntidadeDominio entidade) {
            _database = new DAOSQLServer();

            string Update = @"UPDATE PRODUTOS SET PRD_NOME = @Nome WHERE PRD_ID = @Id;
                              UPDATE PRODUTOS SET PRD_PRECO = @Preco WHERE PRD_ID = @Id;
                              UPDATE PRODUTOS SET PRD_DESCRICAO = @Descricao WHERE PRD_ID = @Id;
                              UPDATE PRODUTOS SET PRD_STATUS = @Status WHERE PRD_ID = @Id;
                              UPDATE PRODUTOS SET PRD_URL_IMG = @URLImg WHERE PRD_ID = @Id;
                              UPDATE PRODUTOS SET PRD_MRC_ID = @MarcaId WHERE PRD_ID = @Id;
                              UPDATE PRODUTOS SET PRD_TPP_ID = @TipoProdutoId WHERE PRD_ID = @Id;
                              UPDATE PRODUTOS SET PRD_GRP_ID = @GrupoPrecificacaoId WHERE PRD_ID = @Id;
                              UPDATE PRODUTOS SET PRD_FRD_ID = @FornecedorId WHERE PRD_ID = @Id;";

            Produto Produto = (Produto)entidade;

            try {
                using (SqlConnection conn = _database.OpenConnection()) {
                    using (SqlCommand query = new(Update, conn)) {
                        query.Parameters.AddWithValue("@Id", Produto.Id);
                        query.Parameters.AddWithValue("@Nome", Produto.Nome);
                        query.Parameters.AddWithValue("@Preco", Produto.Preco);
                        query.Parameters.AddWithValue("@Descricao", Produto.Descricao);
                        query.Parameters.AddWithValue("@URLImg", Produto.URL_Imagem);
                        query.Parameters.AddWithValue("@MarcaId", Produto.Marca.Id);
                        query.Parameters.AddWithValue("@Status", Produto.Status);
                        query.Parameters.AddWithValue("@TipoProdutoId", Produto.TipoProduto.Id);
                        query.Parameters.AddWithValue("@GrupoPrecificacaoId", Produto.GrupoPrecificacao.Id);
                        query.Parameters.AddWithValue("@FornecedorId", Produto.Fornecedor.Id);
                        query.ExecuteNonQuery();
                    }
                }
                return true;
            } catch (Exception ex) {
                return false;
            }
        }
    }
}
