using System;
using UnityEngine;
using System.Collections;
using Battle;
using Utility;

public class PawnMovement : MonoBehaviour
{
    /// <summary>
    /// Used to distinguish between Player and Opponent
    /// </summary>
    public string CharacterName;
    /// <summary>
    /// Holds opposite pawn movement
    /// </summary>
    public PawnMovement OpposingPawnMover;
    /// <summary>
    /// A spot the pawn resides upon currently
    /// </summary>
    private SpotConnection _currentSpot;
    /// <summary>
    /// Specifies the speed multiplier of for the pawn movement
    /// </summary>
    public float PawnSpeed;
    /// <summary>
    /// Returns a SpotAction component of the current spot 
    /// </summary>
    public SpotAction CurrentSpotAction
    {
        get { return _currentSpot.GetComponent<SpotAction>(); }
    }

    /// <summary>
    /// Finds a reference to a starting spot as initialization
    /// </summary>
    protected void Awake()
	{
	    _currentSpot = GameObject.Find("Start Spot").GetComponent<SpotConnection>();
	}

    /// <summary>
    /// Moves a pawn from one spot to another in small steps
    /// </summary>
    /// <param name="nextSpotResolver">Determines the next spot to land on</param>
    /// <param name="postAction">Determines what to do after the movement</param>
    /// <returns></returns>
    public IEnumerator Move(Func<SpotConnection, GameObject> nextSpotResolver, Action postAction)
    {
        var destinationSpot = nextSpotResolver(_currentSpot);

        var sourcePoint = _currentSpot.transform.position;
        var destinationPoint = destinationSpot.transform.position;
        //var startingPosition = sourcePoint + PawnSpotOffset;
        var startingPosition = transform.position;

        //var directionResolver = _currentSpot.GetComponent<StepOrientation>().DetermineDirection();
        var movementInterpolator = new MovementInterpolation(sourcePoint, destinationPoint, PawnSpeed);

        _currentSpot = destinationSpot.GetComponent<SpotConnection>();

        return movementInterpolator.Iterate(p =>
        {
            transform.position = startingPosition + p;
        }, postAction);
    }
}
