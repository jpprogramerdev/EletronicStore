using EletronicStore.DAO.Interface;
using EletronicStore.Models;
using Microsoft.Data.SqlClient;

namespace EletronicStore.DAO {
    public class DAOTelefone : DAOEntidadeDominio, IDAOGeneric {
        public bool Delete(EntidadeDominio entidade) {
            throw new NotImplementedException();
        }

        public void Insert(EntidadeDominio entidade) {
            _database = new DAOSQLServer();

            string Insert = @"INSERT INTO TELEFONES (TLF_DDD,TLF_NUMERO,TLF_TPT_ID)
                              VALUES (@DDD, @Numero, @TipoTelefoneId);
                              SELECT SCOPE_IDENTITY();";

            Telefone Telefone = (Telefone)entidade;

            using (SqlConnection conn = _database.OpenConnection()) {
                using(SqlCommand query = new(Insert, conn)) {
                    query.Parameters.AddWithValue("@DDD",Telefone.DDD);
                    query.Parameters.AddWithValue("@Numero", Telefone.Numero);
                    query.Parameters.AddWithValue("@TipoTelefoneId", Telefone.TipoTelefone.Id);
                    Telefone.Id = Convert.ToInt32(query.ExecuteScalar());
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
