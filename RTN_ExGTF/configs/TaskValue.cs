namespace RTN_ExGTF.configs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Значения по задаче для генерации шаблона
    /// </summary>
    internal class TaskValue
    {
        /// <summary>
        /// Название переменной
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Признак массива
        /// </summary>
        public bool IsArray { get; set; } = false;

        /// <summary>
        /// Признак массива объектов
        /// </summary>
        public bool IsArrayObjects { get; set; } = false;

        /// <summary>
        /// Значение
        /// </summary>
        public object Value { get; set; }
    }
}
