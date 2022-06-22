namespace RTN_ExGTF
{
    using ExGTF.Reader;
    using Newtonsoft.Json;
    using RTN_ExGTF.configs;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class InitTemplate
    {
        private readonly (ConfigValue confValue, string fileNameFormat)[] _configValues;

        public InitTemplate()
        {
            using var sr = new StreamReader("../../../../configs/config.json");
            var config = JsonConvert.DeserializeObject<Config>(sr.ReadToEnd());
            _configValues = new[]
            {
                (config.Provider, "Form{0}SvodyReport"),
                (config.Map, "Form{0}ShowCaseMap"),
                (config.Entity, "Form{0}ShowCase"),
                (config.ModelService, "Module.Service"),
                (config.SvodyShowCaseTableHelper, "SvodyShowCaseTableHelper"),
            };
        }

        public void Create()
        {
            var taskValues = GetTaskValues();
            foreach (var taskValue in taskValues)
            {
                var dict = DoPrepareValues(taskValue.Values);
                foreach (var confValue in _configValues)
                {
                    DoGenerateTemplate(confValue.confValue, string.Format(confValue.fileNameFormat, taskValue.Name), dict);
                }
            }
        }

        private Dictionary<string, object> DoPrepareValues(TaskValue[] taskValues)
        {
            var dictValue = new Dictionary<string, object>();
            foreach (var taskValue in taskValues)
            {
                if (taskValue.IsArray)
                {
                    var arrayValue = JsonConvert.DeserializeObject<string[]>(taskValue.Value.ToString());
                    dictValue.Add(taskValue.Name, arrayValue);
                }
                else
                {
                    dictValue.Add(taskValue.Name, taskValue.Value);
                }
            }

            return dictValue;
        }

        private void DoGenerateTemplate(ConfigValue confValue, string fileName, Dictionary<string, object> dictValues)
        {
            var reader = new ExGTFReader(confValue.Template, dictValues);
            reader.Create(confValue.Path, confValue.IsNeedCreate, fileName);
        }

        private TaskValues[] GetTaskValues()
        {
            using var sr = new StreamReader("../../../../configs/task_values.json");
            var taskValues = JsonConvert.DeserializeObject<TaskValues[]>(sr.ReadToEnd());
            return taskValues;
        }
    }
}
