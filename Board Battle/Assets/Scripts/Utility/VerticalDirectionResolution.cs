using UnityEngine;

namespace Utility
{
    public class VerticalDirectionResolution : DirectionResolution
    {
        public override float DetermineDistance(Vector3 first, Vector3 second)
        {
            return second.z - first.z;
        }

        public override float DetermineLift(Vector3 origin)
        {
            return origin.z;
        }
    }
}
