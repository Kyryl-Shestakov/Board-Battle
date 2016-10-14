using System;

namespace Utility
{
    public class DoubleExecutionCompletionEventResolution
    {
        private Action _action;

        public DoubleExecutionCompletionEventResolution(Action firstAction)
        {
            _action = firstAction;
        }
        public void Resolve(Action action)
        {
            _action();
            _action = action;
        }
    }

    //public class FirstDoubleExecutionCompletionEventResolution : DoubleExecutionCompletionEventResolution
    //{
    //    public override void Resolve(Action action)
    //    {
    //        throw new System.NotImplementedException();
    //    }
    //}

    //public class SecondDoubleExecutionCompletionEventResolution : DoubleExecutionCompletionEventResolution
    //{
    //    public override void Resolve(Action action)
    //    {
    //        throw new System.NotImplementedException();
    //    }
    //}
}
