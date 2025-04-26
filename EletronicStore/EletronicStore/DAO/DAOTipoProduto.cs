using EletronicStore.DAO.Interface;
using EletronicStore.Models;
using Microsoft.Data.SqlClient;

namespace EletronicStore.DAO {
    public class DAOTipoProduto : DAOEntidadeDominio, IDAOGeneric {
        public bool Delete(EntidadeDominio entidade) {
            throw new NotImplementedException();
        }

        public void Insert(EntidadeDominio entidade) {
            throw new NotImplementedException();
        }

        public List<EntidadeDominio> Select() {
            _database = new DAOSQLServer();

            string Select = "SELECT * FROM TIPOS_PRODUTO;";

            List<EntidadeDominio> ListTipoProduto = new List<EntidadeDominio>();

            using (SqlConnection conn = _database.OpenConnection()) {
                using (SqlCommand query = new(Select, conn)) {
                    using (SqlDataReader reader = query.ExecuteReader()) {
                        while (reader.Read()) {
                            ListTipoProduto.Add(new TipoProduto {
                                Id = reader.GetInt32(reader.GetOrdinal("TPP_ID")),
                                Tipo = reader.GetString(reader.GetOrdinal("TPP_TIPO"))
                            });
                        }
                    }
                }
            }

            return ListTipoProduto;
        }

        public bool Update(EntidadeDominio entidade) {
            throw new NotImplementedException();
        }
    }
}
