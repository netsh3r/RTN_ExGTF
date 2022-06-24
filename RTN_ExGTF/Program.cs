namespace RTN_ExGTF
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var temp = new InitTemplate();
            Console.WriteLine("Введите последнюю миграцию");
            var lastMigration = Console.ReadLine();
            Console.WriteLine("Введите список миграций через запятую");
            var migrations = Console.ReadLine();
            var mAr = migrations.Split(',').Select(x => x.Trim()).ToList();
            var result = new Queue<string>(mAr);
            temp.Create(lastMigration, result);
        }
    }
}