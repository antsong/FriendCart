using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{

    public class GeneralItem
    {
        [Column("ID")]
        public string Id { get; set; }

        [Column("ITEM_TYPE_ID")]
        public string ItemTypeId { get; set; }

        [Column("KEYED_NAME")]
        public string KeyedName { get; set; }

        [Column("CREATED_ON")]
        public DateTime CreatedOn
        {
            get; set;
        }

        [Column("MODIFIED_ON")]
        public DateTime ModifiedOn
        {
            get;
            set;
        }
    }
}
