namespace Shitter.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    using Enums;

    public class Message
    {
        private const int DEFAULT_MESSAGE_STATUS = 0;

        private MessageStatus messageStatus;
        private DateTime dateSend;

        public Message()
        {
            this.MessageStatus = DEFAULT_MESSAGE_STATUS;
            this.DateSend = DateTime.Now;
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(2000, ErrorMessage = "Message should be between 1 and 2000 charachters.")]
        public string Content { get; set; }

        [Required]
        [DefaultValue(DEFAULT_MESSAGE_STATUS)]
        public MessageStatus MessageStatus
        {
            get { return this.messageStatus; }
            set { this.messageStatus = value; }
        }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DateSend
        {
            get { return this.dateSend; }
            set { this.dateSend = value; } 
        }

        public string SenderId { get; set; }

        public virtual User Sender { get; set; }

        public string ReceiverId { get; set; }

        public virtual User Receiver { get; set; }
    }
}
