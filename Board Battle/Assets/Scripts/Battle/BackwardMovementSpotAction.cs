using System;

namespace Battle
{
    public class BackwardMovementSpotAction : SpotAction
    {
        public override void PerformAction(Action postAction)
        {
            //throw new System.NotImplementedException();
            postAction();
        }
    }
}
