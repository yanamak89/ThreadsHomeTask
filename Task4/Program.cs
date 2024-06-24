class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Starting resursive threads ... ");
        StartRecursiveThread(0);
        Console.ReadLine(); // Щоб програма не завершувалась одразу
    }

    static void StartRecursiveThread(int level)
    {
        Console.WriteLine($"Thread level {level} started.");
        if (level < 10)
        {
            Thread newThread = new Thread(() =>
                StartRecursiveThread(level + 1));
            newThread.Start();
        }
        Console.Write($"Thread level {level} finished.");
    }
}