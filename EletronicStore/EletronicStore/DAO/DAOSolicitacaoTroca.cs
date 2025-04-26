using EletronicStore.DAO.Interface;
using EletronicStore.Models;
using EletronicStore.ViewModel;
using Microsoft.Data.SqlClient;

namespace EletronicStore.DAO {
    public class DAOSolicitacaoTroca : DAOEntidadeDominio, IDAOGeneric {
        public bool Delete(EntidadeDominio entidade) {
            _database = new DAOSQLServer();

            string Delete = "DELETE SOLICITACOES_TROCA WHERE SLT_PDD_ID = @PedidoId";

            Pedido Pedido = (Pedido)entidade;

            using (SqlConnection conn = _database.OpenConnection()) {
                using (SqlCommand query = new(Delete, conn)) {
                    query.Parameters.AddWithValue("@PedidoId", Pedido.Id);
                    query.ExecuteNonQuery();
                }
            }

            return true;
        }

        public void Insert(EntidadeDominio entidade) {
            _database = new DAOSQLServer();

            string Insert = "INSERT INTO SOLICITACOES_TROCA(SLT_DATA_SOLICITACAO, SLT_MOTIVO, SLT_QUANTIDADE, SLT_PRD_ID, SLT_PDD_ID) VALUES (@Data, @Motivo, @Quantidade, @ProdutoId, @PedidoId)";

            SolicitacaoTroca SolicitacaoTroca = (SolicitacaoTroca)entidade;

            using (SqlConnection conn = _database.OpenConnection()) {
                using (SqlCommand query = new(Insert, conn)) {
                    foreach (ProdutoTroca Produto in SolicitacaoTroca.Produtos.Where(p => p.SoliciarTrocar)) {
                        query.Parameters.Clear();
                        query.Parameters.AddWithValue("@Data", DateTime.Now);
                        query.Parameters.AddWithValue("@Motivo",Produto.Motivo);
                        query.Parameters.AddWithValue("@Quantidade", Produto.QuantidadeTroca);
                        query.Parameters.AddWithValue("@ProdutoId",Produto.Produto.Id);
                        query.Parameters.AddWithValue("@PedidoId",SolicitacaoTroca.PedidoId);
                        query.ExecuteNonQuery();
                    }
                }
            }
        }

        public List<EntidadeDominio> Select() {
            _database = new DAOSQLServer();

            string Select = "SELECT * FROM Vw_SolicitacaoTroca";

            List<EntidadeDominio> ListSolicitacao = new();
            Dictionary<int, SolicitacaoTroca> MapSolicitacoes = new(); // esse map serve para evitar repetições de pedidos, já que no banco de dados é armazenado cada produto do edido que tenha sido solicitado troca

            using (SqlConnection conn = _database.OpenConnection()) {
                using (SqlCommand query = new(Select, conn)) {
                    using (SqlDataReader reader = query.ExecuteReader()) {
                        while (reader.Read()) {
                            SolicitacaoTroca SolicitacaoTroca = new();
                            SolicitacaoTroca.PedidoId = reader.GetInt32(reader.GetOrdinal("SLT_PDD_ID"));

                            if (!MapSolicitacoes.ContainsKey(SolicitacaoTroca.PedidoId)) {
                                SolicitacaoTroca.DataSolicitacao = reader.GetDateTime(reader.GetOrdinal("SLT_DATA_SOLICITACAO"));

                                MapSolicitacoes[SolicitacaoTroca.PedidoId] = SolicitacaoTroca;
                            }
                            SolicitacaoTroca = MapSolicitacoes[SolicitacaoTroca.PedidoId];

                            SolicitacaoTroca.Usuario = new Usuario() {
                                Nome = reader.GetString(reader.GetOrdinal("USU_NOME")),
                                Email = reader.GetString(reader.GetOrdinal("USU_EMAIL"))
                            };

                            SolicitacaoTroca.Produtos.Add(new ProdutoTroca() {
                                Motivo = reader.GetString(reader.GetOrdinal("SLT_MOTIVO")),
                                QuantidadeTroca = reader.GetInt32(reader.GetOrdinal("SLT_QUANTIDADE")),
                                Produto = new Produto() {
                                    Nome = reader.GetString(reader.GetOrdinal("PRD_NOME")),
                                    Descricao = reader.GetString(reader.GetOrdinal("PRD_DESCRICAO")),
                                    URL_Imagem = reader.GetString(reader.GetOrdinal("PRD_URL_IMG")),
                                    Marca = new Marca() {
                                        Nome = reader.GetString(reader.GetOrdinal("MRC_NOME")),
                                    },
                                    TipoProduto = new TipoProduto() {
                                        Tipo = reader.GetString(reader.GetOrdinal("TPP_TIPO"))
                                    },
                                    GrupoPrecificacao = new GrupoPrecificacao() {
                                        GrupoPrecificacaao = reader.GetString(reader.GetOrdinal("GRP_PRECIFICACAO")),
                                        Margem = (double)reader.GetDecimal(reader.GetOrdinal("GRP_MARGEM"))
                                    }
                                }
                            });

                        }
                        ListSolicitacao.AddRange(MapSolicitacoes.Values);
                    }
                }
            }

            return ListSolicitacao;
        }

        public bool Update(EntidadeDominio entidade) {
            throw new NotImplementedException();
        }
    }
}
