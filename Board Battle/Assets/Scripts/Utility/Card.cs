namespace Utility
{
    public class Card
    {
        public int AirRank { get; private set; }
        public int EarthRank { get; private set; }
        public int FireRank { get; private set; }
        public int WaterRank { get; private set; }
        public int ForwardStepCount { get; private set; }
        public int BackwardStepCount { get; private set; }

        public Card(int airRank, int earthRank, int fireRank, int waterRank, int forwardStepCount, int backwardStepCount)
        {
            AirRank = airRank;
            EarthRank = earthRank;
            FireRank = fireRank;
            WaterRank = waterRank;
            ForwardStepCount = forwardStepCount;
            BackwardStepCount = backwardStepCount;
        }
    }
}
