using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public abstract class JoystickHandler : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private Canvas canvas;
    [SerializeField] public RectTransform background;
    [SerializeField] public RectTransform pointer;
    [Space]
    [SerializeField] public float distanceCancel;
    
    private Vector2 _centerPos;
    private Vector2 _beginPos;
    private Vector2 _dragPos;
    
    private float _radius;
    
    private Image _backgroundImage;
    private Image _pointerImage;

    private Color _backgroundColor;
    private Color _pointerColor;

    protected PointerEventData EventData;
    protected float Distance;
    protected float Angle;
    
    protected UnityEvent BeginDrag;
    protected UnityEvent Drag;
    protected UnityEvent EndDrag;

    protected void Start()
    {
        Initialize();
    }

    public virtual void Initialize()
    {
        BeginDrag = new UnityEvent();
        Drag = new UnityEvent();
        EndDrag = new UnityEvent();
        
        _backgroundImage = background.GetComponent<Image>();
        _pointerImage = pointer.GetComponent<Image>();

        _backgroundColor = _backgroundImage.color;
        _pointerColor = _pointerImage.color;
        
        _centerPos = pointer.position;
        
        Reset();
    }

    public void Reset()
    {
        Vector2 center = new Vector2(0.5f, 0.5f);
        
        background.pivot = center;
        pointer.anchorMin = center;
        pointer.anchorMax = center;
        pointer.pivot = center;
        pointer.anchoredPosition = Vector2.zero;
        
        _radius = background.sizeDelta.x / 2;
        _radius *= canvas.scaleFactor;
    }
    
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        Reset();

        EventData = eventData;
        _beginPos = eventData.position;
        
        _backgroundImage.color = _backgroundColor;
        _pointerImage.color = _pointerColor;

        BeginDrag.Invoke();
    }

    public void OnDrag(PointerEventData eventData)
    {
        EventData = eventData;
        _dragPos = eventData.position;
        
        Vector2 dir = _dragPos - _beginPos;
        
        Distance = Vector2.Distance(_dragPos, _beginPos);
        pointer.position = Distance > _radius ? (_centerPos + dir.normalized * _radius) : (_centerPos + dir);
        
        Vector2 vector = (_dragPos - _beginPos).normalized;
        Angle = Mathf.Atan2(vector.x, vector.y) * Mathf.Rad2Deg;
        Angle = Angle < 0 ? 360 + Angle : Angle;

        Drag.Invoke();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        EventData = eventData;
        
        _dragPos = Vector2.zero;
        _beginPos = Vector2.zero;
        pointer.position = _centerPos;

        Reset();
        EndDrag.Invoke();
    }

    protected void CancelEffect()
    {
        if (Distance > (_radius + distanceCancel - 50))
        {
            _backgroundImage.color = Color.red;
            _pointerImage.color = Color.red;
        }
        else if (Distance < (_radius + distanceCancel - 50))
        {
            _backgroundImage.color = _backgroundColor;
            _pointerImage.color = _pointerColor;
        }
        
        if (Distance > (_radius + distanceCancel))
        {
            _backgroundImage.color = _backgroundColor;
            _pointerImage.color = _pointerColor;
            
            OnEndDrag(EventData);
        }
    }
}