using Ron.Ido.EM;
using Ron.Ido.EM.Entities;
using Ron.Ido.Migrator.Base;
using System;

namespace Ron.Ido.Migrator.Tasks
{
    public class UpdateDictionsTask : IUpdateTask
    {
        public void Update(AppDbContext context)
        {
            UpdateApplyAims(context);
            UpdateApplyDeliveryForms(context);
            UpdateApplyDocFullPackageTypes(context);
            UpdateLearnLevels(context);
            UpdateApplyDocTypes(context);

            UpdateApplyEntryForms(context);
            UpdateApplyLearnForms(context);
            UpdateApplyTemplates(context);

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

        private void UpdateApplyDeliveryForms(AppDbContext context)
        {
            var list = new[]
            {
                new ApplyDeliveryForm{ Id = 1, OrderNum = 1, Name = "Забрать лично", NameEng = "self"},
                new ApplyDeliveryForm{ Id = 2, OrderNum = 2, Name = "Доставка курьером (за счет заявителя)", NameEng = "courier"},
                new ApplyDeliveryForm{ Id = 3, OrderNum = 3, Name = "Доставка по почте (за счет Федеральной службы)", NameEng = "federal mail"}
            };

            foreach (var item in list)
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

        private void UpdateApplyDocTypes(AppDbContext context)
        {
            var list = new[]
            {
                new ApplyDocType{ Id = 1, Name = "документ об основном общем образовании", OrderNum = 1, LearnLevelId = 1, BeginDate = null, EndDate = null, NameEng = "basic education document"},
                new ApplyDocType{ Id = 2, Name = "документ о среднем (полном) общем образовании", OrderNum = 2, LearnLevelId = 1, BeginDate = null, EndDate = null, NameEng = "middle education document" },
                new ApplyDocType{ Id = 3, Name = "документ о периоде обучения по программе общего образования", OrderNum = 3, LearnLevelId = 1, BeginDate = null, EndDate = null, NameEng = null },
                new ApplyDocType{ Id = 4, Name = "документ о начальном профессиональном образовании", OrderNum = 3, LearnLevelId = 2, BeginDate = null, EndDate = new DateTime(2014,8,14,13,0,0,0)/*"2014-08-14 13:00:00.000"*/, NameEng = "NULL" },
                new ApplyDocType{ Id = 5, Name = "документ о среднем профессиональном образовании", OrderNum = 5, LearnLevelId = 3, BeginDate = null, EndDate = new DateTime(2013,9,1,0,0,0,0)/*2013-09-01 00:00:00.000*/, NameEng = "NULL" },
                new ApplyDocType{ Id = 6, Name = "документ о периоде обучения по программе среднего профессионального образования", OrderNum = 6, LearnLevelId = 3, BeginDate = null, EndDate = null, NameEng = "NULL" },
                new ApplyDocType{ Id = 7, Name = "документ о высшем профессиональном образовании", OrderNum = 7, LearnLevelId = 4, BeginDate = null, EndDate = new DateTime(2013,9,1,0,0,0,0)/*2013-09-01 00:00:00.000*/, NameEng = "NULL" },
                new ApplyDocType{ Id = 8, Name = "документ о периоде обучения по программе высшего профессионального образования", OrderNum = 8, LearnLevelId = 4, BeginDate = null, EndDate = new DateTime(2013,9,1,0,0,0,0)/*2013-09-01 00:00:00.000*/, NameEng = "NULL" },
                new ApplyDocType{ Id = 9, Name = "документ о среднем общем образовании", OrderNum = 9, LearnLevelId = 1, BeginDate = new DateTime(2013,9,1,0,0,0,0)/*2013-09-01 00:00:00.000*/, EndDate = null, NameEng = "NULL" },
                new ApplyDocType{ Id = 10, Name = "документ о среднем профессиональном образовании по программе подготовки квалифицированных рабочих, служащих", OrderNum = 10, LearnLevelId = 3, BeginDate = new DateTime(2013,9,1,0,0,0,0)/*2013-09-01 00:00:00.000*/, EndDate = null, NameEng = "NULL" },
                new ApplyDocType{ Id = 11, Name = "документ о среднем профессиональном образовании по программе подготовки специалистов среднего звена", OrderNum = 11, LearnLevelId = 3, BeginDate = new DateTime(2013,9,1,0,0,0,0)/*2013-09-01 00:00:00.000*/, EndDate = null, NameEng = "NULL" },
                new ApplyDocType{ Id = 12, Name = "документ о высшем образовании", OrderNum = 12, LearnLevelId = 6, BeginDate = new DateTime(2013,9,1,0,0,0,0)/*2013-09-01 00:00:00.000*/, EndDate = null, NameEng = "NULL" },
                new ApplyDocType{ Id = 13, Name = "документ о периоде обучения по программе высшего образования", OrderNum = 13, LearnLevelId = 6, BeginDate = new DateTime(2013,9,1,0,0,0,0)/*2013-09-01 00:00:00.000*/, EndDate = null, NameEng = "NULL" },
                new ApplyDocType{ Id = 14, Name = "документ о дополнительном профессиональном образовании", OrderNum = 14, LearnLevelId = 5, BeginDate = new DateTime(2013,9,1,0,0,0,0)/*2013-09-01 00:00:00.000*/, EndDate = null, NameEng = "NULL" },
            };

            foreach (var item in list)
                context.AddEntityIfNotExists(item, entity => entity.Id == item.Id);
        }

        private void UpdateApplyEntryForms(AppDbContext context)
        {
            var list = new[]
            {
                new ApplyEntryForm{ Id = 1, Name = "лично", OrderNum = 1},
                new ApplyEntryForm{ Id = 2, Name = "по почте", OrderNum = 2},
                new ApplyEntryForm{ Id = 3, Name = "личный кабинет", OrderNum = 3},
                new ApplyEntryForm{ Id = 4, Name = "ЕПГУ", OrderNum = 4},
                new ApplyEntryForm{ Id = 5, Name = "он-лайн", OrderNum = 5}

            };

            foreach (var item in list)
                context.AddEntityIfNotExists(item, entity => entity.Id == item.Id);
        }
        private void UpdateApplyLearnForms(AppDbContext context)
        {
            var list = new[]
            {
                new ApplyLearnForm{ Id = 1, OrderNum = 1, Name = "дневная" },
                new ApplyLearnForm{ Id = 2, OrderNum = 2, Name = "заочная" },
                new ApplyLearnForm{ Id = 3, OrderNum = 3, Name = "вечерняя" },
                new ApplyLearnForm{ Id = 4, OrderNum = 4, Name = "экстернат" },
                new ApplyLearnForm{ Id = 5, OrderNum = 5, Name = "дистанционная" }
            };

            foreach (var item in list)
                context.AddEntityIfNotExists(item, entity => entity.Id == item.Id);
        }
        private void UpdateLearnLevels(AppDbContext context)
        {
            var list = new[]
            {
                new LearnLevel{ Id = 1, Name = "ОО", OrderNum = 1, BeginDate = null, EndDate = null, FullName = "среднее общее образование" },
                new LearnLevel{ Id = 2, Name = "НПО", OrderNum = 2, BeginDate = null, EndDate = new DateTime(2014,1,1), FullName = "начальное профессиональное образование" },
                new LearnLevel{ Id = 3, Name = "СПО", OrderNum = 3, BeginDate = null, EndDate = null, FullName = "среднее профессиональное образование"  },
                new LearnLevel{ Id = 4, Name = "ВПО", OrderNum = 4, BeginDate = null, EndDate = new DateTime(2013,9,1), FullName = "высшее профессиональное образование"  },
                new LearnLevel{ Id = 5, Name = "ДВПО", OrderNum = 5, BeginDate = null, EndDate = new DateTime(2014,4,3), FullName = "дополнительное высшее профессиональное образование"  },
                new LearnLevel{ Id = 6, Name = "ВО", OrderNum = 4, BeginDate = null, EndDate = new DateTime(2013,9,1), FullName = "высшее образование"  },
                new LearnLevel{ Id = 9, Name = "ДПО", OrderNum = 6, BeginDate = null, EndDate = new DateTime(2013,9,1), FullName = "дополнительное профессиональное образование "  }

            };

            foreach (var item in list)
                context.AddEntityIfNotExists(item, entity => entity.Id == item.Id);
        }

        private void UpdateApplyTemplates(AppDbContext context)
        {
            var list = new[]
            {
                new ApplyTemplate{ Id = 1, Name = "Все предзаявки", IsDefault = true, OrderNum = 1 },
                new ApplyTemplate{ Id = 2, Name = "Предзаявки за день", IsDefault = false, OrderNum = 2 },
                new ApplyTemplate{ Id = 3, Name = "Предзаявки за предыдущий день", IsDefault = false, OrderNum = 3 },
                new ApplyTemplate{ Id = 4, Name = "Предзаявки за неделю", IsDefault = false, OrderNum = 4 },
                new ApplyTemplate{ Id = 5, Name = "Оформленные за месяц", IsDefault = false, OrderNum = 5 },
                new ApplyTemplate{ Id = 6, Name = "Все заявления", IsDefault = false, OrderNum = 6 }

            };

            foreach (var item in list)
                context.AddEntityIfNotExists(item, entity => entity.Id == item.Id);
        }

    }
}
