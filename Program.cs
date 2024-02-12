namespace CONNECT4_CON
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;

            Game g = new Game();
            g.PlayGame();
        }
    }
}
