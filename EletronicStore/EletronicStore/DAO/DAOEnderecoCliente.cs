using EletronicStore.Controllers;
using EletronicStore.DAO.Interface;
using EletronicStore.Models;
using EletronicStore.ViewModel;
using Microsoft.Data.SqlClient;

namespace EletronicStore.DAO {
    public class DAOEnderecoCliente : DAOEntidadeDominio, IDAOGeneric {
        public bool Delete(EntidadeDominio entidade) {
            _database = new DAOSQLServer();

            string Delete = "DELETE USUARIOS_ENDERECOS WHERE USEN_USU_ID = @UsuarioId AND USEN_END_ID = @EnderecoId;";

            FormCadCliente formCadCliente = (FormCadCliente)entidade;

            try {
                using (SqlConnection conn = _database.OpenConnection()) {
                    using (SqlCommand query = new(Delete, conn)) {
                        query.Parameters.AddWithValue("@UsuarioId", formCadCliente.Cliente.Id);
                        query.Parameters.AddWithValue("@EnderecoId", formCadCliente.Endereco.Id);

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

            string Insert = "INSERT INTO USUARIOS_ENDERECOS (USEN_USU_ID, USEN_END_ID, USEN_COBRANCA, USEN_ENTREGA) VALUES (@UsuarioId, @EnderecoId, @Cobranca, @Entrega);";

            FormCadCliente formCadCliente = (FormCadCliente)entidade;

            using (SqlConnection conn = _database.OpenConnection()) {
                using (SqlCommand query = new(Insert, conn)) {
                    query.Parameters.AddWithValue("@UsuarioId",formCadCliente.Cliente.Id);
                    query.Parameters.AddWithValue("@EnderecoId", formCadCliente.Endereco.Id);
                    query.Parameters.AddWithValue("@Cobranca", formCadCliente.Endereco.Cobranca);
                    query.Parameters.AddWithValue("@Entrega", formCadCliente.Endereco.Entrega);

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
