using EletronicStore.DAO.Interface;
using EletronicStore.Exceptions;
using EletronicStore.Models;
using Microsoft.Data.SqlClient;

namespace EletronicStore.DAO {
    public class DAOCliente : DAOEntidadeDominio, IDAOGeneric {

        public bool Delete(EntidadeDominio entidade) {
            throw new NotImplementedException();
        }

        public void Insert(EntidadeDominio entidade) {
            _database = new DAOSQLServer();

            string Insert = @"INSERT INTO USUARIOS(
                                USU_NOME,
                                USU_EMAIL,
                                USU_SENHA,
                                USU_CPF,
                                USU_DATA_NASCIMENTO,
                                USU_GNR_ID,
                                USU_TPU_ID,
                                USU_TLF_ID,
                                USU_STATUS
                              ) VALUES (
                                @Nome, 
                                @Email,
                                @Senha,
                                @CPF,
                                @DtNascimento,
                                (SELECT GNR_ID FROM GENEROS WHERE GNR_GENERO = @Genero),
                                (SELECT TPU_ID FROM TIPOS_USUARIO WHERE TPU_TIPO = 'CLIENTE'),
                                @TelefoneId,
                                1
                            );
                            SELECT SCOPE_IDENTITY();";

            Usuario Cliente = (Usuario)entidade;

            try {
                using (SqlConnection conn = _database.OpenConnection()) {
                    using (SqlCommand query = new(Insert, conn)) {
                        query.Parameters.AddWithValue("@Nome", Cliente.Nome);
                        query.Parameters.AddWithValue("@Email", Cliente.Email);
                        query.Parameters.AddWithValue("@Senha", Cliente.Senha);
                        query.Parameters.AddWithValue("@CPF", Cliente.CPF);
                        query.Parameters.AddWithValue("@DtNascimento", Cliente.DataNascimento);
                        query.Parameters.AddWithValue("@Genero", Cliente.Genero);
                        query.Parameters.AddWithValue("@TelefoneId", Cliente.Telefone.Id);
                        Cliente.Id = Convert.ToInt32(query.ExecuteScalar());
                    }
                }
            } catch (SqlException sqlex) when (sqlex.Number == 2627) {
                throw new UniqueCPFException();
            }
        }

        public List<EntidadeDominio> Select() {
            string Select = "SELECT * FROM Vw_DadosUsuario;";

            _database = new DAOSQLServer();

            List<EntidadeDominio> ListUsuarios = new List<EntidadeDominio>();
            Dictionary<int, Usuario> MapCliente = new Dictionary<int, Usuario>();

            using (SqlConnection conn = _database.OpenConnection()) {
                using (SqlCommand query = new(Select, conn)){
                    using (SqlDataReader reader = query.ExecuteReader()) {
                        while (reader.Read()) {
                            Usuario Cliente = new();
                            Endereco Endereco = new();
                            Cartao Cartao = new();

                            Cliente.Id = reader.GetInt32(reader.GetOrdinal("USU_ID"));
                            Endereco.Id = reader.GetInt32(reader.GetOrdinal("END_ID"));

                            if (!MapCliente.ContainsKey(Cliente.Id)) {
                                Cliente.Nome = reader.GetString(reader.GetOrdinal("USU_NOME"));
                                Cliente.CPF = reader.GetString(reader.GetOrdinal("USU_CPF"));
                                Cliente.Email = reader.GetString(reader.GetOrdinal("USU_EMAIL"));
                                Cliente.Genero = reader.GetString(reader.GetOrdinal("GNR_GENERO"));
                                Cliente.Status = reader.GetBoolean(reader.GetOrdinal("USU_STATUS"));
                                Cliente.Senha = reader.GetString(reader.GetOrdinal("USU_SENHA"));
                                Cliente.Telefone = new Telefone() {
                                    Id = reader.GetInt32(reader.GetOrdinal("TLF_ID")),
                                    DDD = reader.GetString(reader.GetOrdinal("TLF_DDD")),
                                    Numero = reader.GetString(reader.GetOrdinal("TLF_NUMERO")),
                                    TipoTelefone = new TipoTelefone() {
                                        Id = reader.GetInt32(reader.GetOrdinal("TPT_ID")),
                                        Tipo = reader.GetString(reader.GetOrdinal("TPT_TIPO"))
                                    }
                                };
                                Cliente.DataNascimento = reader.GetDateTime(reader.GetOrdinal("USU_DATA_NASCIMENTO"));
                                Cliente.TipoUsuario = new TipoUsuario() {
                                    Id = reader.GetInt32(reader.GetOrdinal("TPU_ID")),
                                    Tipo = reader.GetString(reader.GetOrdinal("TPU_TIPO"))
                                };

                                MapCliente[Cliente.Id] = Cliente;
                            }
                            Cliente = MapCliente[Cliente.Id];

                            if(!Cliente.Enderecos.Any(e => e.Id == Endereco.Id)) {
                                Endereco.CEP = reader.GetString(reader.GetOrdinal("END_CEP"));
                                Endereco.Logradouro = reader.GetString(reader.GetOrdinal("END_LOGRADOURO"));
                                Endereco.Numero = reader.GetString(reader.GetOrdinal("END_NUMERO"));
                                Endereco.Bairro = reader.GetString(reader.GetOrdinal("END_BAIRRO"));
                                Endereco.Apelido = reader.GetString(reader.GetOrdinal("END_APELIDO"));
                                Endereco.Cobranca = reader.GetBoolean(reader.GetOrdinal("USEN_COBRANCA"));
                                Endereco.Entrega = reader.GetBoolean(reader.GetOrdinal("USEN_ENTREGA"));

                                Endereco.TipoLogradouro = new TipoLogradouro() {
                                    Id = reader.GetInt32(reader.GetOrdinal("TPL_ID")),
                                    Tipo = reader.GetString(reader.GetOrdinal("TPL_TIPO"))
                                };
                                Endereco.TipoResidencia = new TipoResidencia() {
                                    Id = reader.GetInt32(reader.GetOrdinal("TPR_ID")),
                                    Tipo = reader.GetString(reader.GetOrdinal("TPR_TIPO"))
                                };
                                Endereco.Municipio = new Municipio() {
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
                                };

                                if (!reader.IsDBNull(reader.GetOrdinal("END_OBSERVACAO"))) {
                                    Endereco.Observacao = reader.GetString(reader.GetOrdinal("END_OBSERVACAO"));
                                } else {
                                    Endereco.Observacao = "";
                                }

                                Cliente.Enderecos.Add(Endereco);
                            }

                            Cartao.Id = reader.GetInt32(reader.GetOrdinal("CRT_ID"));
                            if (!Cliente.Cartoes.Any(c => c.Id == Cartao.Id)) {
                                Cartao.Numero = reader.GetString(reader.GetOrdinal("CRT_NUMERO"));
                                Cartao.CVV = reader.GetString(reader.GetOrdinal("CRT_CVV"));
                                Cartao.Apelido = reader.GetString(reader.GetOrdinal("CRT_APELIDO"));
                                Cartao.Funcao = reader.GetString(reader.GetOrdinal("CRT_FUNCAO"));
                                Cartao.Bandeira = new BandeiraCartao() {
                                    Id = reader.GetInt32(reader.GetOrdinal("BDC_ID")),
                                    Nome = reader.GetString(reader.GetOrdinal("BDC_NOME"))
                                };

                                Cliente.Cartoes.Add(Cartao);
                            }
                        }
                    }
                }
                ListUsuarios.AddRange(MapCliente.Values);
            }
            return ListUsuarios;
        }

