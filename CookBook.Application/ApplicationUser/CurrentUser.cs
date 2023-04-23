namespace CookBook.Application.ApplicationUser
{
    public class CurrentUser
    {
        public string Id { get; set; }
        public string UserName { get; set; }

        public CurrentUser(string id, string name) 
        { 
            Id = id;
            UserName = name;
        }
    }
}
