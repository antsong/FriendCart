using iBoxDB.LocalServer;
using MZBlog.Core.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MZBlog.Core.ViewProjections.Admin
{
    public class AllTablesViewModel
    {
        public IEnumerable<Author> Authors { get; set; }

        public IEnumerable<BlogComment> Comments { get; set; }

        public IEnumerable<BlogPost> Posts { get; set; }

        public IEnumerable<SpamHash> Spams { get; set; }

        public IEnumerable<Tag> Tags { get; set; }

        public IEnumerable<Log> Logs { get; set; }
    }

    public class AllTablesBindingModel
    {
        public AllTablesBindingModel()
        {
            Page = 1;
            Take = 25;
        }

        public int Page { get; set; }

        public int Take { get; set; }
    }

    public class AllTablesViewProjection : IViewProjection<AllTablesBindingModel, AllTablesViewModel>
    {
        private readonly DB.AutoBox _db;

        public AllTablesViewProjection(DB.AutoBox db)
        {
            _db = db;
        }

        public AllTablesViewModel Project(AllTablesBindingModel input)
        {
            var skip = (input.Page - 1) * input.Take;

            var posts = (from p in _db.Select<BlogPost>("from " + DBTableNames.BlogPosts)
                         orderby p.DateUTC descending
                         select p)
                        .Skip(skip)
                        .Take(input.Take + 1)
                        .ToList()
                        .AsReadOnly();

            var authors = (from p in _db.Select<Author>("from " + DBTableNames.Authors)
                           select p)
                        .Skip(skip)
                        .Take(input.Take + 1)
                        .ToList()
                        .AsReadOnly();

            var comments = (from p in _db.Select<BlogComment>("from " + DBTableNames.BlogComments)
                            select p)
                        .Skip(skip)
                        .Take(input.Take + 1)
                        .ToList()
                        .AsReadOnly();

            var spams = (from p in _db.Select<SpamHash>("from " + DBTableNames.SpamHashes)
                         select p)
                        .Skip(skip)
                        .Take(input.Take + 1)
                        .ToList()
                        .AsReadOnly();

            var tags = (from p in _db.Select<Tag>("from " + DBTableNames.Tags)
                        select p)
                        .Skip(skip)
                        .Take(input.Take + 1)
                        .ToList()
                        .AsReadOnly();
            var logs = (from p in _db.Select<Log>("from " + DBTableNames.Logs)
                        select p)
                        .OrderByDescending(x => x.CreatedOn)
                        .ToList()
                        .AsReadOnly();

            return new AllTablesViewModel
            {
                Authors = authors.Take(input.Take),
                Posts = posts.Take(input.Take),
                Comments = comments.Take(input.Take),
                Spams = spams.Take(input.Take),
                Tags = tags.Take(input.Take),
                Logs = logs
            };

        }
    }
}
