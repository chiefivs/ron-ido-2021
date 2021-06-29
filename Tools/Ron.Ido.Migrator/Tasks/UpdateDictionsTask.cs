using Ron.Ido.EM;
using Ron.Ido.EM.Entities;
using Ron.Ido.EM.Enums;
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
            UpdateApplyPassportTypes(context);

            UpdateApplyAttachmentTypes(context);
            UpdateApplyTemplates(context);
            UpdateSchoolTypes(context);
            UpdateLegalizations(context);
            UpdateCertificateDeliveryForms(context);
            UpdateReglamentEtaps(context);

            UpdateDuplicateStatuses(context);
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
                new ApplyDeliveryForm{ Id = (long)ApplyDeliveryFormEnum.SELF, OrderNum = 1, Name = "Забрать лично", NameEng = "self"},
                new ApplyDeliveryForm{ Id = (long)ApplyDeliveryFormEnum.COURIER, OrderNum = 2, Name = "Доставка курьером (за счет заявителя)", NameEng = "courier"},
                new ApplyDeliveryForm{ Id = (long)ApplyDeliveryFormEnum.POST, OrderNum = 3, Name = "Доставка по почте (за счет Федеральной службы)", NameEng = "federal mail"}
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
                new ApplyEntryForm{ Id = (long)ApplyEntryFormEnum.SELF, Name = "лично", OrderNum = 1},
                new ApplyEntryForm{ Id = (long)ApplyEntryFormEnum.MAIL, Name = "по почте", OrderNum = 2},
                new ApplyEntryForm{ Id = (long)ApplyEntryFormEnum.CABINET, Name = "личный кабинет", OrderNum = 3},
                new ApplyEntryForm{ Id = (long)ApplyEntryFormEnum.EPGU, Name = "ЕПГУ", OrderNum = 4},
                new ApplyEntryForm{ Id = (long)ApplyEntryFormEnum.ONLINE, Name = "он-лайн", OrderNum = 5}

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

        private void UpdateApplyPassportTypes(AppDbContext context)
        {
            var list = new[]
            {
                new ApplyPassportType{ Id = 1, Code = "01" ,Name = "Паспорт гражданина СССР",  OrderNum = 1 },
                new ApplyPassportType{ Id = 2, Code = "02",Name = "Загранпаспорт гражданина СССР",  OrderNum = 2 },
                new ApplyPassportType{ Id = 3, Code = "03",Name = "Свидетельство о рождении", OrderNum = 3  },
                new ApplyPassportType{ Id = 4, Code = "04" ,Name = "Удостоверение личности офицера", OrderNum = 4 },
                new ApplyPassportType{ Id = 5, Code = "05",Name = "Справка об освобождении из места лишения свободы", OrderNum = 5 },
                new ApplyPassportType{ Id = 6, Code = "06",Name = "Паспорт Минморфлота", OrderNum = 6 },
                new ApplyPassportType{ Id = 7, Code = "07",Name = "Военный билет", OrderNum = 7 },
                new ApplyPassportType{ Id = 8, Code = "08",Name = "Временное удостоверение, выданное взамен военного билета", OrderNum = 8 },
                new ApplyPassportType{ Id = 9, Code = "09",Name = "Дипломатический паспорт", OrderNum = 9 },
                new ApplyPassportType{ Id = 10, Code = "10",Name = "Паспорт иностранного гражданина", OrderNum = 10 },
                new ApplyPassportType{ Id = 11, Code = "11",Name = "Свидетельство о рассмотрении ходатайства о признании лица беженцем на территории Российской Федерации по существу", OrderNum = 11 },
                new ApplyPassportType{ Id = 12, Code = "12",Name = "Вид на жительство в Российской Федерации", OrderNum = 12 },
                new ApplyPassportType{ Id = 13, Code = "13",Name = "Удостоверение беженца", OrderNum = 13 },
                new ApplyPassportType{ Id = 14, Code = "14",Name = "Временное удостоверение личности гражданина Российской Федерации", OrderNum = 14 },
                new ApplyPassportType{ Id = 15, Code = "15",Name = "Разрешение на временное проживание в Российской Федерации", OrderNum = 15 },
                new ApplyPassportType{ Id = 16, Code = "18",Name = "Свидетельство о предоставлении временного убежища на территории Российской Федерации (до 01.01.2013)", OrderNum = 16 },
                new ApplyPassportType{ Id = 17, Code = "21",Name = "Паспорт гражданина Российской Федерации", OrderNum = 17 },
                new ApplyPassportType{ Id = 18, Code = "22",Name = "Загранпаспорт гражданина Российской Федерации", OrderNum = 18 },
                new ApplyPassportType{ Id = 19, Code = "23",Name = "Свидетельство о рождении, выданное уполномоченным органом иностранного государства", OrderNum = 19 },
                new ApplyPassportType{ Id = 20, Code = "24",Name = "Удостоверение личности военнослужащего Российской Федерации", OrderNum = 20 },
                new ApplyPassportType{ Id = 21, Code = "26",Name = "Паспорт моряка", OrderNum = 21 },
                new ApplyPassportType{ Id = 22, Code = "27",Name = "Военный билет офицера запаса", OrderNum = 22 },
                new ApplyPassportType{ Id = 23, Code = "60",Name = "Документы, подтверждающие факт регистрации по месту жительства (пребывания)", OrderNum = 23 },
                new ApplyPassportType{ Id = 24, Code = "61",Name = "Свидетельство о регистрации по месту жительства", OrderNum = 24 },
                new ApplyPassportType{ Id = 25, Code = "62",Name = "Вид на жительство иностранного гражданина", OrderNum = 25 },
                new ApplyPassportType{ Id = 26, Code = "81",Name = "Свидетельство о смерти", OrderNum = 26 },
                new ApplyPassportType{ Id = 27, Code = "91",Name = "Иные документы", OrderNum = 27 },
                new ApplyPassportType{ Id = 28, Code = "63",Name = "Свидетельство о регистрации по месту пребывания", OrderNum = 28 },
                new ApplyPassportType{ Id = 29, Code = "19",Name = "Свидетельство о предоставлении временного убежища на территории Российской Федерации", OrderNum = 29 },
                new ApplyPassportType{ Id = 30, Code = "28",Name = "Служебный паспорт гражданина Российской Федерации", OrderNum = 30 }
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

        private void UpdateApplyAttachmentTypes(AppDbContext context)
        {
            var list = new[]
            {
                new ApplyAttachmentType{ Id = 1, Name = "Доверенность от заявителя, выданная обладателем", NameEng = "Warrant", OrderNum = 1 , Required = false, ForArchive = false, ForPortal = true },
                new ApplyAttachmentType{ Id = 2, Name = "Копия документа, удостоверяющего личность заявителя", NameEng = "Passport", OrderNum = 2 , Required = true, ForArchive = false, ForPortal = true },
                new ApplyAttachmentType{ Id = 3, Name = "Копия документа, удостоверяющего личность обладателя документа об образовании", NameEng = "", OrderNum = 3 , Required = false, ForArchive = true, ForPortal = true },
                new ApplyAttachmentType{ Id = 4, Name = "Оригинал иностранного документа об образовании, легализованного в установленном порядке (при необходимости)", NameEng = "", OrderNum = 4 , Required = false, ForArchive = true, ForPortal = true },
                new ApplyAttachmentType{ Id = 5, Name = "Оригинал приложения к иностранному документу об образовании легализованного в установленном порядке (при необходимости)	", NameEng = "", OrderNum = 5 , Required = false, ForArchive = true, ForPortal = true },
                new ApplyAttachmentType{ Id = 6, Name = "Копия документа об образовании с переводом на русский язык, заверенная надлежащим образом", NameEng = "", OrderNum = 6 , Required = false, ForArchive = true, ForPortal = true },
                new ApplyAttachmentType{ Id = 7, Name = "Копия приложения документа об образовании с переводом на русский язык, заверенная надлежащим образом", NameEng = "", OrderNum = 7 , Required = false, ForArchive = true, ForPortal = true },
                new ApplyAttachmentType{ Id = 8, Name = "Копия документа о предыдущем образовании", NameEng = "", OrderNum = 8 , Required = false, ForArchive = true, ForPortal = true },
                new ApplyAttachmentType{ Id = 9, Name = "Информация о наличии лицензии у образовательного учреждения на период обучения", NameEng = "", OrderNum = 9 , Required = false, ForArchive = true, ForPortal = true },
                new ApplyAttachmentType{ Id = 10, Name = "Информация о наличии аккредитации у образовательного учреждения на период обучения", NameEng = "", OrderNum = 10 , Required = false, ForArchive = true, ForPortal = true },
                new ApplyAttachmentType{ Id = 11, Name = "Справка из образовательного учреждения, подтверждающая факт обучения и выдачи документа иностранного государства об образовании", NameEng = "", OrderNum = 11 , Required = false, ForArchive = false, ForPortal = true },
                new ApplyAttachmentType{ Id = 12, Name = "Сведения о форме освоения образовательной программы	", NameEng = "", OrderNum = 12 , Required = false, ForArchive = false, ForPortal = true },
                new ApplyAttachmentType{ Id = 13, Name = "Сведения о профессиональной деятельности", NameEng = "", OrderNum = 13 , Required = false, ForArchive = false, ForPortal = true },
                new ApplyAttachmentType{ Id = 14, Name = "Копия свидетельства", NameEng = "", OrderNum = 3 , Required = false, ForArchive = true, ForPortal = false },
            };

            foreach (var item in list)
                context.AddEntityIfNotExists(item, entity => entity.Id == item.Id);
        }

        private void UpdateLegalizations(AppDbContext context)
        {
            var list = new[]
            {
                new Legalization{ Id = 1, Name = "Консульская легализация", Description = "Консульская легализация", OrderNum = 1},
                new Legalization{ Id = 2, Name = "Апостиль", Description = "Апостиль", OrderNum = 2},
                new Legalization{ Id = 3, Name = "В легализации не нуждается", Description = "Конвенцией о правовой помощи и правовых отношениях по гражданским, семейным и уголовным делам от 22.01.1993", OrderNum = 3},
            };

            foreach (var item in list)
                context.AddEntityIfNotExists(item, entity => entity.Id == item.Id);
        }

        private void UpdateSchoolTypes(AppDbContext context)
        {
            var list = new[]
            {
                new SchoolType{ Id = 1, Name = "Государственное", NameEng = "Governmental", OrderNum = 1},
                new SchoolType{ Id = 2, Name = "Негосударственное", NameEng = "Non govermental", OrderNum = 2}
            };

            foreach (var item in list)
                context.AddEntityIfNotExists(item, entity => entity.Id == item.Id);
        }

        private void UpdateCertificateDeliveryForms(AppDbContext context)
        {
            var list = new[]
            {
                new CertificateDeliveryForm{ Id = (long)CertificateDeliveryFormEnum.EMAIL, OrderNum = 1, Name = "Посредством электронной почты", NameEng = "By E-mail"},
                new CertificateDeliveryForm{ Id = (long)CertificateDeliveryFormEnum.PORTAL, OrderNum = 2, Name = "В личный кабинет заявителя на портале nic.gov.ru", NameEng = "To the  nic.gov.ru portal"},
                new CertificateDeliveryForm{ Id = (long)CertificateDeliveryFormEnum.EPGU, OrderNum = 3, Name = "В личный кабинет заявителя на ЕПГУ", NameEng = "To the  www.gosuslugi.ru portal"}
            };

            foreach (var item in list)
                context.AddEntityIfNotExists(item, entity => entity.Id == item.Id);
        }

        private void UpdateReglamentEtaps(AppDbContext context)
        {
            var list = new[]
            {
                new ReglamentEtap{ Id = 1, OrderNum = 1, Name = "Прием", MinTerm = 3, MaxTerm = 5, Required = true },
                new ReglamentEtap{ Id = 2, OrderNum = 2, Name = "Рассмотрение", MinTerm = 7, MaxTerm = 10, Required = true },
                new ReglamentEtap{ Id = 3, OrderNum = 5, Name = "Приостановка", MinTerm = 40, MaxTerm = 45, Required = false },
                new ReglamentEtap{ Id = 4, OrderNum = 9, Name = "Производство", MinTerm = 4, MaxTerm = 5, Required = true },
                new ReglamentEtap{ Id = 5, OrderNum = 8, Name = "Подготовка проекта акта", MinTerm = 3, MaxTerm = 5, Required = true },
                new ReglamentEtap{ Id = 6, OrderNum = 3, Name = "Экспертиза", MinTerm = 15, MaxTerm = 20, Required = true },
                new ReglamentEtap{ Id = 7, OrderNum = 4, Name = "Информационное письмо", MinTerm = 0, MaxTerm = 0, Required = false },
                new ReglamentEtap{ Id = 8, OrderNum = 7, Name = "Повторный запрос", MinTerm = 45, MaxTerm = 45, Required = false }
            };

            foreach (var item in list)
                context.AddEntityIfNotExists(item, entity => entity.Id == item.Id);
        }

        private void UpdateDuplicateStatuses(AppDbContext context)
        {
            var list = new[]
            {
                new DuplicateStatus { Id = (long)DuplicateStatusEnum.DUPLICATE_CREATED, OrderNum = 1, Name = "Заявление создано", NameEng = ""},
                new DuplicateStatus { Id = (long)DuplicateStatusEnum.DUPLICATE_APPROVED, OrderNum = 2, Name = "Заявление принято", NameEng = ""},
                new DuplicateStatus { Id = (long)DuplicateStatusEnum.DECISION_PREPARATION, OrderNum = 3, Name = "Подготовка решения", NameEng = ""},
                new DuplicateStatus { Id = (long)DuplicateStatusEnum.WAITING_PAYMENT, OrderNum = 4, Name = "Ожидание оплаты", NameEng = ""},
                new DuplicateStatus { Id = (long)DuplicateStatusEnum.READY_TO_GIVE, OrderNum = 5, Name = "Готов к выдаче", NameEng = ""},
                new DuplicateStatus { Id = (long)DuplicateStatusEnum.DECLINED_TO_GIVE, OrderNum = 6, Name = "Отказ в выдаче дубликата", NameEng = ""},
                new DuplicateStatus { Id = (long)DuplicateStatusEnum.GIVEN, OrderNum = 7, Name = "Выдан", NameEng = ""},
            };

            foreach (var item in list)
                context.AddEntityIfNotExists(item, entity => entity.Id == item.Id);
        }
    }
}
