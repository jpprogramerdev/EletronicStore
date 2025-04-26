using EletronicStore.DAO.Interface;
using EletronicStore.Models;
using Microsoft.Data.SqlClient;

namespace EletronicStore.DAO {
    public class DAOMarca : DAOEntidadeDominio, IDAOGeneric {
        public bool Delete(EntidadeDominio entidade) {
            throw new NotImplementedException();
        }

        public void Insert(EntidadeDominio entidade) {
            _database = new DAOSQLServer();

            string Insert = @"INSERT INTO MARCAS (MRC_NOME) VALUES (@Nome);
                              SELECT MRC_ID FROM MARCAS WHERE MRC_NOME = @Nome;";

            Marca Marca = (Marca)entidade;

            using(SqlConnection conn = _database.OpenConnection()) {
                using(SqlCommand query = new(Insert, conn)) {
                    query.Parameters.AddWithValue("@Nome", Marca.Nome.ToUpper().Trim());
                    using (SqlDataReader reader = query.ExecuteReader()) {
                        while (reader.Read()) {
                            Marca.Id = reader.GetInt32(reader.GetOrdinal("MRC_ID"));
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
