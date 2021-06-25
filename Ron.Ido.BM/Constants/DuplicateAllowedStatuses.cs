using Ron.Ido.EM.Enums;

namespace Ron.Ido.BM.Constants
{
    public static class DuplicateAllowedStatuses
    {

        public static readonly long[] Search = new long[] {
            (long)DuplicateStatusEnum.DECISION_PREPARATION,
            (long)DuplicateStatusEnum.DECLINED_TO_GIVE,
            (long)DuplicateStatusEnum.DUPLICATE_APPROVED,
            (long)DuplicateStatusEnum.DUPLICATE_CREATED,
            (long)DuplicateStatusEnum.GIVEN,
            (long)DuplicateStatusEnum.READY_TO_GIVE,
            (long)DuplicateStatusEnum.WAITING_PAYMENT
        };
    }
}
