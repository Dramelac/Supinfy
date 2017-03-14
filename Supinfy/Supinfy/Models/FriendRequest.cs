using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Supinfy.Models
{
    public class FriendRequest
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [ForeignKey("Initiator")]
        public Guid InitiatorId { get; set; }
        
        public virtual User Initiator { get; set; }

        [ForeignKey("Target")]
        public Guid TargetId { get; set; }

        public virtual User Target { get; set; }

        //public FriendStatus Status { get; set; }
        
    }
}