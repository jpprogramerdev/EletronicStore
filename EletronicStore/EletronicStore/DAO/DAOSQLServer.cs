using EletronicStore.DAO.Interface;
using Microsoft.Data.SqlClient;

namespace EletronicStore.DAO {
    public class DAOSQLServer : IDAODatabase {
        private string _conexao;
        
        public DAOSQLServer() {
            _conexao = "Server=GORDOX\\SQLEXPRESS;Database=EletronicStore;Integrated Security=SSPI;TrustServerCertificate=True;";
        }

        public string Conexao {
            get { return _conexao; }
            set { _conexao = value; }
        }

        public void CloseConnection(SqlConnection conn) {
            conn.Close();
        }

        public SqlConnection OpenConnection() {
            SqlConnection conn = new(_conexao);
            conn.Open();
            return conn;
        }
    }
}
