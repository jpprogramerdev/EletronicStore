using EletronicStore.DAO.Interface;
using EletronicStore.Models;
using Microsoft.Data.SqlClient;

namespace EletronicStore.DAO {
    public class DAOCartaoPedido : DAOEntidadeDominio, IDAOGeneric {
        public bool Delete(EntidadeDominio entidade) {
            throw new NotImplementedException();
        }

        public void Insert(EntidadeDominio entidade) {
            _database = new DAOSQLServer();

            string Insert = "INSERT INTO CARTOES_PEDIDOS(CTPD_CRT_ID,CTPD_PDD_ID) VALUES (@CartoeId, @PedidoId);";

            Pedido Pedido = (Pedido)entidade;

            using (SqlConnection conn = _database.OpenConnection()) {
                using (SqlCommand query = new(Insert, conn)) {
                    query.Parameters.AddWithValue("@CartoeId", Pedido.Cartao.Id);
                    query.Parameters.AddWithValue("@PedidoId", Pedido.Id);
                    query.ExecuteNonQuery();
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
