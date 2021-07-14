using Ron.Ido.Common.Extensions;
using Ron.Ido.EM.Entities;
using Ron.Ido.EM.Enums;

namespace Ron.Ido.BM.Models.Applies.Acceptance
{
    public class AcceptancePageItemDto
    {
        public long Id { get; set; }

        public long DossierId { get; set; }

        public string BarCode { get; set; }

        public string CreateDate { get; set; }

        public ApplyEntryFormEnum EntryFormId { get; set; }

        public string CreatorFullName { get; set; }

        public string OwnerFullName { get; set; }

        public string Status { get; set; }

        public static string PrimaryBarCodeFilterField = nameof(Apply.PrimaryBarCode).ToCamel();
        public static string BarCodeFilterField = nameof(Apply.BarCode).ToCamel();
        public static string CreateTimeFilterField = nameof(Apply.CreateTime).ToCamel();
        public static string CreatorSurnameFilterField = nameof(Apply.CreatorSurname).ToCamel();
        public static string CreatorFirstNameFilterField = nameof(Apply.CreatorFirstName).ToCamel();
        public static string CreatorLastNameFilterField = nameof(Apply.CreatorLastName).ToCamel();
        public static string OwnerSurnameFilterField = nameof(Apply.OwnerSurname).ToCamel();
        public static string OwnerFirstNameFilterField = nameof(Apply.OwnerFirstName).ToCamel();
        public static string OwnerLastNameFilterField = nameof(Apply.OwnerLastName).ToCamel();
        public static string StatusesFilterField = "statuses";
        public static string LearnLevelsFilterField = "learnLevels";
        public static string EntryFormsFilterField = "entryForms";
        public static string StagesFilterField = "stages";
    }
}
