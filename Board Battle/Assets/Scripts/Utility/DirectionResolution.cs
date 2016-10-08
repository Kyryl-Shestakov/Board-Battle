using UnityEngine;

namespace Utility
{
    public abstract class DirectionResolution
    {
        public abstract float DetermineDistance(Vector3 first, Vector3 second);
        public abstract float DetermineLift(Vector3 origin);
    }
}
