using Ron.Ido.EM;

namespace Ron.Ido.Migrator
{
    public interface IUpdateTask
    {
        void Update(AppDbContext context);
    }
}
