using System;

namespace Ron.Ido.Migrator.Base
{
    public class UpdateTaskAttribute : Attribute
    {
        public int OrderNum { get; private set; }

        public UpdateTaskAttribute(int ordernum)
        {
            OrderNum = ordernum;
        }
    }
}
