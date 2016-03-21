namespace App.Client.Infastructure.ViewModels
{
    public class CurrentUserVm
    {
        public bool IsAuthenticated { get; set; }
        public string Username { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsDashboard { get; set; }
        public bool IsInvestigate { get; set; }
    }
}
