using EletronicStore.DAO.Interface;
using EletronicStore.Models;
using Microsoft.Data.SqlClient;

namespace EletronicStore.DAO {
    public class DAOMunicipio : DAOEntidadeDominio, IDAOGeneric {
        public bool Delete(EntidadeDominio entidade) {
            throw new NotImplementedException();
        }

        public void Insert(EntidadeDominio entidade) {
            _database = new DAOSQLServer();

            string Insert = @"INSERT INTO MUNICIPIOS(MUC_NOME, MUC_EST_ID) VALUES (@Nome, @EstadoId);
                              SELECT MUC_ID FROM MUNICIPIOS WHERE MUC_NOME = @Nome AND MUC_EST_ID = @EstadoId;";

            Municipio Municipio = (Municipio)entidade;

            using (SqlConnection conn = _database.OpenConnection()) {
                using (SqlCommand query = new(Insert, conn)) {
                    query.Parameters.AddWithValue("@Nome", Municipio.Nome.ToUpper());
                    query.Parameters.AddWithValue("@EstadoId", Municipio.Estado.Id);
                    using (SqlDataReader reader = query.ExecuteReader()) {
                        while (reader.Read()) {
                            Municipio.Id = reader.GetInt32(reader.GetOrdinal("MUC_ID"));
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
