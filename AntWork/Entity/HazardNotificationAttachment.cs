using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    [Table("NOTIFICATIONATTACHMENT")]
    public class HazardNotificationAttachment : GeneralItem
    {
        [Column("RELATED_ID")]
        public string Related { get; set; }
        [Column("SOURCE_ID")]
        public string Source { get; set; }
    }
}
