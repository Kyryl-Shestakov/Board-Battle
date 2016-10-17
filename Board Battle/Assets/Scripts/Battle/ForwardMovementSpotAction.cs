using System;

namespace Battle
{
    public class ForwardMovementSpotAction : SpotAction
    {
        public override void PerformAction(Action postAction)
        {
            //throw new System.NotImplementedException();
            postAction();
        }
    }
}
