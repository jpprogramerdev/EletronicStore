using EletronicStore.DAO.Interface;
using EletronicStore.Models;
using Microsoft.Data.SqlClient;

namespace EletronicStore.DAO {
    public class DAOTipoResidencia : DAOEntidadeDominio, IDAOGeneric {
        public bool Delete(EntidadeDominio entidade) {
            throw new NotImplementedException();
        }

        public void Insert(EntidadeDominio entidade) {
            throw new NotImplementedException();
        }

        public List<EntidadeDominio> Select() {
            _database = new DAOSQLServer();

            string Select = "SELECT * FROM TIPOS_RESIDENCIA";

            List<EntidadeDominio> ListTipoResidencia = new();

            using (SqlConnection conn = _database.OpenConnection()) {
                using (SqlCommand query = new(Select, conn)) {
                    using (SqlDataReader reader = query.ExecuteReader()) {
                        while (reader.Read()) {
                            TipoResidencia Tipo = new TipoResidencia() {
                                Id = reader.GetInt32(reader.GetOrdinal("TPR_ID")),
                                Tipo = reader.GetString(reader.GetOrdinal("TPR_TIPO"))
                            };

                            ListTipoResidencia.Add(Tipo);
                        }
                    }
                }
            }
            return ListTipoResidencia;
        }

        public bool Update(EntidadeDominio entidade) {
            throw new NotImplementedException();
        }
    }
}
