namespace RTN_ExGTF
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var temp = new InitTemplate();
            Console.WriteLine("Введите список миграций через запятую, где первая - последняя проведенная");
            var mAr = Console.ReadLine()?.Split(',').Select(x => x.Trim()).ToList() ?? throw new Exception("Не передан список миграций");
            var result = new Queue<string>(mAr);
            temp.Create(result);
        }
    }
}