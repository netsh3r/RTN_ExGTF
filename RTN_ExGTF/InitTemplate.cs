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
    using ExGTF_ParamsHelper = ExGTF.Reader.ParamsHelper;

    internal class InitTemplate
    {
        private readonly (string confName, ConfigValue confValue, string fileNameFormat)[] _configValues;

        public InitTemplate()
        {
            using var sr = new StreamReader("../../../../configs/config.json");
            var config = JsonConvert.DeserializeObject<Config>(sr.ReadToEnd());
            _configValues = new[]
            {
                ("Provider", config.Provider, "Form{0}SvodyReport"),
                ("Migration", config.Migration, "{1}"),
                ("Map", config.Map, "Form{0}ShowCaseMap"),
                ("Entity", config.Entity, "Form{0}ShowCase"),
                ("ModelService", config.ModelService, "Module.Service"),
                ("SvodyShowCaseTableHelper", config.SvodyShowCaseTableHelper, "SvodyShowCaseTableHelper"),
                ("ShowCaseTables", config.SvodyShowCaseTableHelper, "ShowCaseTables")
            };
        }

        public void Create(string lastMigration, Queue<string> migrations)
        {
            var taskValues = GetTaskValues();
            foreach (var taskValue in taskValues)
            {
                var dict = ExGTF_ParamsHelper.GetParams(taskValue.Values);
                var migration = migrations.Dequeue();
                dict.Add("LastMigration", lastMigration);
                dict.Add("Migration", migration);
                foreach (var confValue in _configValues)
                {
                    var fileName = taskValue.Name;
                    if (confValue.confName == "Migration")
                    {
                        fileName = migration;
                    }
                    
                    DoGenerateTemplate(confValue.confValue, string.Format(confValue.fileNameFormat, taskValue.Name, fileName), dict);
                }

                lastMigration = migration;
            }
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
