using TMPro;
using UnityEngine;

public class ProfileView : View
{
    [SerializeField] private TextMeshProUGUI username;
    [SerializeField] private TextMeshProUGUI crystals;
    [SerializeField] private TextMeshProUGUI gold;
    
    public override void Initialize()
    {
        username.text = Profile.Username;
        crystals.text = Profile.Crystals.ToString();
        gold.text =     Profile.Gold.ToString();
    }
}