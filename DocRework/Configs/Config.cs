namespace DocRework.Configs
{
    using Exiled.API.Interfaces;
    using SubConfigs;

    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public DoctorConfigs DoctorConfigs { get; set; } = new DoctorConfigs();
        public ZombieConfigs ZombieConfigs { get; set; } = new ZombieConfigs();
        public Translations Translations { get; set; } = new Translations();
    }
}