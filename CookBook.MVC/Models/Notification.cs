namespace CookBook.MVC.Models
{
    public class Notification
    {
        public Notification(string type, string message)
        {
            Type = type;
            Message = message;
            if (Type == "success" || Type == "info")
            {
                Title = "";
            }
            else
            {
                Title = "Uwaga!";
            }
        }

        public string Type { get; set; }
        public string Message { get; set; }
        public string Title { get; }
    }
}
