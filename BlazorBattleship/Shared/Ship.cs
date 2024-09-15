namespace BlazorBattleship.Shared
{
    public class Ship
    {
        public List<(int x, int y)> shipCoords;
        public int length;
        public int hp;
        public string name;

        public Ship(int length, int hp, string name, List<(int x, int y)> coords)
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

        public bool IsHit(int x, int y)
        {
            return shipCoords.Contains((x, y));
        }

        public void DeductHP()
        {
            if (hp > 0)
            {
                this.hp--;
            }
        }
    }
}
