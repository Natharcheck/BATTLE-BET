using System;
using UnityEngine;

public class AbilitiesChooseView : View
{
    [SerializeField] private AbilityPanel prefab;
    [Space]
    [SerializeField] private int count;
    [SerializeField] private bool isExpand;
    
    private PoolMono<AbilityPanel> _pool;

    private void Start()
    {
        _pool = new PoolMono<AbilityPanel>(prefab, count, isExpand, transform);
    }

    public override void Initialize()
    {
        
    }
    
    public void CreateAbility(string nameAbility, string optionalAbility, Sprite iconAbility)
    {
        var abilityPanel = _pool.GetFreeElement();
            abilityPanel.InitAbilityPanel(nameAbility, optionalAbility, iconAbility);
            abilityPanel.btnChoose.onClick.AddListener(() => ChooseAbility(nameAbility));
    }
    
    private void ChooseAbility(string nameAbility)
    {
        print($"Choose {nameAbility}");
        
    }
}