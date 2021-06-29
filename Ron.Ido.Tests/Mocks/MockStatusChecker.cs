using Ron.Ido.BM.Extensions;
using Ron.Ido.BM.Interfaces;
using Ron.Ido.BM.Services;
using Ron.Ido.Common.Extensions;
using Ron.Ido.EM;
using Ron.Ido.EM.Entities;
using Ron.Ido.EM.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ron.Ido.Tests.Mocks
{
    public class MockStatusChecker : IStatusChecker
    {
        public MockStatusChecker(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        AppDbContext _dbContext;

        #warning Проверка скорее всего неверная
        public bool ContainsDouble(Apply apply, string pars)
        {
            return _dbContext.Applies.Any(z=>z.CreatorEmail == apply.CreatorEmail && z.Id != apply.Id);
        }

        public ContainErrorsEnum ContainsErrors(Apply apply, string pars)
        {

            ErrorTest rt = pars.FromJson<ErrorTest>();
            if ( !rt.HasErrors)
                return ContainErrorsEnum.No;
            if ( rt.Form == ApplyEntryFormEnum.MAIL )
                return ContainErrorsEnum.Mail;

            return ContainErrorsEnum.Online;

            //throw new NotImplementedException();

        }

        public bool DigitalCertificate(Apply apply, string pars)
        {
            throw new NotImplementedException();
        }

        public bool ErrorsInExpertise(Apply apply, string pars)
        {
            throw new NotImplementedException();
        }

        public bool ErrorsInExpertOpinion(Apply apply, string pars)
        {
            throw new NotImplementedException();
        }

        public bool FullPackageSent(Apply apply, string pars)
        {
            throw new NotImplementedException();
        }

        public bool IsFullSet(Apply apply, string pars)
        {
            throw new NotImplementedException();
        }

        public bool IsPaymentReceived(Apply apply, string pars)
        {
            throw new NotImplementedException();
        }

        public bool IsProcedure(Apply apply, string pars)
        {
            return !apply.ForInfoLetter;
        }

        public bool IsPublicService(Apply apply, string pars)
        {
            return !string.IsNullOrEmpty(apply.EpguCode);
        }

        public bool IsResultCertificate(Apply apply, string pars)
        {
            throw new NotImplementedException();
        }

        public bool IsReturn(Apply apply, string pars)
        {
            return apply.ReturnOriginalsForm != null;
        }

        public bool RequestRequired(Apply apply, string pars)
        {
            throw new NotImplementedException();
        }

        public bool ResponseReceived(Apply apply, string pars)
        {
            throw new NotImplementedException();
        }

        public ApplyDeliveryFormEnum Transport(Apply apply, string pars)
        {
            return (ApplyDeliveryFormEnum)apply.DeliveryFormId.GetValueOrDefault((long)ApplyDeliveryFormEnum.SELF);
        }

        public ApplyEntryFormEnum AdmissionForm(Apply apply, string pars)
        {
            return (ApplyEntryFormEnum)apply.EntryFormId.GetValueOrDefault((long)ApplyEntryFormEnum.SELF);
        }
    }

    class ErrorTest
    {
        public ApplyEntryFormEnum Form { get; set; }
        public bool HasErrors { get; set; }
    }

}
