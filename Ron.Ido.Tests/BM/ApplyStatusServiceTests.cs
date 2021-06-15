using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Ron.Ido.BM.Extensions.Entities;
using Ron.Ido.BM.Services;
using Ron.Ido.EM;
using Ron.Ido.EM.Entities;
using Ron.Ido.Migrator.Tasks;
using Ron.Ido.Tests.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Ron.Ido.BM;
using Ron.Ido.EM.Enums;
using Ron.Ido.BM.Interfaces;

namespace Ron.Ido.Tests.BM
{
    [TestClass]
    public class ApplyStatusServiceTests
    {
        AppDbContext _dbContext;
        [TestInitialize]
        public void Setup()
        {
            _dbContext = new MockAppDbContext();
            var migrator = new UpdateDictionsTask();
            migrator.Update(_dbContext);
            FillStatus();
            FillApplies();
        }

        [TestMethod]
        public void TestDictions()
        {
            Assert.IsTrue(_dbContext.ApplyStatuses.Any());
        }
        [TestMethod]
        public void SimpleStatusSteps()
        {
            var notChecked = _dbContext.ApplyStatuses.Find(1L);
            var withErrors = _dbContext.ApplyStatuses.Find(2L);
            var incomplete = _dbContext.ApplyStatuses.Find(3L);
            Assert.IsNotNull(notChecked, "Статус не найден");
            Assert.IsNotNull(withErrors, "Статус не найден");
            Assert.IsNotNull(incomplete, "Статус не найден");
            Assert.IsTrue(notChecked.CanJumpTo(withErrors));
            Assert.IsFalse(incomplete.CanJumpTo(notChecked));
        }

