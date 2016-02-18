namespace MyNotes.Web.MultiTenancy
{
    public class AppTenant
    {
        public string Id { get; set; }

        public string DisplayName { get; set; }

        public string IpAddress { get; set; }

        public bool IsPrivate { get; set; }

        public bool IsDeleted { get; set; }
    }
}
