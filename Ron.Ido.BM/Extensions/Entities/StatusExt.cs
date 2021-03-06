﻿using Ron.Ido.EM.Entities;
using Ron.Ido.EM.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ron.Ido.BM.Extensions.Entities
{
	public static class StatusExt
	{
		public static bool CanJumpTo( this ApplyStatus applyStatus, ApplyStatus newStatus )
		{
			return applyStatus.CanJumpTo( newStatus.Id );
		}
		public static bool CanJumpTo( this ApplyStatus applyStatus, long newStatus )
		{
			return applyStatus.AllowStepToStatuses.Split( ';', StringSplitOptions.RemoveEmptyEntries ).Select( str=>Convert.ToInt64(str) ).Contains( newStatus );
		}
		public static ApplyStatusEnum EnumStatus( this ApplyStatus applyStatus )
		{
            if ( Enum.TryParse<ApplyStatusEnum>(applyStatus.StatusEnumValue, out var data) )
                return data;
            return ApplyStatusEnum.NO_VALIDATED;
		}
	}
}
