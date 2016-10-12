using System;
using UnityEngine;
using System.Collections;

public class CardMovement : MonoBehaviour
{
    public static float ProximityThreshold = 0.1f;

    public static bool AreNear(Vector3 a, Vector3 b)
    {
        var distance = Vector3.Distance(a, b);

        return distance < ProximityThreshold;
    }

    public IEnumerator Move(Vector3 destinationPosition, Action<GameObject> postAction)
    {
        var currentPosition = gameObject.transform.position;

        //while (!AreNear(currentPosition, destinationPosition))
        //{
        //    var step = Vector3.Lerp(currentPosition, destinationPosition, Time.deltaTime);
        //    //gameObject.transform.Translate(step);
        //    gameObject.transform.position = step;
        //    yield return step;
        //}

        do
        {
            var step = Vector3.Lerp(currentPosition, destinationPosition, Time.deltaTime);
            //gameObject.transform.Translate(step);
            gameObject.transform.position = step;
            currentPosition = step;
            yield return step;
        }
        while (!AreNear(currentPosition, destinationPosition));

        //gameObject.transform.Translate(destinationPosition);
        gameObject.transform.position = destinationPosition;
        postAction(gameObject);
        yield return destinationPosition;
    }
}
