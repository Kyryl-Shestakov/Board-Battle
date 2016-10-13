using UnityEngine;
using UnityEngine.UI;
using Utility;

public class CardManagement : MonoBehaviour
{
    public Card CardStats { get; private set; }

    public void FormCardStats(Card cardStats)
    {
        CardStats = cardStats;
         
        var front = transform.FindChild("Front");
        var canvas = front.FindChild("Stats");
        var fireText = canvas.FindChild("Fire Text").GetComponent<Text>();
        var waterText = canvas.FindChild("Water Text").GetComponent<Text>();
        var earthText = canvas.FindChild("Earth Text").GetComponent<Text>();
        var airText = canvas.FindChild("Air Text").GetComponent<Text>();
        var forwardText = canvas.FindChild("Forward Text").GetComponent<Text>();
        var backwardText = canvas.FindChild("Backward Text").GetComponent<Text>();

        fireText.text = cardStats.FireRank.ToString();
        waterText.text = cardStats.WaterRank.ToString();
        earthText.text = cardStats.EarthRank.ToString();
        airText.text = cardStats.AirRank.ToString();
        forwardText.text = cardStats.ForwardStepCount.ToString();
        backwardText.text = cardStats.BackwardStepCount.ToString();
    }
}
