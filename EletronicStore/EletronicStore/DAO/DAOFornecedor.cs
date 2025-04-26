using EletronicStore.DAO.Interface;
using EletronicStore.Models;
using Microsoft.Data.SqlClient;

namespace EletronicStore.DAO {
    public class DAOFornecedor : DAOEntidadeDominio, IDAOGeneric {
        public bool Delete(EntidadeDominio entidade) {
            throw new NotImplementedException();
        }

        public void Insert(EntidadeDominio entidade) {
            _database = new DAOSQLServer();

            string Insert = @"INSERT INTO FORNECEDORES (FRD_NOME) VALUES (@Nome);
                              SELECT FRD_ID FROM FORNECEDORES WHERE FRD_NOME = @Nome;";

            Fornecedor Fornecedor = (Fornecedor)entidade;

            using (SqlConnection conn = _database.OpenConnection()) {
                using (SqlCommand query = new(Insert, conn)) {
                    query.Parameters.AddWithValue("@Nome", Fornecedor.Nome.ToUpper().Trim());
                    using (SqlDataReader reader = query.ExecuteReader()) {
                        while (reader.Read()) {
                            Fornecedor.Id = reader.GetInt32(reader.GetOrdinal("FRD_ID"));
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
