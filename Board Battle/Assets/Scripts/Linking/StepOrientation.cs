using UnityEngine;
using Utility;

namespace Linking
{
    public abstract class StepOrientation : MonoBehaviour
    {
        public abstract DirectionResolution DetermineDirection();
    }
}