        public bool Update(EntidadeDominio entidade) {
            _database = new DAOSQLServer();

            string Update = @"UPDATE USUARIOS 
                            SET USU_NOME = @Nome,
                                USU_EMAIL = @Email, 
                                USU_SENHA = @Senha,
                                USU_DATA_NASCIMENTO = @DtNascimento,
                                USU_GNR_ID = (SELECT GNR_ID FROM GENEROS WHERE GNR_GENERO = @Genero),
                                USU_TPU_ID = @TipoUsuarioId,
                                USU_TLF_ID = @TelefoneId,
                                USU_STATUS = @Status";


            string selectCPF = "SELECT USU_CPF FROM USUARIOS WHERE USU_ID = @Id";
            string CPFAtual = "";

            Usuario Cliente = (Usuario)entidade;

            try {
                using (SqlConnection conn = _database.OpenConnection()) {

                    using (SqlCommand queryCPF = new SqlCommand(selectCPF, conn)) {
                        queryCPF.Parameters.AddWithValue("@Id", Cliente.Id);
                        CPFAtual = (string)queryCPF.ExecuteScalar();
                    }


                    if(Cliente.CPF != CPFAtual) {
                        Update += ", USU_CPF = @CPF";
                    }
                    Update += " WHERE USU_ID = @Id";

                    using (SqlCommand query = new(Update, conn)) {
                        query.Parameters.AddWithValue("@Nome", Cliente.Nome);
                        query.Parameters.AddWithValue("@Email", Cliente.Email);
                        query.Parameters.AddWithValue("@Senha", Cliente.Senha);
                        query.Parameters.AddWithValue("@DtNascimento", Cliente.DataNascimento);
                        query.Parameters.AddWithValue("@Genero", Cliente.Genero);
                        query.Parameters.AddWithValue("@TelefoneId", Cliente.Telefone.Id);
                        query.Parameters.AddWithValue("@TipoUsuarioId", Cliente.TipoUsuario.Id);
                        query.Parameters.AddWithValue("@Status", Cliente.Status ? false : true);
                        query.Parameters.AddWithValue("@Id", Cliente.Id);

                        if(Cliente.CPF != CPFAtual) {
                            query.Parameters.AddWithValue("@CPF", Cliente.CPF);
                        }

                        query.ExecuteNonQuery();
                    }
                }
                return true;
            } catch (SqlException sqlex) when (sqlex.Number == 2627) {
                throw new UniqueCPFException();
            }
        }
    }
}
