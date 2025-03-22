using EletronicStore.DAO.Interface;

namespace EletronicStore.DAO {
    public class DAOEntidadeDominio {
        protected IDAODatabase _database;

        public IDAODatabase Database {
            get { return _database; }
            set { _database = value; }
        }

    }
}
