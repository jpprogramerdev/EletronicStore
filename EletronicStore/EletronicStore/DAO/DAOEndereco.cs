using EletronicStore.DAO.Interface;
using EletronicStore.Models;
using Microsoft.Data.SqlClient;

namespace EletronicStore.DAO {
    public class DAOEndereco : DAOEntidadeDominio, IDAOGeneric {
        public bool Delete(EntidadeDominio entidade) {
            _database = new DAOSQLServer();

            string Delete = "DELETE ENDERECOS WHERE END_ID = @EnderecoId";

            Endereco Endereco = (Endereco)entidade;

            try {
                using (SqlConnection conn = _database.OpenConnection()) {
                    using (SqlCommand query = new(Delete, conn)) {
                        query.Parameters.AddWithValue("@EnderecoId", Endereco.Id);
                        query.ExecuteNonQuery();
                    }
                }
                return true;
            }catch(Exception ex) {
                return false;
            }
        }

        public void Insert(EntidadeDominio entidade) {
            _database = new DAOSQLServer();

            string Insert = @"
                            INSERT INTO ENDERECOS (END_LOGRADOURO, END_NUMERO, END_CEP, END_OBSERVACAO, END_APELIDO, END_BAIRRO, END_TPR_ID, END_TPL_ID, END_MUC_ID) 
                            VALUES (@Logradouro, @Numero, @CEP, @Observacao, @Apelido, @Bairro, @TipoResidenciaId, @TipoLogradouroId, @MunicipioId);

                            SELECT END_ID FROM ENDERECOS 
                            WHERE END_LOGRADOURO = @Logradouro 
                            AND END_NUMERO = @Numero 
                            AND END_CEP = @CEP 
                            AND (@Observacao IS NOT NULL AND END_OBSERVACAO = @Observacao OR @Observacao IS NULL AND END_OBSERVACAO IS NULL) 
                            AND END_APELIDO = @Apelido 
                            AND END_BAIRRO = @Bairro 
                            AND END_TPR_ID = @TipoResidenciaId 
                            AND END_TPL_ID = @TipoLogradouroId 
                            AND END_MUC_ID = @MunicipioId;";

            Endereco Endereco = (Endereco)entidade;

            using (SqlConnection conn = _database.OpenConnection()) {
                using (SqlCommand query = new(Insert, conn)) {
                    query.Parameters.AddWithValue("@Logradouro", Endereco.Logradouro);
                    query.Parameters.AddWithValue("@Numero", Endereco.Numero);
                    query.Parameters.AddWithValue("@CEP", Endereco.CEP);
                    query.Parameters.AddWithValue("@Observacao", string.IsNullOrEmpty(Endereco.Observacao) ? DBNull.Value : Endereco.Observacao);
                    query.Parameters.AddWithValue("@Apelido", Endereco.Apelido);
                    query.Parameters.AddWithValue("@Bairro", Endereco.Bairro);
                    query.Parameters.AddWithValue("@TipoResidenciaId", Endereco.TipoResidencia.Id);
                    query.Parameters.AddWithValue("@TipoLogradouroId", Endereco.TipoLogradouro.Id);
                    query.Parameters.AddWithValue("@MunicipioId", Endereco.Municipio.Id);

                    using (SqlDataReader reader = query.ExecuteReader()) {
                        while (reader.Read()) {
                            Endereco.Id = reader.GetInt32(reader.GetOrdinal("END_ID"));
                        }
                    }
                }
            }
        }

        public List<EntidadeDominio> Select() {
            _database = new DAOSQLServer();

            string Select = "SELECT * FROM Vw_Endereco;";

            List<EntidadeDominio> ListEndereco = new List<EntidadeDominio>();

            using (SqlConnection conn = _database.OpenConnection()) {
                using (SqlCommand query = new(Select, conn)) {
                    using(SqlDataReader reader = query.ExecuteReader()) {
                        while (reader.Read()) {
                            Endereco Endereco = new Endereco() {
                                Id = reader.GetInt32(reader.GetOrdinal("END_ID")),
                                CEP = reader.GetString(reader.GetOrdinal("END_CEP")),
                                Logradouro = reader.GetString(reader.GetOrdinal("END_LOGRADOURO")),
                                Numero = reader.GetString(reader.GetOrdinal("END_NUMERO")),
                                Bairro = reader.GetString(reader.GetOrdinal("END_BAIRRO")),
                                Apelido = reader.GetString(reader.GetOrdinal("END_APELIDO")),
                                Cobranca = reader.GetBoolean(reader.GetOrdinal("USEN_COBRANCA")),
                                Entrega = reader.GetBoolean(reader.GetOrdinal("USEN_ENTREGA")),

                                TipoLogradouro = new TipoLogradouro() {
                                    Id = reader.GetInt32(reader.GetOrdinal("TPL_ID")),
                                    Tipo = reader.GetString(reader.GetOrdinal("TPL_TIPO"))
                                },

                                TipoResidencia = new TipoResidencia() {
                                    Id = reader.GetInt32(reader.GetOrdinal("TPR_ID")),
                                    Tipo = reader.GetString(reader.GetOrdinal("TPR_TIPO"))
                                },

                                Municipio = new Municipio() {
                                    Id = reader.GetInt32(reader.GetOrdinal("MUC_ID")),
                                    Nome = reader.GetString(reader.GetOrdinal("MUC_NOME")),
                                    Estado = new Estado() {
                                        Id = reader.GetInt32(reader.GetOrdinal("EST_ID")),
                                        Nome = reader.GetString(reader.GetOrdinal("EST_NOME")),
                                        Pais = new Pais() {
                                            Id = reader.GetInt32(reader.GetOrdinal("PAS_ID")),
                                            Nome = reader.GetString(reader.GetOrdinal("PAS_NOME"))
                                        }
                                    }
                                },

                                Observacao = reader.IsDBNull(reader.GetOrdinal("END_OBSERVACAO")) ? "" : reader.GetString(reader.GetOrdinal("END_OBSERVACAO"))
                            };

                            ListEndereco.Add(Endereco);
                        }
                    }
                }
            }
            return ListEndereco;
        }

        public bool Update(EntidadeDominio entidade) {
            throw new NotImplementedException();
        }
    }
}
