using EletronicStore.DAO.Interface;
using EletronicStore.Models;
using EletronicStore.ViewModel;
using Microsoft.Data.SqlClient;

namespace EletronicStore.DAO {
    public class DAOItemPedido : DAOEntidadeDominio, IDAOGeneric {
        public bool Delete(EntidadeDominio entidade) {
            throw new NotImplementedException();
        }

        public void Insert(EntidadeDominio entidade) {
            _database = new DAOSQLServer();

            string Insert = "INSERT INTO ITENS_PEDIDO (IPD_QUANTIDADE, IPD_PRD_ID, IPD_PDD_ID) VALUES (@Quantidade, @ProdutoId, @PedidoId)";

            Pedido Pedido = (Pedido)entidade;

            using (SqlConnection conn = _database.OpenConnection()) {
                using (SqlCommand query = new(Insert, conn)) {
                    foreach(ItemPedido Item in Pedido.Produtos) {
                        query.Parameters.Clear();
                        query.Parameters.AddWithValue("@Quantidade", Item.Quantidade);
                        query.Parameters.AddWithValue("@ProdutoId", Item.Id);
                        query.Parameters.AddWithValue("@PedidoId", Pedido.Id);
                        query.ExecuteNonQuery();
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
