using EletronicStore.DAO.Interface;

namespace EletronicStore.Facade {
    public class FacadeEntidadeDominio {
        protected IDAOGeneric _dao;

        public IDAOGeneric DAO {
            get { return _dao; }
            private set { _dao = value; }
        }
    }
}
