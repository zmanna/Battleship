using System.Security.Cryptography.X509Certificates;

class Player{
    private List<(int, int)> previousShots = new List<(int, int)>(); //Stores a list of previous shots, so that the player cannot shoot at the same coordinate twice
    private bool[,] board = new bool[10,10]; // 10x10 board to store the ship locations
    public int life; // Number of ship sections remaining
    public Player(int shipNum, string name) //Player constructor that takes the number of ships and the player name as arguments
    {
        string playerName = name; //Player name
        Ship[] ships = new Ship[shipNum]; //List of ships
        this.CreateShips(ships, shipNum); //Creates the ship objects and places them on the board
        life = (shipNum * (shipNum +1))/2; //Life is derived from the number of ships
    }
    struct Ship
    {
        public int x;
        public int y;
        public int length;
        public bool vertical;
    }

    private void CreateShips(Ship[]ships, int shipNum)
    {
        for (int i = 0; i < shipNum; i++)
        {
            Console.WriteLine("Enter the x coordinate of ship " + (i + 1) + ": ");
            int x = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter the y coordinate of ship " + (i + 1) + ": ");
            int y = Convert.ToInt32(Console.ReadLine());

            int length = i+1; //Length of the ship is derived from it's index

            Console.WriteLine("Enter the orientation of ship " + (i + 1) + " (vertical/horizontal): ");//Verticality of the ship
            string orientation = Console.ReadLine();
            if (orientation == "vertical")
            {
                ships[i].vertical = true;
            }
            else
            {
                ships[i].vertical = false;
            }

            //Placing ships on the board
            if (ships[i].vertical)
            {
                for (int j = 0; j < length; j++)
                {
                    board[x, y + j] = true;//Board spaces are set to true, to indicate that a ship is present
                }
            }
            else
            {
                for (int j = 0; j < length; j++)
                {
                    board[x + j, y] = true;
                }
            }
        }
    }
    public void TakeTurn(Player opponent)
    {
        Console.WriteLine("Enter the x coordinate of your shot: ");
        int shotX = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Enter the y coordinate of your shot: ");
        int shotY = Convert.ToInt32(Console.ReadLine());
        (int,int) shotCoordinate = (shotX, shotY);
        this.CheckShot(opponent, shotCoordinate);
        if (previousShots.Contains(shotCoordinate))
        {
            Console.WriteLine("You have already shot at this coordinate. Please try again.");
            this.TakeTurn(opponent);
        }
        else
        {
            previousShots.Add(shotCoordinate);
            CheckShot(opponent, shotCoordinate);
        }
    }

    bool CheckShot(Player opponent, (int,int) shotCoordinate)
    {
        if (opponent.board[shotCoordinate.Item1, shotCoordinate.Item2])
        {
            Console.WriteLine("Hit!");
            opponent.board[shotCoordinate.Item1, shotCoordinate.Item2] = false; //Board space is set to false, to indicate that the ship section has been hit (Shouldn't be needed, but just in case the  player can somehow shoot at the same coordinate twice)
            opponent.life--;//Life is deducted from the opponent
            return true;
        }
        else
        {
            Console.WriteLine("Miss!");
            return false;
        }
    }
}