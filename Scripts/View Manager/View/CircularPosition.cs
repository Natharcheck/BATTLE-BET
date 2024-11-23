using System;
using System.Collections.Generic;
using UnityEngine;

public class CircularPosition : MonoBehaviour
{
    [SerializeField] private float radius;
    [SerializeField] private float multiple;
    [Space]
    [SerializeField] private List<RectTransform> slots;

    private void OnDrawGizmosSelected()
    {
        Align();
    }

    public void Align()
    {
        for (var i = 0; i < slots.Count; i++)
        {
            var slot = slots[i];
            var angle = Mathf.PI * i / (multiple);
            
            var x = radius * Mathf.Cos(angle);
            var y = radius * Mathf.Sin(angle);

            slot.anchoredPosition = new Vector2(x, y);
        }
    }
}