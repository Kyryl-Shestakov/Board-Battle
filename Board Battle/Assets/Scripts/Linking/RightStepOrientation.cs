using Utility;

namespace Linking
{
    public class RightStepOrientation : StepOrientation
    {
        public override DirectionResolution DetermineDirection()
        {
            return new HorizontalDirectionResolution();
        }
    }
}
