using EletronicStore.DAO.Interface;
using EletronicStore.Models;
using Microsoft.Data.SqlClient;

namespace EletronicStore.DAO {
    public class DAOEstado : DAOEntidadeDominio, IDAOGeneric {
        public bool Delete(EntidadeDominio entidade) {
            throw new NotImplementedException();
        }

        public void Insert(EntidadeDominio entidade) {
            _database = new DAOSQLServer();

            string Insert = @"INSERT INTO ESTADOS(EST_NOME, EST_PAS_ID) VALUES (@Nome, @PaisId);
                              SELECT EST_ID FROM ESTADOS WHERE EST_NOME = @Nome AND EST_PAS_ID = @PaisId";

            Estado Estado = (Estado)entidade;

            using (SqlConnection conn = _database.OpenConnection()) {
                using (SqlCommand query = new(Insert, conn)) {
                    query.Parameters.AddWithValue("@Nome", Estado.Nome);
                    query.Parameters.AddWithValue("@PaisId", Estado.Pais.Id);
                    using(SqlDataReader reader = query.ExecuteReader()) {
                        while(reader.Read()){
                            Estado.Id = reader.GetInt32(reader.GetOrdinal("EST_ID"));
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
