namespace MZBlog.Core.Documents
{
    public class Tag : Base
    {
        public string Slug { get; set; }

        public string Name { get; set; }

        public int PostCount { get; set; }
    }
}