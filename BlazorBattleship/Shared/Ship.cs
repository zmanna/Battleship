namespace BlazorBattleship.Shared
{
    public struct Ship
    {
        public (int x, int y) shipCoords;
        public int length;
        public int hp;
        public string name;

        public Ship(int length, int hp, string name, (int x, int y) coords)
        {
            this.length = length;
            this.hp = hp;
            this.name = name;
            this.shipCoords = coords;
        }

        public bool IsDestroyed()
        {
            return hp <= 0;
        }
    }
}
