using EletronicStore.DAO.Interface;
using EletronicStore.Models;
using EletronicStore.ViewModel;
using Microsoft.Data.SqlClient;

namespace EletronicStore.DAO {
    public class DAOCartaoCliente : DAOEntidadeDominio, IDAOGeneric {
        public bool Delete(EntidadeDominio entidade) {
            _database = new DAOSQLServer();

            string Delete = "DELETE USUARIOS_CARTOES WHERE USCT_CRT_ID = @CartaoId AND USCT_USU_ID = @UsuarioId";

            FormCadCliente CartaoCliente = (FormCadCliente)entidade;

            try {
                using (SqlConnection conn = _database.OpenConnection()) {
                    using (SqlCommand query = new(Delete, conn)) {
                        query.Parameters.AddWithValue("@CartaoId", CartaoCliente.Cartao.Id);
                        query.Parameters.AddWithValue("@UsuarioId", CartaoCliente.Cliente.Id);
                        query.ExecuteNonQuery();
                    }
                }
                return true;
            }catch (Exception ex) {
                return false;
            }
        }

        public void Insert(EntidadeDominio entidade) {
            _database = new DAOSQLServer();

            string Insert = @"INSERT INTO USUARIOS_CARTOES(USCT_CRT_ID, USCT_USU_ID) VALUES (@CartaoId, @UsuarioId);";

            FormCadCliente CartaoCliente = (FormCadCliente)entidade;

            using (SqlConnection conn = _database.OpenConnection()) {
                using (SqlCommand query = new(Insert, conn)) {
                    query.Parameters.AddWithValue("@CartaoId", CartaoCliente.Cartao.Id);
                    query.Parameters.AddWithValue("@UsuarioId", CartaoCliente.Cliente.Id);
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
