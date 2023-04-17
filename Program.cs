namespace Final;
class Program
{
    static void Main(string[] args)
    {
        // Create Game
        Game game = new Game();
        game.Run(skipIntro: false);
    }
}
