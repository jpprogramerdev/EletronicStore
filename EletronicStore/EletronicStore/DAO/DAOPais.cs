using EletronicStore.DAO.Interface;
using EletronicStore.Models;
using Microsoft.Data.SqlClient;

namespace EletronicStore.DAO {
    public class DAOPais : DAOEntidadeDominio, IDAOGeneric {
        public bool Delete(EntidadeDominio entidade) {
            throw new NotImplementedException();
        }

        public void Insert(EntidadeDominio entidade) {
            _database = new DAOSQLServer();

            string Insert = @"INSERT INTO PAISES(PAS_NOME) VALUES (@Nome);
                              SELECT PAS_ID FROM PAISES WHERE PAS_NOME = @Nome";

            Pais Pais = (Pais)entidade;

            using (SqlConnection conn = _database.OpenConnection()) {
                using (SqlCommand query = new(Insert, conn)) {
                    query.Parameters.AddWithValue("@Nome", Pais.Nome);
                    using(SqlDataReader reader = query.ExecuteReader()) {
                        while (reader.Read()) {
                            Pais.Id = reader.GetInt32(reader.GetOrdinal("PAS_ID"));
                        }
                    }
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
