namespace MZBlog.Core.Documents
{
    public class Author : Base
    {
        public Author()
        {
            Id = ObjectId.NewObjectId();
        }

        public string HashedPassword { get; set; }

        public string Email { get; set; }

        public string DisplayName { get; set; }

        public string[] Roles { get; set; }
    }
}