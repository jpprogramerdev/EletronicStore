using EletronicStore.DAO.Interface;
using EletronicStore.Models;
using Microsoft.Data.SqlClient;

namespace EletronicStore.DAO {
    public class DAOCartao : DAOEntidadeDominio, IDAOGeneric {
        public bool Delete(EntidadeDominio entidade) {
            _database = new DAOSQLServer();

            string Delete = "DELETE CARTOES WHERE CRT_ID = @CartaoId";

            Cartao Cartao = (Cartao)entidade;

            try {
                using (SqlConnection conn = _database.OpenConnection()) {
                    using (SqlCommand query = new(Delete, conn)) {
                        query.Parameters.AddWithValue("@CartaoId", Cartao.Id);
                        query.ExecuteNonQuery();
                    }
                }
                return true;
            }catch (Exception ex) {
                return false;
            }
        }

        public void Insert(EntidadeDominio entidade) {
            _database = new DAOSQLServer();

            string Insert = @"INSERT INTO CARTOES(CRT_NUMERO, CRT_CVV, CRT_APELIDO, CRT_BDC_ID, CRT_FUNCAO)
                              VALUES (@Numero, @CVV, @Apelido, @BandeiraCartaoId, @Funcao);
                              SELECT CRT_ID FROM CARTOES WHERE CRT_NUMERO = @Numero AND CRT_CVV = @CVV AND CRT_APELIDO = @Apelido AND CRT_BDC_ID = @BandeiraCartaoId AND CRT_FUNCAO = @Funcao; ";

            Cartao Cartao = (Cartao)entidade;

            using (SqlConnection conn = _database.OpenConnection()) {
                using(SqlCommand query = new(Insert, conn)) {
                    query.Parameters.AddWithValue("@Numero",Cartao.Numero);
                    query.Parameters.AddWithValue("@CVV", Cartao.CVV);
                    query.Parameters.AddWithValue("@Apelido", Cartao.Apelido);
                    query.Parameters.AddWithValue("@BandeiraCartaoId", Cartao.Bandeira.Id);
                    query.Parameters.AddWithValue("@Funcao", Cartao.Funcao);
                    using (SqlDataReader reader = query.ExecuteReader()) {
                        while (reader.Read()) {
                            Cartao.Id = reader.GetInt32(reader.GetOrdinal("CRT_ID"));
                        }
                    }
                }
            }
        }

        public List<EntidadeDominio> Select() {
            _database = new DAOSQLServer();

            string Select = "SELECT * FROM Vw_Cartao;";

            List<EntidadeDominio> ListCartao = new();

            using (SqlConnection conn = _database.OpenConnection()) {
                using (SqlCommand query = new(Select, conn)) {
                    using (SqlDataReader reader = query.ExecuteReader()) {
                        while (reader.Read()) {
                            Cartao Cartao = new Cartao() {
                                Id = reader.GetInt32(reader.GetOrdinal("CRT_ID")),
                                Numero = reader.GetString(reader.GetOrdinal("CRT_NUMERO")),
                                CVV = reader.GetString(reader.GetOrdinal("CRT_CVV")),
                                Apelido = reader.GetString(reader.GetOrdinal("CRT_APELIDO")),
                                Funcao = reader.GetString(reader.GetOrdinal("CRT_FUNCAO")),
                                Bandeira = new BandeiraCartao() {
                                    Id = reader.GetInt32(reader.GetOrdinal("BDC_ID")),
                                    Nome = reader.GetString(reader.GetOrdinal("BDC_NOME"))
                                }
                            };

                            ListCartao.Add(Cartao);
                        }
                    }
                }
            }

            return ListCartao;
        }

        public bool Update(EntidadeDominio entidade) {
            throw new NotImplementedException();
        }
    }
}
