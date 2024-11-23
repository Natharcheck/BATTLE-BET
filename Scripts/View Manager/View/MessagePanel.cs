using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MessagePanel : View
{
    public enum Type
    {
        Successful,
        Failed
    }

    public Type messageType;
    
    [SerializeField] private Image currentImageColor;
                     public  TextMeshProUGUI table;
                     public  int lifeTime;
    [Space] 
    [SerializeField] private List<Color> colors;

    private Animation _animations;

    private void Start()
    {
        _animations = GetComponent<Animation>();
    }

    public override void Initialize()
    {
        InitMessageType();
        StartCoroutine(Timer());
    }
    
    private void InitMessageType()
    {
        switch (messageType)
        {
            case Type.Successful: ShowColor(colors[0]); break;
            case Type.Failed: ShowColor(colors[1]); break;
        }
    }

    private void ShowColor(Color selectColor)
    {
        foreach (var color in colors)
        {
            if (color == selectColor)
                currentImageColor.color = color;
        }
    }
    
    private IEnumerator Timer()
    {
        for (int i = 0; i <= lifeTime; i++)
            yield return new WaitForSeconds(1f);
        
        Hide();
    }
}