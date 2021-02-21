using Ron.Ido.EM;
using System;

namespace Ron.Ido.Migrator.Base
{
    public interface IUpdateTask
    {
        void Update(AppDbContext context);
    }
}
