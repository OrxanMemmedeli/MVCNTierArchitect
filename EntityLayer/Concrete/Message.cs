using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;


namespace EntityLayer.Concrete
{
    public class Message
    {
        [Key]
        public int ID { get; set; }
        [StringLength(250)]
        public string SenderEmail { get; set; }
        [StringLength(250)]
        public string ReceiverEmail { get; set; }
        [StringLength(50)]
        public string Subject { get; set; }
        [AllowHtml]
        public string MessageText { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsReaded { get; set; } = false;
        public bool IsResponded { get; set; } = false;
        public bool IsDraft { get; set; } = false;
        public bool IsDeleted { get; set; } = false;
        public DateTime DeletedDate { get; set; } = DateTime.Now;
        public int? ContactID { get; set; } = null;

        public static implicit operator Message(Contact model)
        {
            return new Message
            {
                ReceiverEmail = model.Email,
                Subject = model.Subject,
                MessageText = model.Message,
                CreatedDate = model.CreatedDate,
                IsReaded = model.IsReaded,
                IsDeleted = model.IsDeleted,
                IsResponded = model.IsResponded,
                DeletedDate = model.DeletedDate,
                ContactID = model.ID
            };
        }

        //public static implicit operator Contact(Message model)
        //{
        //    return new Contact
        //    {
        //        Email = model.SenderEmail,
        //        Subject = model.Subject,
        //        Message = model.MessageText,
        //        CreatedDate = model.CreatedDate,
        //        IsReaded = model.IsReaded,
        //        IsDeleted = model.IsDeleted,
        //        IsResponded = model.IsResponded,
        //        DeletedDate = model.DeletedDate,
        //    };
        //}
    }
}
