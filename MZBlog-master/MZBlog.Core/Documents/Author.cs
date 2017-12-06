namespace MZBlog.Core.Documents
{
    public class Author : Base
    {
        public Author()
        {
            Id = ObjectId.NewObjectId();
        }

        public string HashedPassword;

        public string Email;

        public string DisplayName;

        public string Phone;

        public bool Enable;

        public string[] Roles;
    }
}