using iBoxDB.LocalServer;
using MZBlog.Core.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MZBlog.Core.ViewProjections.Dist
{
    public class AllDistributionsViewModel
    {
        public IEnumerable<Distribution> Distributions { get; set; }

        public int Page { get; set; }

        public bool HasNextPage { get; set; }

        public bool HasPrevPage
        {
            get
            {
                return Page > 1;
            }
        }
    }

    public class AllDistributionsBindingModel
    {
        public AllDistributionsBindingModel()
        {
            Page = 1;
            Take = 10;
        }

        public int Page { get; set; }

        public int Take { get; set; }
    }

    public class AllDistributionViewProjection : IViewProjection<AllDistributionsBindingModel, AllDistributionsViewModel>
    {
        private readonly DB.AutoBox _db;

        public AllDistributionViewProjection(DB.AutoBox db)
        {
            _db = db;
        }

        public AllDistributionsViewModel Project(AllDistributionsBindingModel input)
        {
            var skip = (input.Page - 1) * input.Take;

            var distributions = (from p in _db.Select<Distribution>("from " + DBTableNames.Distributions)
                                 orderby p.CreatedOn descending
                                 select p)
                        .Skip(skip)
                        .Take(input.Take + 1)
                        .ToList()
                        .AsReadOnly();

            var pagedDistributions = distributions.Take(input.Take);
            var hasNextPage = distributions.Count > input.Take;

            return new AllDistributionsViewModel
            {
                Distributions = pagedDistributions,
                Page = input.Page,
                HasNextPage = hasNextPage
            };
        }
    }

    public class DistributionSearchBindingModel
    {
        public string PlateNum { get; set; }
        public string TaxNum { get; set; }
        public string PersonName { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
    }

    public class SearchDistributionViewProjection : IViewProjection<DistributionSearchBindingModel, AllDistributionsViewModel>
    {
        private readonly DB.AutoBox _db;

        public SearchDistributionViewProjection(DB.AutoBox db)
        {
            _db = db;
        }

        public AllDistributionsViewModel Project(DistributionSearchBindingModel input)
        {
            var distributions = (from p in _db.Select<Distribution>("from " + DBTableNames.Distributions)
                                 select p);
            #region Where

            if (!string.IsNullOrWhiteSpace(input.TaxNum))
            {
                distributions = distributions.Where(x => !string.IsNullOrWhiteSpace(x.TaxNum) && x.TaxNum.Contains(input.TaxNum));
            }
            if (!string.IsNullOrWhiteSpace(input.PlateNum))
            {
                distributions = distributions.Where(x => !string.IsNullOrWhiteSpace(x.PlateNum) && x.PlateNum.Contains(input.PlateNum));
            }
            if (!string.IsNullOrWhiteSpace(input.PersonName))
            {
                distributions = distributions.Where(x => !string.IsNullOrWhiteSpace(x.PersonName) && x.PersonName.Contains(input.PersonName));
            }
            if (input.StartTime.HasValue)
            {
                distributions = distributions.Where(x => x.StartTime.HasValue && x.StartTime > input.StartTime.Value);
            }
            if (input.EndTime.HasValue)
            {
                distributions = distributions.Where(x => x.StartTime.HasValue && x.StartTime <= input.EndTime.Value);
            }

            #endregion

            var pagedDistributions = distributions.OrderByDescending(x => x.CreatedOn).ToList().AsReadOnly();

            return new AllDistributionsViewModel
            {
                Distributions = pagedDistributions,
                Page = 0,
                HasNextPage = false
            };
        }
    }
}
