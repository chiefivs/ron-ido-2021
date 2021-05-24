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
            UpdateApplyPassportTypes(context);

            UpdateApplyTemplates(context);
            UpdateSchoolTypes(context);
            UpdateLegalizations(context);
            UpdateCertificateDeliveryForms(context);
            UpdateReglamentEtaps(context);
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
                new CertificateDeliveryForm{ Id = 1, OrderNum = 1, Name = "Посредством электронной почты", NameEng = "By E-mail"},
                new CertificateDeliveryForm{ Id = 2, OrderNum = 2, Name = "В личный кабинет заявителя на портале nic.gov.ru", NameEng = "To the  nic.gov.ru portal"},
                new CertificateDeliveryForm{ Id = 3, OrderNum = 3, Name = "В личный кабинет заявителя на ЕПГУ", NameEng = "To the  www.gosuslugi.ru portal"}
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

        private void UpdateApplyStatuses(AppDbContext context)
        {
            /*
    Id	Name	NameForButton	NameForApplier	DescriptionForApplier	VisibleForApplier	ViewGrants	StepGrants	StepCondition	AllowStepToStatuses	StepAction	RollbackAction	DateFieldName	Etap_Id	IsBuiltin	OrderNum	EmailNotification	NameForApplierEng	DescriptionForApplierEng
    1	не проверена		Заявление на рассмотрении	<p><span style="font-family: 'times new roman', times; font-size: medium;">Ваше заявление находится на рассмотрении к принятию по существу.</span></p>	1	E0000	E0000		2;3;5;99;101	NULL	NULL	NULL	NULL	1	1	1	Application pending	<p><span style="font-family: 'times new roman', times; font-size: medium;">Your application is pending on the merits.</span></p>
    2	содержит ошибки		Заявление содержит ошибки!	<p><strong><span style="font-size: medium; font-family: 'times new roman', times; color: #ff0000;">В процессе проверки Ваших документов были выявлены ошибки. С помощью "Личного кабинета" необходимо исправить указанные ошибки и повторно отправить заявление на проверку.</span></strong></p>	1	E0000	E0000		1;3;5	NULL	NULL	NULL	NULL	1	2	1	The application contains errors!	<p><strong><span style="font-size: medium; font-family: 'times new roman', times; color: #ff0000;">In the process of checking your documents errors were identified. Using the "Personal Account" you need to correct these errors and resubmit the application for verification.</span></strong></p>
    3	некомплект		Некомплект!	<p><span style="color: #ff0000;"><strong><span style="font-size: medium; font-family: 'times new roman', times;">При обработке Вашего заявления была выявлена некомплектность - как можно скорее свяжитесь с нами по телефону (495) <strong>317-17-10</strong>&nbsp;для прояснения ситуации.</span></strong></span></p>	1	E0000	E0000		2;5;99	NULL	NULL	NULL	NULL	1	3	1	Incompleteness!	<p>&nbsp;</p>
    <p><span style="color: #ff0000;"><strong><span style="font-size: medium; font-family: 'times new roman', times;">While processing your application, an incompleteness was revealed - as soon as possible contact us by phone (495) <strong>317-17-10</strong> to clarify the situation.</span></strong></span></p>
    4	внесены изменения		Внесены изменения заявителем		1	E0000	E0000		2;3;5;99	NULL	NULL	NULL	NULL	1	4	0	Amended by the applicant	
    5	заявление принято		Заявление принято к рассмотрению	<p><span style="font-size: medium; font-family: 'times new roman', times;">Ваше заявление принято к рассмотрению.</span></p>	1	10000E0000	E0000	StepRequiredDocsAttached	12;99	StatusApprove	NULL	AcceptDate	1	1	5	0	Application accepted for consideration	<p><span style="font-size: medium; font-family: 'times new roman', times;">Your application has been accepted for consideration.</span></p>
    10	на исследовании		На рассмотрении	<p><span style="font-size: medium; font-family: 'times new roman', times;">Ваши документы находятся на рассмотрении.</span></p>	1	B000000000	1000000000		11;13;99		NULL	NULL	6	1	6	0	Under consideration	<p><span style="font-size: medium; font-family: 'times new roman', times;">Your documents are pending.</span></p>
    11	рассмотрение завершено		На рассмотрении	<p><span style="font-family: 'times new roman', times; font-size: medium;">Ваши документы находятся на рассмотрении.</span></p>	1	1000000000	1000000000	HaveConclusionOnLastExpertize	10;14;17;23;24;99	NULL	NULL	NULL	6	1	7	0	NULL	NULL
    12	предварительная экспертиза		На рассмотрении	<p><span style="font-size: medium; font-family: 'times new roman', times;">Ваши документы находятся на рассмотрении.</span></p>	1	B000000000	1000000000		10;17;99	StatusExpertizeAssign	NULL	NULL	2	1	8	0	NULL	NULL
    13	приостановлено		Приостановлено (ожидание дополнительно информации)	<p><span style="font-size: medium;">Рассмотрение Вашего заявления приостановлено в связи с необходимостью получения дополнительной информации от третьих лиц (образовательного учреждения, государственного органа и т.п.) и&nbsp;</span><span style="font-size: medium;">будет продолжено после получения официального ответа.</span></p>	1	3060000000	3000000000		10;18;99	StatusExpertizeSuspend	NULL	NULL	3	1	9	1	Suspended (pending additional information)	<p><span style="font-size: medium;">Consideration of your application has been suspended due to the need to obtain additional information from third parties (educational institution, government agency, etc.) and will continue after receiving a formal response.</span></p>
    14	подготовка решения		Рассмотрено, оформляется официальный документ	<p><span style="font-size: medium; font-family: 'times new roman', times;">Ваше заявление рассмотрено, оформляется официальный документ.</span></p>	1	8000000000	3000000000	StepExpertizeCompleteOrNotAssign	11;15;23;24;99	NULL	NULL	NULL	5	1	10	0	Considered, issued an official document	<p><span style="font-size: medium; font-family: 'times new roman', times;">Your application has been reviewed, an official document is being drawn up.</span></p>
    15	подготовка акта		Рассмотрено, оформляется официальный документ		1	8000000000	3000000000	StepExpertizeCompleteOrNotAssign	14;16;99	NULL	NULL	NULL	5	1	11	0	NULL	NULL
    16	на визирование		Рассмотрено, оформляется официальный документ	<p><span style="font-size: medium; font-family: 'times new roman', times;">Ваше заявление рассмотрено, оформляется официальный документ.</span></p>	1	70000000000000	30000000000000		14;23;32;33	NULL	NULL	NULL	4	1	12	1	NULL	NULL
    17	информационное письмо		На рассмотрении	<p><span style="font-size: medium; font-family: 'times new roman', times;">Ваши документы находятся на рассмотрении.</span></p>	1	B000000000	1000000000		11;99		NULL	NULL	7	1	13	0	NULL	NULL
    18	повторный запрос		Приостановлено (ожидание дополнительно информации)	<p><span style="font-size: medium;">Рассмотрение Вашего заявления приостановлено в связи с необходимостью получения дополнительной информации от третьих лиц (образовательного учреждения, государственного органа и т.п.) и&nbsp;</span><span style="font-size: medium;">будет продолжено после получения официального ответа.</span></p>	1	3060000000	3000000000		10;99		NULL	NULL	8	1	14	1	NULL	NULL
    23	готов к выдаче		Готов к выдаче или отправке	<p><span style="font-size: medium; font-family: 'times new roman', times;" data-mce-mark="1">Рассмотрение Вашего заявления завершено.</span></p>
    <p><span style="font-size: medium; font-family: 'times new roman', times;">Получить готовый документ можно ПН-ЧТ с 09:30 до 17:30, в пятницу - с 9.30 до 16.30. Перерыв с 13.00 до 14.00. Адрес: Ленинский проспект, д.6, стр.3, 119049, г.Москва, Россия. При себе необходимо иметь паспорт; договор или расписку; доверенность (при необходимости). Если документ отправлялся по почте, Вы писали заявление на обратную отправку, можете самостоятельно заказать получение курьерской службой за Ваш счет (курьер должен назвать № заявления, фамилию, адрес доставки).</span></p>	1	C0000000000000	40000000000000	StepGivingAllowed	14;24;32;33;41;99	StatusStorageAssign	NULL	NULL	NULL	1	15	1	Ready to issue or send	<p><span style="font-size: medium; font-family: 'times new roman', times;" data-mce-mark="1">Your application has been completed.</span></p>
    <p><span style="font-size: medium; font-family: 'times new roman', times;">You can get the finished document Mon-Thu from 09:30 to 17:30, on Friday - from 9.30 to 16.30. Break from 13.00 to 14.00. Address: Leninsky Prospect, 6, bld. 3, 119049, Moscow, Russia. You must have a passport with you; contract or receipt; power of attorney (if necessary). If the document was sent by mail, you wrote an application for return, you can independently order the receipt of the courier service at your expense (the courier should call the application number, name, delivery address).</span></p>
    24	подготовка к выдаче		подготовка к выдаче или отправке		0	C0000000000000	40000000000000		23;32;33;99;101		NULL	NULL	NULL	1	16	0	preparation for issue or shipment	
    32	ожидание оплаты		Ожидание оплаты	<p><strong><span style="font-size: medium; font-family: 'times new roman', times; color: #ff0000;">Решение по Вашему заявлению принято, но у нас нет сведений об оплате государственной пошлины. Для получения свидетельства необходимо оплатить государственную пошлину и обратиться к нам.</span></strong></p>	1	1C0000000000000	8000000000	StepPaymentNeeded	14;16;23;33;41;99	StatusStorageAssign	NULL	NULL	NULL	1	17	1	Waiting for payment	<p><strong><span style="font-size: medium; font-family: 'times new roman', times; color: #ff0000;">The decision on your application was made, but we have no information about the payment of the state duty. To obtain a certificate, you must pay the state fee and contact us.</span></strong></p>
    33	ожидание документов	NULL	Ожидание пакета документов		1	1C0000000000000	NULL	StepDocumentsNeeded	23;32;41;99;101	NULL	NULL	NULL	NULL	1	17	1	Waiting for a package of documents	
    41	выдан		Выдан	<p>Выдан</p>	1	80000000000000	80000000000000	StepGivingAllowed;StepRequiredDocsAttached	14;23	NULL	NULL	NULL	NULL	1	22	0	Issued	<p>Issued</p>
    99	дело прекращено		Дело прекращено		0	18070B0700F0000	180000000000000			StatusClose	NULL	NULL	NULL	1	23	0	NULL	NULL
    101	повторный	NULL	дубликат заявления		1				2;3;4	NULL	NULL	NULL	NULL	0	24	0	duplicate application	         
             */

        }
    }
}
