﻿using Ron.Ido.BM.Extensions;
using Ron.Ido.BM.Interfaces;
using Ron.Ido.BM.Services;
using Ron.Ido.Common.Extensions;
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
        public ApplyFormEnum AdmissionForm(Apply apply, string pars)
        {
            if ( apply.DeliveryForm.Name.In("self", "courier") )
                return ApplyFormEnum.Personal;

            if ( apply.DeliveryForm.Name == "federal mail" )
                return ApplyFormEnum.Mail;

            return ApplyFormEnum.Online;
        }

        #warning Проверка скорее всего неверная
        public bool ContainsDouble(Apply apply, string pars)
        {
            return _dbContext.Applies.Any(z=>z.CreatorEmail == apply.CreatorEmail && z.Id != apply.Id);
        }

        public ContainErrorsEnum ContainsErrors(Apply apply, string pars)
        {
            return ContainErrorsEnum.No;
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

        public ReceiveMethodEnum Transport(Apply apply, string pars)
        {
            throw new NotImplementedException();
        }
    }
}
