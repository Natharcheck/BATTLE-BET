using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbilityPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI name;
    [SerializeField] private TextMeshProUGUI optional;
    [SerializeField] private Image icon;
    [Space]
    public Button btnChoose;

    public void InitAbilityPanel(string nameAbility, string optionalAbility, Sprite iconAbility)
    {
        name.text = nameAbility;
        optional.text = optionalAbility;
        icon.sprite = iconAbility;
    }
}