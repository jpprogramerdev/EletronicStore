using EletronicStore.DAO.Interface;
using EletronicStore.Models;
using Microsoft.Data.SqlClient;

namespace EletronicStore.DAO {
    public class DAOGrupoPrecificacao : DAOEntidadeDominio, IDAOGeneric {
        public bool Delete(EntidadeDominio entidade) {
            throw new NotImplementedException();
        }

        public void Insert(EntidadeDominio entidade) {
            throw new NotImplementedException();
        }

        public List<EntidadeDominio> Select() {
            _database = new DAOSQLServer();

            string Select = "SELECT * FROM GRUPO_PRECIFICACAO;";

            List<EntidadeDominio> ListGrupoPrecificacao = new List<EntidadeDominio>();

            using (SqlConnection conn = _database.OpenConnection()) {
                using (SqlCommand query = new(Select, conn)) {
                    using (SqlDataReader reader = query.ExecuteReader()) {
                        while (reader.Read()) {
                            ListGrupoPrecificacao.Add(new GrupoPrecificacao() {
                                Id = reader.GetInt32(reader.GetOrdinal("GRP_ID")),
                                GrupoPrecificacaao = reader.GetString(reader.GetOrdinal("GRP_PRECIFICACAO")),
                                Margem = (double)reader.GetDecimal(reader.GetOrdinal("GRP_MARGEM"))
                            });
                        }
                    }
                }
            }

            return ListGrupoPrecificacao;
        }

        public bool Update(EntidadeDominio entidade) {
            throw new NotImplementedException();
        }
    }
}
