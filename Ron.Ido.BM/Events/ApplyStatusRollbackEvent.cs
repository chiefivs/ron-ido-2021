using MediatR;
using Ron.Ido.EM.Entities;

namespace Ron.Ido.BM.Events
{
    public class ApplyStatusRollbackEvent : INotification
    {
        public Dossier Dossier { get; private set; }
        public string Pars { get; private set; }

        public ApplyStatusRollbackEvent(Dossier dossier, string pars)
        {
            Dossier = dossier;
            Pars = pars;
        }
    }
}
