using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Entity
{
    [Table("FILE")]
    public class FileDocement : GeneralItem
    {
        [Column("NAME")]
        public string Name { get; set; }

        [Column("FILE_CONTENT")]
        public byte[] FileContent { get; set; }

        [Column("SIZE")]
        public int Size { get; set; }

        //public File GetFile()
        //{
        //    MemoryStream ms = new MemoryStream(FileContent);
        //    File file = File.Open()
        //}
    }
}
