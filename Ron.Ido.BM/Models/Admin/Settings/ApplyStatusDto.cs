using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Ron.Ido.BM.Models.Admin.Settings
{
	public class ApplyStatusDto : IValidatableObject
	{
		public long Id { get; set; }

		[Required( ErrorMessage = "Название статуса не может быть пустым", AllowEmptyStrings = false )]
		[StringLength( 50 )]
		public string Name { get; set; }

		public IEnumerable<long> AllowStepToStatuses { get; set; } = Array.Empty<long>();

		[StringLength( 50 )]
		public string NameForButton { get; set; }

		[StringLength( 50 )]
		[Required( ErrorMessage = "Поле обязательно для заполнения", AllowEmptyStrings = false )]
		public string NameForApplier { get; set; }

		[StringLength( 50 )]
		[Required( ErrorMessage = "Поле обязательно для заполнения", AllowEmptyStrings = false )]
		public string NameForApplierEng { get; set; }

		[StringLength( 1000 )]
		[Required( ErrorMessage = "Поле обязательно для заполнения", AllowEmptyStrings = false )]
		public string DescriptionForApplier { get; set; }

		[StringLength( 1000 )]
		[Required( ErrorMessage = "Поле обязательно для заполнения", AllowEmptyStrings = false )]
		public string DescriptionForApplierEng { get; set; }

		public bool VisibleForApplier { get; set; }


		public IEnumerable<ValidationResult> Validate( ValidationContext validationContext )
		{
			var errors = new List<ValidationResult>();

			if ( !AllowStepToStatuses.Any() )
			{
				errors.Add( new ValidationResult( "Должен быть выбран хотя бы один статус", new[] { nameof( AllowStepToStatuses ) } ) );
			}
			else
				if ( Id < 0 && AllowStepToStatuses.Contains( Id ) )
				errors.Add( new ValidationResult( "Циклическая ссылка на статус", new[] { nameof( AllowStepToStatuses ) } ) );
			return errors;
		}
	}
}
