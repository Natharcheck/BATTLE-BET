using TMPro;
using UnityEngine;

public class RoundView : View
{
    [SerializeField] private TextMeshProUGUI time;
    [SerializeField] private TextMeshProUGUI round;

    public override void Initialize()
    {
        
    }

    public void InitTime(int localTime)
    {
        time.text = $"{localTime}s";
    }
    
    public void InitRound(int quantityRound)
    {
        round.text = $"{quantityRound}s";
    }
}