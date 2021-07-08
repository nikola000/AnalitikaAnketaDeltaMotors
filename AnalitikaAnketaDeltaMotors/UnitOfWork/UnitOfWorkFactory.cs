namespace UnitOfWorkExample.UnitOfWork
{
    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        public IUnitOfWork Create()
        {
            return new UnitOfWork(new DatabaseContext());
        }
    }
}