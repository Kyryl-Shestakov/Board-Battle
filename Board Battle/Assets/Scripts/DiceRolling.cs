using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class DiceRolling : MonoBehaviour
{
    public const int MinStepCount = 1;
    public const int MaxStepCount = 6;
    public Text DieRollValue;

    public int RollTheDie()
    {
        int rollOutcomeValue = Random.Range(MinStepCount, MaxStepCount + 1);
        DieRollValue.text = rollOutcomeValue.ToString();
        return rollOutcomeValue;
    }
}
