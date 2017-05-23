using System;
using System.Collections;
using UnityEngine;

namespace Utility
{
    /// <summary>
    /// Determines how a player will move
    /// </summary>
    public class MovementInterpolation
    {
        /// <summary>
        /// Movement vector
        /// </summary>
        private readonly Vector3 _difference;
        /// <summary>
        /// A spped multiplier that determines how fast a pawn will move from one spot to another 
        /// </summary>
        private readonly float _speed;
        /// <summary>
        /// Radius of a movement semicircle path
        /// </summary>
        private readonly float _radius;
        /// <summary>
        /// Squared radius of a movement semicircle path precalculated for simplicity and efficiency
        /// </summary>
        private readonly float _squaredRadius;

        public static float ProximityThreshold = 0.1f;

        public static bool AreNear(Vector3 a, Vector3 b)
        {
            var distance = Vector3.Distance(a, b);

            return distance < ProximityThreshold;
        }

        public MovementInterpolation(Vector3 source, Vector3 destination, float speed)
        {
            _difference = destination - source;
            _speed = speed;
            var distance = _difference.magnitude;
            _radius = distance/2.0f;
            _squaredRadius = Mathf.Pow(_radius, 2.0f);
        }

        /// <summary>
        /// Yields a set of positions on a normalized semicircle path
        /// </summary>
        /// <param name="action">Determines what to do with each position</param>
        /// <param name="postAction">Determines what to do after the iteration</param>
        /// <returns>Vector3 enumerator</returns>
        public IEnumerator Iterate(Action<Vector3> action, Action postAction)
        {
            var currentInterpolant = Time.deltaTime;
            Vector3 incrementedPosition;

            do
            {
                incrementedPosition = Vector3.Lerp(Vector3.zero, _difference, _speed * currentInterpolant);
                var liftedIncrementedPosition = TransformToTheCurve(incrementedPosition);

                action(liftedIncrementedPosition);
                yield return liftedIncrementedPosition;
                currentInterpolant += Time.deltaTime;
            }
            while (!AreNear(incrementedPosition, _difference));

            action(_difference);
            yield return _difference;
            postAction();
        }

        /// <summary>
        /// Transforms a position towards destination on a straight line to a position lifted to be on a semicircle path
        /// </summary>
        /// <param name="origin">Position on a line</param>
        /// <returns>Position on a semicircle</returns>
        public Vector3 TransformToTheCurve(Vector3 origin)
        {
            var increment = Mathf.Sqrt(Mathf.Pow(origin.x, 2.0f) + Mathf.Pow(origin.z, 2.0f));
            return new Vector3(origin.x, origin.y + Mathf.Sqrt(_squaredRadius - Mathf.Pow(increment - _radius, 2.0f)), origin.z);
        }
    }
}
