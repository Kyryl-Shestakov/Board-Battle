using Utility;

namespace Linking
{
    public class LeftStepOrientation : StepOrientation
    {
        public override DirectionResolution DetermineDirection()
        {
            return new HorizontalDirectionResolution();
        }
    }
}
