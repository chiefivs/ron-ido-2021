using Ron.Ido.EM.Enums;

namespace Ron.Ido.BM.Constants
{
    public static class ApplyAllowedStatuses
    {
        public static long[] Acceptance = new long[] {
            (long)ApplyStatusEnum.NO_VALIDATED,
            (long)ApplyStatusEnum.HAS_ERRORS,
            (long)ApplyStatusEnum.UNDERMANNED,
            (long)ApplyStatusEnum.FIXED_BY_USER,
            (long)ApplyStatusEnum.SECOND_REQUEST};

        public static long[] Search = new long[] {
            (long)ApplyStatusEnum.APPROVED,
            (long)ApplyStatusEnum.DECISION_ACT,
            (long)ApplyStatusEnum.DECISION_PREPARATION,
            (long)ApplyStatusEnum.DELETED,
            (long)ApplyStatusEnum.GIVEN,
            (long)ApplyStatusEnum.INFO_LETTER,
            (long)ApplyStatusEnum.NO_VALIDATED,
            (long)ApplyStatusEnum.ON_EXPERTIZE,
            (long)ApplyStatusEnum.ON_RESEARCH,
            (long)ApplyStatusEnum.ON_RESEARCH_END,
            (long)ApplyStatusEnum.PREPARE_TO_GIVE,
            (long)ApplyStatusEnum.READY_TO_GIVE,
            (long)ApplyStatusEnum.SUSPENDED,
            (long)ApplyStatusEnum.UNDERMANNED,
            (long)ApplyStatusEnum.WAIT_DOCUMENTS,
            (long)ApplyStatusEnum.WAIT_PAYMENT };

    }
}
