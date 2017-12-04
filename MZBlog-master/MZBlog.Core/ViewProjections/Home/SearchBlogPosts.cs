using iBoxDB.LocalServer;
using MZBlog.Core.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MZBlog.Core.ViewProjections.Home
{
    public class SearchBlogPostsViewModel
    {
        public IEnumerable<BlogPost> Posts { get; set; }

        public string SearchContent { get; set; }
    }

    public class SearchBlogPostsBindingModel
    {
        public string SearchContent { get; set; }
    }

    public class SearchBlogPostsViewProjection : IViewProjection<SearchBlogPostsBindingModel, SearchBlogPostsViewModel>
    {
        private readonly DB.AutoBox _db;

        public SearchBlogPostsViewProjection(DB.AutoBox db)
        {
            _db = db;
        }

        public SearchBlogPostsViewModel Project(SearchBlogPostsBindingModel input)
        {
            var posts = (from p in _db.Select<BlogPost>("from " + DBTableNames.BlogPosts)
                         where p.IsPublished && p.Description.Contains(input.SearchContent)
                         orderby p.PubDate descending
                         select p)
                     .ToList();
            if (posts.Count == 0)
                return null;
            return new SearchBlogPostsViewModel
            {
                Posts = posts,
                SearchContent = input.SearchContent
            };
        }
    }
}
