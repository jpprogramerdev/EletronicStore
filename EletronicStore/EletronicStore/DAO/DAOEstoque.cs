using EletronicStore.DAO.Interface;
using EletronicStore.Models;
using Microsoft.Data.SqlClient;

namespace EletronicStore.DAO {
    public class DAOEstoque : DAOEntidadeDominio, IDAOGeneric {
        public bool Delete(EntidadeDominio entidade) {
            throw new NotImplementedException();
        }

        public void Insert(EntidadeDominio entidade) {
            _database = new DAOSQLServer();

            string Insert = @"INSERT INTO ESTOQUE (ETQ_QUANTIDADE, ETQ_PRD_ID) VALUES (@Quantidade, @ProdutoId);";

            Produto Produto = (Produto)entidade;

            using (SqlConnection conn = _database.OpenConnection()) {
                using (SqlCommand query = new(Insert, conn)) {
                    query.Parameters.AddWithValue("@Quantidade", Produto.QuantidadeEstoque);
                    query.Parameters.AddWithValue("@ProdutoId", Produto.Id);
                    query.ExecuteNonQuery();
                }
            }
        }

        public List<EntidadeDominio> Select() {
            throw new NotImplementedException();
        }

        public bool Update(EntidadeDominio entidade) {
            _database = new DAOSQLServer();

            string Update = @"UPDATE ESTOQUE SET ETQ_QUANTIDADE = @Quantidade WHERE ETQ_PRD_ID = @ProdutoId";

            Produto Produto = (Produto)entidade;

            try {
                using (SqlConnection conn = _database.OpenConnection()) {
                    using (SqlCommand query = new(Update, conn)) {
                        query.Parameters.AddWithValue("@Quantidade", Produto.QuantidadeEstoque);
                        query.Parameters.AddWithValue("@ProdutoId", Produto.Id);
                        query.ExecuteNonQuery();
                    }
                }
                return true;
            }catch(Exception ex) {
                return false;
            }
        }
    }
}
