using EletronicStore.DAO.Interface;
using EletronicStore.Models;
using Microsoft.Data.SqlClient;

namespace EletronicStore.DAO {
    public class DAOBandeiraCartao : DAOEntidadeDominio, IDAOGeneric {
        public bool Delete(EntidadeDominio entidade) {
            throw new NotImplementedException();
        }

        public void Insert(EntidadeDominio entidade) {
            throw new NotImplementedException();
        }

        public List<EntidadeDominio> Select() {
            _database = new DAOSQLServer();

            string Select = "SELECT * FROM BANDEIRAS_CARTAO";

            List<EntidadeDominio> ListBandeiraCartao = new();

            using (SqlConnection conn = _database.OpenConnection()) {
                using (SqlCommand query = new(Select, conn)) {
                    using (SqlDataReader reader = query.ExecuteReader()) {
                        while (reader.Read()) {
                            BandeiraCartao BandeiraCartao = new BandeiraCartao() {
                                Id = reader.GetInt32(reader.GetOrdinal("BDC_ID")),
                                Nome = reader.GetString(reader.GetOrdinal("BDC_NOME"))
                            };

                            ListBandeiraCartao.Add(BandeiraCartao);
                        }
                    }
                }
            }
            return ListBandeiraCartao;
        }

        public bool Update(EntidadeDominio entidade) {
            throw new NotImplementedException();
        }
    }
}
