using EletronicStore.DAO.Interface;
using EletronicStore.Models;
using EletronicStore.ViewModel;
using Microsoft.Data.SqlClient;

namespace EletronicStore.DAO {
    public class DAOProdutoInativado : DAOEntidadeDominio, IDAOGeneric {
        public bool Delete(EntidadeDominio entidade) {
            _database = new DAOSQLServer();

            string UpdateInsert = @"UPDATE PRODUTOS SET PRD_STATUS = 1 WHERE PRD_ID = @ProdutoId;
                                    DELETE PRODUTOS_DESATIVADOS WHERE PDV_PRD_ID = @ProdutoId;";

            Produto Produto = (Produto)entidade;

            try {
                using (SqlConnection conn = _database.OpenConnection()) {
                    using (SqlCommand query = new(UpdateInsert, conn)) {
                        query.Parameters.AddWithValue("@ProdutoId", Produto.Id);
                        query.ExecuteNonQuery();
                    }
                }
                return true;
            }catch(Exception ex) {
                return false;
            }
        }

        public void Insert(EntidadeDominio entidade) {
            _database = new DAOSQLServer();

            string UpdateInsert =  @"UPDATE PRODUTOS SET PRD_STATUS = 0 WHERE PRD_ID = @ProdutoId;
                               INSERT INTO PRODUTOS_DESATIVADOS(PDV_JUSTIFICATIVA, PDV_PRD_ID) VALUES (@Motivo, @ProdutoId);";

            FormInativacaoProduto ProdutoInativado = (FormInativacaoProduto)entidade;

            using (SqlConnection conn = _database.OpenConnection()) {
                using(SqlCommand query = new(UpdateInsert, conn)) {
                    query.Parameters.AddWithValue("@ProdutoId", ProdutoInativado.Produto.Id);
                    query.Parameters.AddWithValue("@Motivo", ProdutoInativado.Motivo);
                    query.ExecuteNonQuery();
                }
            }
        }

        public List<EntidadeDominio> Select() {
            throw new NotImplementedException();
        }

        public bool Update(EntidadeDominio entidade) {
            throw new NotImplementedException();
        }
    }
}
