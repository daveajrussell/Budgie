using Domain.Contracts.Settings;

namespace Domain
{
    public class AppSettings : IAppSettings
    {
        public string ConnectionString { get; set; }
    }
}
