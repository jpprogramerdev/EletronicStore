using EletronicStore.DAO.Interface;
using EletronicStore.Models;
using Microsoft.Data.SqlClient;

namespace EletronicStore.DAO {
    public class DAOCupom : DAOEntidadeDominio, IDAOGeneric {
        public bool Delete(EntidadeDominio entidade) {
            _database = new DAOSQLServer();

            string Delete = "DELETE CUPONS WHERE CPN_ID = @CupomId";

            Cupom Cupom = (Cupom)entidade;

            using (SqlConnection conn = _database.OpenConnection()) {
                using (SqlCommand query = new(Delete, conn)) {
                    query.Parameters.AddWithValue("@CupomId", Cupom.Id);
                    query.ExecuteNonQuery();
                }
            }

            return true;
        }

        public void Insert(EntidadeDominio entidade) {
            _database = new DAOSQLServer();

            string Insert = "INSERT INTO CUPONS(CPN_NOME, CPN_DESCONTO, CPN_USU_ID) VALUES (@Nome, @Desconto, @UsuarioId);";

            Cupom Cupom = (Cupom)entidade;

            using (SqlConnection conn = _database.OpenConnection()) {
                using (SqlCommand query = new(Insert, conn)) {
                    query.Parameters.AddWithValue("@Nome",Cupom.Nome);
                    query.Parameters.AddWithValue("@Desconto",Cupom.Desconto);
                    query.Parameters.AddWithValue("@UsuarioId",Cupom.Usuario.Id);
                    query.ExecuteNonQuery();
                }
            }
        }

        public List<EntidadeDominio> Select() {
            _database = new DAOSQLServer();

            string Select = "SELECT * FROM CUPONS;";

            List<EntidadeDominio> ListCupom = new List<EntidadeDominio>();

            using (SqlConnection conn = _database.OpenConnection()) {
                using (SqlCommand query = new(Select, conn)) {
                    using (SqlDataReader reader = query.ExecuteReader()) {
                        while (reader.Read()) {
                            ListCupom.Add(new Cupom {
                                Id = reader.GetInt32(reader.GetOrdinal("CPN_ID")),
                                Nome = reader.GetString(reader.GetOrdinal("CPN_NOME")),
                                Desconto = (double)reader.GetDecimal(reader.GetOrdinal("CPN_DESCONTO")),
                                Usuario = new Usuario { 
                                    Id = reader.IsDBNull(reader.GetOrdinal("CPN_USU_ID"))? 0 : reader.GetInt32(reader.GetOrdinal("CPN_USU_ID"))
                                }
                            });
                        }
                    }
                }
            }
            return ListCupom;
        }

        public bool Update(EntidadeDominio entidade) {
            throw new NotImplementedException();
        }
    }
}
