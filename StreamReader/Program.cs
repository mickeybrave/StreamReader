// See https://aka.ms/new-console-template for more information

using StreamReader;

class Program
{
    const int DefaultPauseMilliseconds = 2000;
    static async Task Main(string[] args)
    {
        Runner runner = new Runner();
        do
        {
            while (!Console.KeyAvailable)
            {
                Console.WriteLine("**********************************************************");
                var result = await runner.ExtractText();
                Console.WriteLine(result);
                Console.WriteLine("**********************************************************");
                Console.WriteLine("Press ESC to stop");
                Thread.Sleep(DefaultPauseMilliseconds);
            }
        } while (Console.ReadKey(true).Key != ConsoleKey.Escape);


    }
}