        [TestMethod]
        public void HeavyTest()
        {
            IServiceCollection collection = new ServiceCollection();
            collection.AddTransient(typeof(AppDbContext), provider => _dbContext);
            collection.AddTransient(typeof(IStatusChecker), provider => new MockStatusChecker(_dbContext));
            collection.AddTransient<IIdentityService,MockIdentityService>();
            collection.AddMediatR(typeof(IAssemblyMarker));
            collection.AddTransient<ApplyStatusService>();
            var services = collection.BuildServiceProvider();

            var service = services.GetService<ApplyStatusService>();
            var me = services.GetService<IIdentityService>()?.Identity;

            Assert.AreEqual(service.RevertStatus(1L, "Undo"), ApplyStatusService.NoHistory);

            Assert.AreEqual(service.SetStatus(0L, 1L, "Hmm"), ApplyStatusService.ApplyNotFound);
            Assert.AreEqual(service.SetStatus(1L, 0L, "Hmm"), ApplyStatusService.StatusNotFound);
            Assert.AreEqual(service.SetStatus(1L, 5L, "Hmm"), string.Empty);
            Assert.AreEqual(service.SetStatus(1L, 19L, "Hmm"), ApplyStatusService.NotAllowed);

            var closed = _dbContext.ApplyStatuses.FirstOrDefault(stts => stts.StatusEnumValue == ApplyStatusEnum.DELETED.ToString("f"));
            Assert.AreEqual(service.SetStatus(1L, closed.Id, "Close at any"), string.Empty);

            Assert.AreEqual(service.RevertStatus(1L, "Undo"), string.Empty);
            var apply = _dbContext.Applies.Find(1L);
            Assert.AreEqual(apply.StatusId, 5L);
            Assert.AreEqual(apply.StatusHistories.Last().UserId, me?.Id);


        }
        private void FillStatus()
        {
            var json = @"[{""Id"":1,""StatusEnumValue"":""NO_VALIDATED"",""Name"":""не проверена"",""NameForButton"":"""",""NameForApplier"":""Заявление на рассмотрении"",""NameForApplierEng"":""Application pending"",""DescriptionForApplier"":""<p><span style=\""font-family: 'times new roman', times; font-size: medium;\"">Ваше заявление находится на рассмотрении к принятию по существу.</span></p>"",""DescriptionForApplierEng"":""<p><span style=\""font-family: 'times new roman', times; font-size: medium;\"">Your application is pending on the merits.</span></p>"",""VisibleForApplier"":true,""AllowStepToStatuses"":""2;3;5;20;21"",""EtapId"":null,""OldId"":1,""Etap"":null,""AppliesStatusIds"":null,""ApplyStatusHistoriesPrevStatusIds"":null,""ApplyStatusHistoriesStatusIds"":null},{""Id"":3,""StatusEnumValue"":""UNDERMANNED"",""Name"":""некомплект"",""NameForButton"":"""",""NameForApplier"":""Некомплект!"",""NameForApplierEng"":""Incompleteness!"",""DescriptionForApplier"":""<p><span style=\""color: #ff0000;\""><strong><span style=\""font-size: medium; font-family: 'times new roman', times;\"">При обработке Вашего заявления была выявлена некомплектность - как можно скорее свяжитесь с нами по телефону (495) <strong>317-17-10</strong>&nbsp;для прояснения ситуации.</span></strong></span></p>"",""DescriptionForApplierEng"":""<p>&nbsp;</p>\r\n<p><span style=\""color: #ff0000;\""><strong><span style=\""font-size: medium; font-family: 'times new roman', times;\"">While processing your application, an incompleteness was revealed - as soon as possible contact us by phone (495) <strong>317-17-10</strong> to clarify the situation.</span></strong></span></p>"",""VisibleForApplier"":true,""AllowStepToStatuses"":""2;5;20"",""EtapId"":null,""OldId"":3,""Etap"":null,""AppliesStatusIds"":null,""ApplyStatusHistoriesPrevStatusIds"":null,""ApplyStatusHistoriesStatusIds"":null},{""Id"":4,""StatusEnumValue"":""FIXED_BY_USER"",""Name"":""внесены изменения"",""NameForButton"":"""",""NameForApplier"":""Внесены изменения заявителем"",""NameForApplierEng"":""Amended by the applicant"",""DescriptionForApplier"":"""",""DescriptionForApplierEng"":"""",""VisibleForApplier"":true,""AllowStepToStatuses"":""2;3;5;20"",""EtapId"":null,""OldId"":4,""Etap"":null,""AppliesStatusIds"":null,""ApplyStatusHistoriesPrevStatusIds"":null,""ApplyStatusHistoriesStatusIds"":null},{""Id"":5,""StatusEnumValue"":""APPROVED"",""Name"":""заявление принято"",""NameForButton"":"""",""NameForApplier"":""Заявление принято к рассмотрению"",""NameForApplierEng"":""Application accepted for consideration"",""DescriptionForApplier"":""<p><span style=\""font-size: medium; font-family: 'times new roman', times;\"">Ваше заявление принято к рассмотрению.</span></p>"",""DescriptionForApplierEng"":""<p><span style=\""font-size: medium; font-family: 'times new roman', times;\"">Your application has been accepted for consideration.</span></p>"",""VisibleForApplier"":true,""AllowStepToStatuses"":""8;20"",""EtapId"":1,""OldId"":5,""Etap"":null,""AppliesStatusIds"":null,""ApplyStatusHistoriesPrevStatusIds"":null,""ApplyStatusHistoriesStatusIds"":null},{""Id"":6,""StatusEnumValue"":""ON_RESEARCH"",""Name"":""на исследовании"",""NameForButton"":"""",""NameForApplier"":""На рассмотрении"",""NameForApplierEng"":""Under consideration"",""DescriptionForApplier"":""<p><span style=\""font-size: medium; font-family: 'times new roman', times;\"">Ваши документы находятся на рассмотрении.</span></p>"",""DescriptionForApplierEng"":""<p><span style=\""font-size: medium; font-family: 'times new roman', times;\"">Your documents are pending.</span></p>"",""VisibleForApplier"":true,""AllowStepToStatuses"":""7;9;20"",""EtapId"":6,""OldId"":10,""Etap"":null,""AppliesStatusIds"":null,""ApplyStatusHistoriesPrevStatusIds"":null,""ApplyStatusHistoriesStatusIds"":null},{""Id"":7,""StatusEnumValue"":""ON_RESEARCH_END"",""Name"":""рассмотрение завершено"",""NameForButton"":"""",""NameForApplier"":""На рассмотрении"",""NameForApplierEng"":null,""DescriptionForApplier"":""<p><span style=\""font-family: 'times new roman', times; font-size: medium;\"">Ваши документы находятся на рассмотрении.</span></p>"",""DescriptionForApplierEng"":null,""VisibleForApplier"":true,""AllowStepToStatuses"":""6;10;13;15;16;20"",""EtapId"":6,""OldId"":11,""Etap"":null,""AppliesStatusIds"":null,""ApplyStatusHistoriesPrevStatusIds"":null,""ApplyStatusHistoriesStatusIds"":null},{""Id"":8,""StatusEnumValue"":""ON_EXPERTIZE"",""Name"":""предварительная экспертиза"",""NameForButton"":"""",""NameForApplier"":""На рассмотрении"",""NameForApplierEng"":null,""DescriptionForApplier"":""<p><span style=\""font-size: medium; font-family: 'times new roman', times;\"">Ваши документы находятся на рассмотрении.</span></p>"",""DescriptionForApplierEng"":null,""VisibleForApplier"":true,""AllowStepToStatuses"":""6;13;20"",""EtapId"":2,""OldId"":12,""Etap"":null,""AppliesStatusIds"":null,""ApplyStatusHistoriesPrevStatusIds"":null,""ApplyStatusHistoriesStatusIds"":null},{""Id"":10,""StatusEnumValue"":""DECISION_PREPARATION"",""Name"":""подготовка решения"",""NameForButton"":"""",""NameForApplier"":""Рассмотрено, оформляется официальный документ"",""NameForApplierEng"":""Considered, issued an official document"",""DescriptionForApplier"":""<p><span style=\""font-size: medium; font-family: 'times new roman', times;\"">Ваше заявление рассмотрено, оформляется официальный документ.</span></p>"",""DescriptionForApplierEng"":""<p><span style=\""font-size: medium; font-family: 'times new roman', times;\"">Your application has been reviewed, an official document is being drawn up.</span></p>"",""VisibleForApplier"":true,""AllowStepToStatuses"":""7;11;15;16;20"",""EtapId"":5,""OldId"":14,""Etap"":null,""AppliesStatusIds"":null,""ApplyStatusHistoriesPrevStatusIds"":null,""ApplyStatusHistoriesStatusIds"":null},{""Id"":11,""StatusEnumValue"":""DECISION_ACT"",""Name"":""подготовка акта"",""NameForButton"":"""",""NameForApplier"":""Рассмотрено, оформляется официальный документ"",""NameForApplierEng"":null,""DescriptionForApplier"":"""",""DescriptionForApplierEng"":null,""VisibleForApplier"":true,""AllowStepToStatuses"":""10;12;20"",""EtapId"":5,""OldId"":15,""Etap"":null,""AppliesStatusIds"":null,""ApplyStatusHistoriesPrevStatusIds"":null,""ApplyStatusHistoriesStatusIds"":null},{""Id"":12,""StatusEnumValue"":""SIGNING_POSTED"",""Name"":""на визирование"",""NameForButton"":"""",""NameForApplier"":""Рассмотрено, оформляется официальный документ"",""NameForApplierEng"":null,""DescriptionForApplier"":""<p><span style=\""font-size: medium; font-family: 'times new roman', times;\"">Ваше заявление рассмотрено, оформляется официальный документ.</span></p>"",""DescriptionForApplierEng"":null,""VisibleForApplier"":true,""AllowStepToStatuses"":""10;15;17;18"",""EtapId"":4,""OldId"":16,""Etap"":null,""AppliesStatusIds"":null,""ApplyStatusHistoriesPrevStatusIds"":null,""ApplyStatusHistoriesStatusIds"":null},{""Id"":13,""StatusEnumValue"":""INFO_LETTER"",""Name"":""информационное письмо"",""NameForButton"":"""",""NameForApplier"":""На рассмотрении"",""NameForApplierEng"":null,""DescriptionForApplier"":""<p><span style=\""font-size: medium; font-family: 'times new roman', times;\"">Ваши документы находятся на рассмотрении.</span></p>"",""DescriptionForApplierEng"":null,""VisibleForApplier"":true,""AllowStepToStatuses"":""7;20"",""EtapId"":7,""OldId"":17,""Etap"":null,""AppliesStatusIds"":null,""ApplyStatusHistoriesPrevStatusIds"":null,""ApplyStatusHistoriesStatusIds"":null},{""Id"":14,""StatusEnumValue"":""SECOND_REQUEST"",""Name"":""повторный запрос"",""NameForButton"":"""",""NameForApplier"":""Приостановлено (ожидание дополнительно информации)"",""NameForApplierEng"":null,""DescriptionForApplier"":""<p><span style=\""font-size: medium;\"">Рассмотрение Вашего заявления приостановлено в связи с необходимостью получения дополнительной информации от третьих лиц (образовательного учреждения, государственного органа и т.п.) и&nbsp;</span><span style=\""font-size: medium;\"">будет продолжено после получения официального ответа.</span></p>"",""DescriptionForApplierEng"":null,""VisibleForApplier"":true,""AllowStepToStatuses"":""6;20"",""EtapId"":8,""OldId"":18,""Etap"":null,""AppliesStatusIds"":null,""ApplyStatusHistoriesPrevStatusIds"":null,""ApplyStatusHistoriesStatusIds"":null},{""Id"":2,""StatusEnumValue"":""HAS_ERRORS"",""Name"":""содержит ошибки"",""NameForButton"":"""",""NameForApplier"":""Заявление содержит ошибки!"",""NameForApplierEng"":""The application contains errors!"",""DescriptionForApplier"":""<p><strong><span style=\""font-size: medium; font-family: 'times new roman', times; color: #ff0000;\"">В процессе проверки Ваших документов были выявлены ошибки. С помощью \""Личного кабинета\"" необходимо исправить указанные ошибки и повторно отправить заявление на проверку.</span></strong></p>"",""DescriptionForApplierEng"":""<p><strong><span style=\""font-size: medium; font-family: 'times new roman', times; color: #ff0000;\"">In the process of checking your documents errors were identified. Using the \""Personal Account\"" you need to correct these errors and resubmit the application for verification.</span></strong></p>"",""VisibleForApplier"":true,""AllowStepToStatuses"":""1;3;5"",""EtapId"":null,""OldId"":2,""Etap"":null,""AppliesStatusIds"":null,""ApplyStatusHistoriesPrevStatusIds"":null,""ApplyStatusHistoriesStatusIds"":null},{""Id"":9,""StatusEnumValue"":""SUSPENDED"",""Name"":""приостановлено"",""NameForButton"":"""",""NameForApplier"":""Приостановлено (ожидание дополнительно информации)"",""NameForApplierEng"":""Suspended (pending additional information)"",""DescriptionForApplier"":""<p><span style=\""font-size: medium;\"">Рассмотрение Вашего заявления приостановлено в связи с необходимостью получения дополнительной информации от третьих лиц (образовательного учреждения, государственного органа и т.п.) и&nbsp;</span><span style=\""font-size: medium;\"">будет продолжено после получения официального ответа.</span></p>"",""DescriptionForApplierEng"":""<p><span style=\""font-size: medium;\"">Consideration of your application has been suspended due to the need to obtain additional information from third parties (educational institution, government agency, etc.) and will continue after receiving a formal response.</span></p>"",""VisibleForApplier"":true,""AllowStepToStatuses"":""6;14;20"",""EtapId"":3,""OldId"":13,""Etap"":null,""AppliesStatusIds"":null,""ApplyStatusHistoriesPrevStatusIds"":null,""ApplyStatusHistoriesStatusIds"":null},{""Id"":15,""StatusEnumValue"":""READY_TO_GIVE"",""Name"":""готов к выдаче"",""NameForButton"":"""",""NameForApplier"":""Готов к выдаче или отправке"",""NameForApplierEng"":""Ready to issue or send"",""DescriptionForApplier"":""<p><span style=\""font-size: medium; font-family: 'times new roman', times;\"" data-mce-mark=\""1\"">Рассмотрение Вашего заявления завершено.</span></p>\r\n<p><span style=\""font-size: medium; font-family: 'times new roman', times;\"">Получить готовый документ можно ПН-ЧТ с 09:30 до 17:30, в пятницу - с 9.30 до 16.30. Перерыв с 13.00 до 14.00. Адрес: Ленинский проспект, д.6, стр.3, 119049, г.Москва, Россия. При себе необходимо иметь паспорт; договор или расписку; доверенность (при необходимости). Если документ отправлялся по почте, Вы писали заявление на обратную отправку, можете самостоятельно заказать получение курьерской службой за Ваш счет (курьер должен назвать № заявления, фамилию, адрес доставки).</span></p>"",""DescriptionForApplierEng"":""<p><span style=\""font-size: medium; font-family: 'times new roman', times;\"" data-mce-mark=\""1\"">Your application has been completed.</span></p>\r\n<p><span style=\""font-size: medium; font-family: 'times new roman', times;\"">You can get the finished document Mon-Thu from 09:30 to 17:30, on Friday - from 9.30 to 16.30. Break from 13.00 to 14.00. Address: Leninsky Prospect, 6, bld. 3, 119049, Moscow, Russia. You must have a passport with you; contract or receipt; power of attorney (if necessary). If the document was sent by mail, you wrote an application for return, you can independently order the receipt of the courier service at your expense (the courier should call the application number, name, delivery address).</span></p>"",""VisibleForApplier"":true,""AllowStepToStatuses"":""10;16;17;18;19;20"",""EtapId"":null,""OldId"":23,""Etap"":null,""AppliesStatusIds"":null,""ApplyStatusHistoriesPrevStatusIds"":null,""ApplyStatusHistoriesStatusIds"":null},{""Id"":16,""StatusEnumValue"":""PREPARE_TO_GIVE"",""Name"":""подготовка к выдаче"",""NameForButton"":"""",""NameForApplier"":""подготовка к выдаче или отправке"",""NameForApplierEng"":""preparation for issue or shipment"",""DescriptionForApplier"":"""",""DescriptionForApplierEng"":"""",""VisibleForApplier"":false,""AllowStepToStatuses"":""15;17;18;20;21"",""EtapId"":null,""OldId"":24,""Etap"":null,""AppliesStatusIds"":null,""ApplyStatusHistoriesPrevStatusIds"":null,""ApplyStatusHistoriesStatusIds"":null},{""Id"":17,""StatusEnumValue"":""WAIT_PAYMENT"",""Name"":""ожидание оплаты"",""NameForButton"":"""",""NameForApplier"":""Ожидание оплаты"",""NameForApplierEng"":""Waiting for payment"",""DescriptionForApplier"":""<p><strong><span style=\""font-size: medium; font-family: 'times new roman', times; color: #ff0000;\"">Решение по Вашему заявлению принято, но у нас нет сведений об оплате государственной пошлины. Для получения свидетельства необходимо оплатить государственную пошлину и обратиться к нам.</span></strong></p>"",""DescriptionForApplierEng"":""<p><strong><span style=\""font-size: medium; font-family: 'times new roman', times; color: #ff0000;\"">The decision on your application was made, but we have no information about the payment of the state duty. To obtain a certificate, you must pay the state fee and contact us.</span></strong></p>"",""VisibleForApplier"":true,""AllowStepToStatuses"":""10;12;15;18;19;20"",""EtapId"":null,""OldId"":32,""Etap"":null,""AppliesStatusIds"":null,""ApplyStatusHistoriesPrevStatusIds"":null,""ApplyStatusHistoriesStatusIds"":null},{""Id"":18,""StatusEnumValue"":""WAIT_DOCUMENTS"",""Name"":""ожидание документов"",""NameForButton"":null,""NameForApplier"":""Ожидание пакета документов"",""NameForApplierEng"":""Waiting for a package of documents"",""DescriptionForApplier"":"""",""DescriptionForApplierEng"":"""",""VisibleForApplier"":true,""AllowStepToStatuses"":""15;17;19;20;21"",""EtapId"":null,""OldId"":33,""Etap"":null,""AppliesStatusIds"":null,""ApplyStatusHistoriesPrevStatusIds"":null,""ApplyStatusHistoriesStatusIds"":null},{""Id"":19,""StatusEnumValue"":""GIVEN"",""Name"":""выдан"",""NameForButton"":"""",""NameForApplier"":""Выдан"",""NameForApplierEng"":""Issued"",""DescriptionForApplier"":""<p>Выдан</p>"",""DescriptionForApplierEng"":""<p>Issued</p>"",""VisibleForApplier"":true,""AllowStepToStatuses"":""10;15"",""EtapId"":null,""OldId"":41,""Etap"":null,""AppliesStatusIds"":null,""ApplyStatusHistoriesPrevStatusIds"":null,""ApplyStatusHistoriesStatusIds"":null},{""Id"":20,""StatusEnumValue"":""DELETED"",""Name"":""дело прекращено"",""NameForButton"":"""",""NameForApplier"":""Дело прекращено"",""NameForApplierEng"":null,""DescriptionForApplier"":"""",""DescriptionForApplierEng"":null,""VisibleForApplier"":false,""AllowStepToStatuses"":"""",""EtapId"":null,""OldId"":99,""Etap"":null,""AppliesStatusIds"":null,""ApplyStatusHistoriesPrevStatusIds"":null,""ApplyStatusHistoriesStatusIds"":null},{""Id"":21,""StatusEnumValue"":null,""Name"":""повторный"",""NameForButton"":null,""NameForApplier"":""дубликат заявления"",""NameForApplierEng"":""duplicate application"",""DescriptionForApplier"":"""",""DescriptionForApplierEng"":"""",""VisibleForApplier"":true,""AllowStepToStatuses"":""2;3;4"",""EtapId"":null,""OldId"":101,""Etap"":null,""AppliesStatusIds"":null,""ApplyStatusHistoriesPrevStatusIds"":null,""ApplyStatusHistoriesStatusIds"":null}]";
            var objects = JsonConvert.DeserializeObject<ApplyStatus[]>(json);
            _dbContext.ApplyStatuses.AddRange(objects);
            _dbContext.SaveChanges();
        }

        private void FillApplies()
        {
            var apply = new Apply { StatusId = 1L };
            _dbContext.Add(apply);
            _dbContext.SaveChanges();
        }

    }
}
