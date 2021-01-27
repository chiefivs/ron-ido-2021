using System;
using System.Collections.Generic;

#nullable disable

namespace Ron.Ido.Importer
{
    public partial class Request
    {
        public Request()
        {
            UploadedFileRequests = new HashSet<UploadedFileRequest>();
        }

        public int Id { get; set; }
        public string OutNum { get; set; }
        public DateTime? OutDate { get; set; }
        public bool IsToSchool { get; set; }
        public string ToName { get; set; }
        public string ToPostIndex { get; set; }
        public string ToAddress { get; set; }
        public string ToEmail { get; set; }
        public string InNum { get; set; }
        public DateTime? InDate { get; set; }
        public DateTime? SentTime { get; set; }
        public DateTime? ReceivedTime { get; set; }
        public string SentByPostService { get; set; }
        public string SentTicket { get; set; }
        public int StatusId { get; set; }
        public string ApplyBarCode { get; set; }
        public string DocxFileName { get; set; }
        public byte[] DocxData { get; set; }
        public int? DocxLength { get; set; }
        public DateTime? DocxCreatedOn { get; set; }
        public int? DocxCreatedById { get; set; }
        public DateTime? DocxModifiedOn { get; set; }
        public int? DocxModifiedById { get; set; }
        public string RespFileName { get; set; }
        public byte[] RespData { get; set; }
        public int? RespLength { get; set; }
        public DateTime? RespCreatedOn { get; set; }
        public int? RespCreatedById { get; set; }
        public int? DocxTemplateId { get; set; }

        public virtual Apply ApplyBarCodeNavigation { get; set; }
        public virtual DocxTemplate DocxTemplate { get; set; }
        public virtual RequestStatus Status { get; set; }
        public virtual ICollection<UploadedFileRequest> UploadedFileRequests { get; set; }
    }
}
