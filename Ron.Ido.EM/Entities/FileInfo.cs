using Ron.Ido.Common.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Ron.Ido.EM.Entities
{
	public class FileInfo : IFileInfo
	{
		[Key]
		public Guid Uid { get; set; }

		[StringLength(260)]
		public string Name { get; set; }

		public int Size { get; set; }

		[StringLength(200)]
		public string ContentType { get; set; }

		public DateTime CreateTime { get; set; } = DateTime.Now;
		
		public long? CreatedById { get; set; }
		
		[JsonIgnore]
		[ForeignKey("CreatedById")]
		public virtual User CreatedBy { get; set; }

        #region данные из Н2010
        [JsonIgnore]
		public int OldId { get; set; }

		[JsonIgnore]
		[StringLength(260)]
		public string CreatorEmail { get; set; }

		[JsonIgnore]
		[StringLength(1)]
		public string Source { get; set; }
		#endregion
	}
}
