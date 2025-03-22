using System.Data.Common;
using Microsoft.Data.SqlClient;

namespace EletronicStore.DAO.Interface {
    public interface IDAODatabase {
        public SqlConnection OpenConnection();
        public void CloseConnection(SqlConnection conn);
    }
}
