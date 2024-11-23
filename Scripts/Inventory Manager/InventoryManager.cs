using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    [SerializeField] private List<RectTransform> slotAbilities;
    [SerializeField] private List<RectTransform> slotItems;
    [Space]
    [SerializeField] private List<RectTransform> abilities;
    [SerializeField] private List<RectTransform> items;
    [Space]
    [SerializeField] private int maxAbilitiesSlots;
    [SerializeField] private int maxItemsSlots;
    
    private void Awake()
    {
        Instance = this;
    }

    public void AddAbility(Ability ability)
    {
        if (slotAbilities.Count < maxAbilitiesSlots)
        {
            var slot =  FindEmptySlot(slotAbilities);
            
            if(slot != null)
                ability.transform.parent = slot;
        }
    }
    
    public void AddItem(Item item)
    {
        if (slotItems.Count < maxItemsSlots)
        {
            var slot =  FindEmptySlot(slotAbilities);
            
            if(slot != null)
                item.transform.parent = slot;
        }
    }

    private RectTransform FindEmptySlot(List<RectTransform> slots)
    {
        foreach (var slot in slots)
        {
            if (slot.childCount < 1)
            {
                return slot;
            }
        }

        return null;
    }
}