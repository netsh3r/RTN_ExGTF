namespace RTN_ExGTF.configs
{
    /// <summary>
    /// Модель конфигурации
    /// </summary>
    internal class Config
    {
        /// <summary>
        /// Провайдер
        /// </summary>
        public ConfigValue Provider { get; set; }

        /// <summary>
        /// Маппинг сущности
        /// </summary>
        public ConfigValue Map { get; set; }

        /// <summary>
        /// Сущность
        /// </summary>
        public ConfigValue Entity { get; set; }

        /// <summary>
        /// Регистрация сервисов
        /// </summary>
        public ConfigValue ModelService { get; set; }

        /// <summary>
        /// Класс сопоставления приходящего formCode и названий SvodyReport'ов.
        /// </summary>
        public ConfigValue SvodyShowCaseTableHelper { get; set; }
    }
}
