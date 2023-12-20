namespace Savanna.Console
{
    public class ConsoleWrapper : IConsoleWrapper
    {
        public void WriteLine(string message)
        {
            System.Console.WriteLine(message);
        }

        public void Write(string message)
        {
            System.Console.Write(message);
        }

        public void Clear()
        {
            System.Console.Clear();
        }

        public string ReadLine()
        {
            return System.Console.ReadLine();
        }

        public ConsoleKeyInfo ReadKey(bool intercept)
        {
            return System.Console.ReadKey(intercept);
        }

        public void SetCursorPosition(int left, int top)
        {
            System.Console.SetCursorPosition(left, top);
        }
    }
}
