using UnityEngine;

namespace Utility
{
    public class HorizontalDirectionResolution : DirectionResolution
    {
        public override float DetermineDistance(Vector3 first, Vector3 second)
        {
            return second.x - first.x;
        }

        public override float DetermineLift(Vector3 origin)
        {
            return origin.x;
        }
    }
}
