using Ron.Ido.BM.Extensions;
using Ron.Ido.BM.Services;
using Ron.Ido.EM;
using Ron.Ido.EM.Entities;
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
        public ApplyFormEnum AdmissionForm(Apply apply)
        {
            if ( apply.DeliveryForm.Name.In("self", "courier") )
                return ApplyFormEnum.Personal;

            if ( apply.DeliveryForm.Name == "federal mail" )
                return ApplyFormEnum.Mail;

            return ApplyFormEnum.Online;
        }

        #warning Проверка скорее всего неверная
        public bool ContainsDouble(Apply apply)
        {
            return _dbContext.Applies.Any(z=>z.CreatorEmail == apply.CreatorEmail && z.Id != apply.Id);
        }

        public ContainErrorsEnum ContainsErrors(Apply apply)
        {
            return ContainErrorsEnum.No;
            //throw new NotImplementedException();

        }

        public bool DigitalCertificate(Apply apply)
        {
            throw new NotImplementedException();
        }

        public bool ErrorsInExpertise(Apply apply)
        {
            throw new NotImplementedException();
        }

        public bool ErrorsInExpertOpinion(Apply apply)
        {
            throw new NotImplementedException();
        }

        public bool FullPackageSent(Apply apply)
        {
            throw new NotImplementedException();
        }

        public bool IsFullSet(Apply apply)
        {
            throw new NotImplementedException();
        }

        public bool IsPaymentReceived(Apply apply)
        {
            throw new NotImplementedException();
        }

        public bool IsProcedure(Apply apply)
        {
            return !apply.ForInfoLetter;
        }

        public bool IsPublicService(Apply apply)
        {
            return !string.IsNullOrEmpty(apply.EpguCode);
        }

        public bool IsResultCertificate(Apply apply)
        {
            throw new NotImplementedException();
        }

        public bool IsReturn(Apply apply)
        {
            return apply.ReturnOriginalsForm != null;
        }

        public bool RequestRequired(Apply apply)
        {
            throw new NotImplementedException();
        }

        public bool ResponseReceived(Apply apply)
        {
            throw new NotImplementedException();
        }

        public ReceiveMethodEnum Transport(Apply apply)
        {
            throw new NotImplementedException();
        }
    }
}
