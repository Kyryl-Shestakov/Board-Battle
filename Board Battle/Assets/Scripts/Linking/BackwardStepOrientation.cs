using Utility;

namespace Linking
{
    public class BackwardStepOrientation : StepOrientation
    {
        public override DirectionResolution DetermineDirection()
        {
            return new VerticalDirectionResolution();
        }
    }
}
