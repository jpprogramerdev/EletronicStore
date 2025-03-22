using EletronicStore.DAO.Interface;
using EletronicStore.Models;
using Microsoft.Data.SqlClient;

namespace EletronicStore.DAO {
    public class DAOTipoUsuario : DAOEntidadeDominio, IDAOGeneric {
        public bool Delete(EntidadeDominio entidade) {
            throw new NotImplementedException();
        }

        public void Insert(EntidadeDominio entidade) {
            throw new NotImplementedException();
        }

        public List<EntidadeDominio> Select() {
            _database = new DAOSQLServer();

            string Select = "SELECT * FROM TIPOS_USUARIO;";

            List<EntidadeDominio> ListTipoUsuario = new();

            using (SqlConnection conn = _database.OpenConnection()) {
                using(SqlCommand query = new(Select, conn)) {
                    using (SqlDataReader reader = query.ExecuteReader()) {
                        while (reader.Read()) {
                            TipoUsuario Tipo = new TipoUsuario() {
                                Id = reader.GetInt32(reader.GetOrdinal("TPU_ID")),
                                Tipo = reader.GetString(reader.GetOrdinal("TPU_TIPO"))
                            };

                            ListTipoUsuario.Add(Tipo);
                        }
                    }
                }
            }
            return ListTipoUsuario;
        }

        public bool Update(EntidadeDominio entidade) {
            throw new NotImplementedException();
        }
    }
}
