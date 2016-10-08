using Utility;

namespace Linking
{
    public class ForwardStepOrientation : StepOrientation
    {
        public override DirectionResolution DetermineDirection()
        {
            return new VerticalDirectionResolution();
        }
    }
}
