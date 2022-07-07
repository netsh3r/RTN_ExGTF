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
                ("ShowCaseTables", config.ShowCaseTables, "ShowCaseTables")
            };
        }

        public void Create(Queue<string> migrations)
        {
            var taskValues = GetTaskValues();
            var lastMigration = migrations.Dequeue();
            foreach (var taskValue in taskValues)
            {
                Console.WriteLine($"Генерация {taskValue.Name}");
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

                    Console.WriteLine($"Генерация {confValue.confName}");
                    try
                    {
                        DoGenerateTemplate(confValue.confValue, string.Format(confValue.fileNameFormat, taskValue.Name, fileName), dict);
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        return;
                    }
                    Console.WriteLine("----------");
                }
                Console.WriteLine("###########################");
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
            if (taskValues == null)
            {
                throw new Exception("Не удалось получить список данных");
            }

            return taskValues;
        }
    }
}
