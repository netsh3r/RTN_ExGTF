namespace RTN_ExGTF.configs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class ConfigValue
    {
        /// <summary>
        /// Путь, куда нужно положить файл
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Шаблон
        /// </summary>
        public string Template { get; set; }

        /// <summary>
        /// Нужно ли создавать файл
        /// </summary>
        public bool IsNeedCreate { get; set; } = true;
    }
}
