using EletronicStore.DAO.Interface;
using EletronicStore.Models;
using Microsoft.Data.SqlClient;

namespace EletronicStore.DAO {
    public class DAOTipoLogradouro : DAOEntidadeDominio, IDAOGeneric {

        public bool Delete(EntidadeDominio entidade) {
            throw new NotImplementedException();
        }

        public void Insert(EntidadeDominio entidade) {
            throw new NotImplementedException();
        }

        public List<EntidadeDominio> Select() {
            _database = new DAOSQLServer();

            string Select = "SELECT * FROM TIPOS_LOGRADOURO";

            List<EntidadeDominio> ListTipoLogradouro = new();

            using(SqlConnection conn = _database.OpenConnection()) {
                using(SqlCommand query = new(Select, conn)) {
                    using (SqlDataReader reader = query.ExecuteReader()) {
                        while (reader.Read()) {
                            TipoLogradouro Tipo = new TipoLogradouro() {
                                Id = reader.GetInt32(reader.GetOrdinal("TPL_ID")),
                                Tipo = reader.GetString(reader.GetOrdinal("TPL_TIPO"))
                            };

                            ListTipoLogradouro.Add(Tipo);
                        }
                    }
                }
            }
            return ListTipoLogradouro;
        }

        public bool Update(EntidadeDominio entidade) {
            throw new NotImplementedException();
        }
    }
}
