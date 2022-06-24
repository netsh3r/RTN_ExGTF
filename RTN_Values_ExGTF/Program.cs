namespace RTN_Values_ExGTF
{
    using ExGTF.Reader;
    using System.Text.RegularExpressions;

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите путь до файлов");
            var line = Console.ReadLine();
            var filePath = line;
            Console.WriteLine("Введите путь до шаблона");
            line = Console.ReadLine();
            var tampPath = line;
            var rg = new Regex(@"create table (\w*).(\w*)\((.*)\)");
            Console.WriteLine("Введите запрос на создание таблицы");
            while ((line = Console.ReadLine()) != "exit")
            {
                if (rg.IsMatch(line))
                {
                    var res = rg.Match(line);
                    var tableName = res.Groups[2].Value;
                    var @props = new Dictionary<string, object>()
                    {
                        { "short_name", tableName.Substring(7) },
                        { "table_name", tableName },
                        { "has_row_codes", Check("Нужны RowCodes?")  },
                        { "has_column_codes", Check("Нужны ColumnCodes?")  },
                        { "rowCodes", res.Groups[3].Value.Split(' ').Where(x => x.Contains("row_code")).Select(x => x.Substring(9)).ToArray() },
                        { "columnCodes", res.Groups[3].Value.Split(' ').Where(x => x.Contains("column_code")).Select(x => x.Substring(9)).ToArray() }
                    };
                    var er = new ExGTFReader(tampPath, props);
                    er.Create(filePath, true, tableName);
                    Console.WriteLine("Введите следующий запрос или напишите exit для выхода");
                }
            }
        }

        private static string Check(string msg)
        {
            Console.WriteLine($"{msg} {{Y/N}}");
            var res = Console.ReadLine();
            return (res == "Y").ToString();
        }
    }
}