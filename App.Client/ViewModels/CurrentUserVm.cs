namespace App.Client.ViewModels
{
    public class CurrentUserVm
    {
        public bool IsAuthenticated { get; set; }
        public string Username { get; set; }
        public bool IsAdmin { get; set; }
    }
}
