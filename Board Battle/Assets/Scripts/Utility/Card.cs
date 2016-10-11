namespace Utility
{
    public class Card
    {
        public int AirRank { get; private set; }
        public int EarthRank { get; private set; }
        public int FireRank { get; private set; }
        public int WaterRank { get; private set; }

        public Card(int airRank, int earthRank, int fireRank, int waterRank)
        {
            AirRank = airRank;
            EarthRank = earthRank;
            FireRank = fireRank;
            WaterRank = waterRank;
        }
    }
}
