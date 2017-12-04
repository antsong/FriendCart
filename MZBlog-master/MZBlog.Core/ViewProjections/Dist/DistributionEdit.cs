using iBoxDB.LocalServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MZBlog.Core.Documents;
using MZBlog.Core.Extensions;

namespace MZBlog.Core.ViewProjections.Dist
{
    
    public class DistributionEditBindingModel
    {
        public string DistId { get; set; }
    }

    public class DistributionEditViewModel
    {
        public Distribution Distribution { get; set; }
    }

    public class DistributionEditViewProjection : IViewProjection<DistributionEditBindingModel, DistributionEditViewModel>
    {
        private readonly DB.AutoBox _db;

        public DistributionEditViewProjection(DB.AutoBox db)
        {
            _db = db;
        }

        public DistributionEditViewModel Project(DistributionEditBindingModel input)
        {
            var dist = _db.SelectKey<Distribution>(DBTableNames.Distributions, input.DistId);

            return new DistributionEditViewModel { Distribution = dist };
        }
    }

}
