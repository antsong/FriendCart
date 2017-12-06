using iBoxDB.LocalServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MZBlog.Core.Documents;
using MZBlog.Core.Extensions;

namespace MZBlog.Core.Commands.Dist
{
    public class DeleteDistributionCommand
    {
        public string Id { get; set; }
    }

    public class DeleteDistributionCommandInvoker
    {
        private readonly DB.AutoBox _db;
        public DeleteDistributionCommandInvoker(DB.AutoBox db)
        {
            _db = db;
        }

        public CommandResult Execute(DeleteDistributionCommand command)
        {
            _db.Delete(DBTableNames.Authors, command.Id);
            return CommandResult.SuccessResult;
        }
    }

    public class NewDistributionCommand
    {
        public bool Type { get; set; }
        public string PersonName { get; set; }
        public string PlateNum { get; set; }
        public string Phone { get; set; }
        public DateTime? StartTime { get; set; }
        public decimal? StrongInsurance { get; set; }
        public decimal? CommercialInsurance { get; set; }
        public decimal? Tax { get; set; }
        public string TaxNum { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
    }

    public class NewDistributionCommandInvoker : ICommandInvoker<NewDistributionCommand, CommandResult>
    {
        private readonly DB.AutoBox _db;

        public NewDistributionCommandInvoker(DB.AutoBox db)
        {
            _db = db;
        }

        public CommandResult Execute(NewDistributionCommand command)
        {
            var distribution = new Distribution
            {
                Id = ObjectId.NewObjectId(),
                Type = command.Type,
                PersonName = command.PersonName,
                PlateNum = command.PlateNum,
                Phone = command.Phone,
                StartTime = command.StartTime,
                StrongInsurance = command.StrongInsurance,
                CommercialInsurance = command.CommercialInsurance,
                Tax = command.Tax,
                TaxNum = command.TaxNum,
                Address = command.Address,
                Description = command.Description
            };

            var result = _db.Insert(DBTableNames.Distributions, distribution);

            return CommandResult.SuccessResult;
        }
    }

    public class EditDistributionCommand
    {
        public string Id { get; set; }
        public bool Type { get; set; }
        public string PersonName { get; set; }
        public string PlateNum { get; set; }
        public string Phone { get; set; }
        public DateTime? StartTime { get; set; }
        public decimal? StrongInsurance { get; set; }
        public decimal? CommercialInsurance { get; set; }
        public decimal? Tax { get; set; }
        public string TaxNum { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
    }

    public class EditDistributionCommandInvoker : ICommandInvoker<EditDistributionCommand, CommandResult>
    {
        private readonly DB.AutoBox _db;

        public EditDistributionCommandInvoker(DB.AutoBox db)
        {
            _db = db;
        }

        public CommandResult Execute(EditDistributionCommand command)
        {
            var distribution = _db.SelectKey<Distribution>(DBTableNames.Distributions, command.Id);

            if (distribution == null)
                throw new ApplicationException("Distribution with id: {0} was not found".FormatWith(command.Id));

            distribution.Type = command.Type;
            distribution.PersonName = command.PersonName;
            distribution.PlateNum = command.PlateNum;
            distribution.Phone = command.Phone;
            distribution.StartTime = command.StartTime;
            distribution.StrongInsurance = command.StrongInsurance;
            distribution.CommercialInsurance = command.CommercialInsurance;
            distribution.Tax = command.Tax;
            distribution.TaxNum = command.TaxNum;
            distribution.Address = command.Address;
            distribution.Description = command.Description;
            _db.Update(DBTableNames.Distributions, distribution);
            return CommandResult.SuccessResult;
        }
    }
}
