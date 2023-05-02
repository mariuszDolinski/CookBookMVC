namespace CookBook.Application.ApplicationUser
{
    public class CurrentUser
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public IEnumerable<string> Roles { get; set; }

        public CurrentUser(string id, string name, IEnumerable<string> roles) 
        { 
            Id = id;
            UserName = name;
            Roles = roles;
        }
    }
}
