namespace BlazorBattleship.GameClassLib
{
    class Game
    {
        public void Start()
        {
            Console.WriteLine("Welcome to Battleship!");
            Console.WriteLine("Choose the number of ships you want to play with: ");
            int shipNum = Convert.ToInt32(Console.ReadLine());
            var player1 = new Player(shipNum, "Player 1");
            var player2 = new Player(shipNum, "Player 2");

            while (true)
            {
                player1.TakeTurn(player2);
                if (player2.life == 0)
                {
                    Console.WriteLine("Player 1 wins!");
                    break;
                }
                player2.TakeTurn(player1);
                if (player1.life == 0)
                {
                    Console.WriteLine("Player 2 wins!");
                    break;
                }
            }
        }
    }
}