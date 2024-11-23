using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthView : View
{
    private int _health;
    private int _maxHealth;

    public int Health
    {
        get => _health;
        set
        {
            _health = value;
            
            SetHealth(_health, _maxHealth);
        }
    }

    public int MaxHealth
    {
        get => _maxHealth;
        set
        {
            _maxHealth = value;
            
            SetHealth(_health, _maxHealth);
        }
    }
    
    [SerializeField] private TextMeshProUGUI numbersHealth;
    [SerializeField] private Slider sliderHealth;
    
    public override void Initialize()
    {
        
    }

    private void SetHealth(int value, int maxHealth)
    {
        numbersHealth.text = $"{value}/{maxHealth}";
        sliderHealth.value = value;
    }
}