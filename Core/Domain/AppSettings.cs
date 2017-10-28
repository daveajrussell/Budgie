using Core.Domain.Contracts.Settings;

namespace Core.Domain
{
    public class AppSettings : IAppSettings
    {
        public string ConnectionString { get; set; }
    }
}
