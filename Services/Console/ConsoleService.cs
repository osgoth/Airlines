namespace Services.Console;

public class ConsoleService : IConsoleService
{
    public void Output()
    {
        System.Console.WriteLine("It Worked");
    }
}