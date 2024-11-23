using UnityEngine;

public abstract class View : MonoBehaviour
{
    [SerializeField] private bool isNotHide;
    public abstract void Initialize();

    public virtual void Hide()
    {
        if(isNotHide)
            return;
        
        gameObject.SetActive(false);
    }

    public virtual void Show() => gameObject.SetActive(true);
}