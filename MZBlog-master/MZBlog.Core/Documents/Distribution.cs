using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MZBlog.Core.Documents
{
    public class Distribution: Base
    {
        public Distribution()
        {
            Id = ObjectId.NewObjectId();
        }

        public bool Type;
        public string TypeDescript
        {
            get
            {
                return Type ? "正式保单" : "投保单";
            }
        }
        public string PersonName;
        public string PlateNum;
        public string Phone;
        public DateTime? StartTime;
        public decimal? StrongInsurance;
        public decimal? CommercialInsurance;
        public decimal? Tax;
        public decimal? Total
        {
            get
            {
                return StrongInsurance.GetValueOrDefault() + CommercialInsurance.GetValueOrDefault() + Tax.GetValueOrDefault();
            }
        }
        public string TaxNum;
        public string Address;
        public string Description;
    }
}
