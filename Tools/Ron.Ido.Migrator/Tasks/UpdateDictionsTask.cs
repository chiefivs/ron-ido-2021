using Ron.Ido.EM;
using Ron.Ido.EM.Entities;
using Ron.Ido.Migrator.Base;

namespace Ron.Ido.Migrator.Tasks
{
    public class UpdateDictionsTask : IUpdateTask
    {
        public void Update(AppDbContext context)
        {
            UpdateApplyAims(context);
            UpdateApplyDocFullPackageTypes(context);
        }

        private void UpdateApplyAims(AppDbContext context)
        {
            var list = new[]
            {
                new ApplyAim{ Id = 1, OrderNum = 1, Name = "(АП) Продолжение обучения в Российской Федерации", NameEng = "Education"},
                new ApplyAim{ Id = 2, OrderNum = 2, Name = "(ПП) Осуществление профессиональной деятельности в Российской Федерации", NameEng = "Work"},
                new ApplyAim{ Id = 3, OrderNum = 3, Name = "(АП) и (ПП)", NameEng = "Education and work"},
                new ApplyAim{ Id = 4, OrderNum = 4, Name = "Другое", NameEng = "Other"},
            };

            foreach(var item in list)
                context.AddEntityIfNotExists(item, entity => entity.Id == item.Id);
        }

        private void UpdateApplyDocFullPackageTypes(AppDbContext context)
        {
            var list = new[]
            {
                new ApplyDocFullPackageType{ Id = 1, OrderNum = 1, Name = "оригиналы документов" },
                new ApplyDocFullPackageType{ Id = 2, OrderNum = 2, Name = "нотариальные копии документов" },
                new ApplyDocFullPackageType{ Id = 3, OrderNum = 3, Name = "полный пакет документов" },
            };

            foreach(var item in list)
                context.AddEntityIfNotExists(item, entity => entity.Id == item.Id);
        }
    }
}
