using EletronicStore.DAO.Interface;
using EletronicStore.Models;
using Microsoft.Data.SqlClient;

namespace EletronicStore.DAO {
    public class DAOTipoTelefone : DAOEntidadeDominio, IDAOGeneric {
        public bool Delete(EntidadeDominio entidade) {
            throw new NotImplementedException();
        }

        public void Insert(EntidadeDominio entidade) {
            throw new NotImplementedException();
        }

        public List<EntidadeDominio> Select() {
            _database = new DAOSQLServer();

            string Select = "SELECT * FROM TIPOS_TELEFONE;";

            List<EntidadeDominio> ListTiposTelefone = new();

            using (SqlConnection conn = _database.OpenConnection()) {
                using (SqlCommand query = new(Select, conn)){
                    using (SqlDataReader reader = query.ExecuteReader()) {
                        while (reader.Read()) {
                            TipoTelefone Tipo = new TipoTelefone() {
                                Id = reader.GetInt32(reader.GetOrdinal("TPT_ID")),
                                Tipo = reader.GetString(reader.GetOrdinal("TPT_TIPO"))
                            };

                            ListTiposTelefone.Add(Tipo);
                        }
                    }
                }
            }
            return ListTiposTelefone;
        }

        public bool Update(EntidadeDominio entidade) {
            throw new NotImplementedException();
        }
    }
}
