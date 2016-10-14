using System;
using UnityEngine;
using System.Collections;

public class CardMovement : MonoBehaviour
{
    public static float ProximityThreshold = 0.1f;

    public float CardSpeed;

    public static bool AreNear(Vector3 a, Vector3 b)
    {
        var distance = Vector3.Distance(a, b);

        return distance < ProximityThreshold;
    }

    public IEnumerator Move(Vector3 destinationPosition, Action<GameObject> postAction)
    {
        var sourcePosition = gameObject.transform.position;
        var currentInterpolant = Time.deltaTime;

        do
        {
            var step = Vector3.Lerp(sourcePosition, destinationPosition, CardSpeed*currentInterpolant);

            gameObject.transform.position = step;

            yield return step;
            
            currentInterpolant += Time.deltaTime;
        }
        while (!AreNear(transform.position, destinationPosition));
        
        gameObject.transform.position = destinationPosition;
        yield return destinationPosition;

        postAction(gameObject);
    }
}
