using System.Collections.Generic;
using UnityEngine;

public class ViewManager : MonoBehaviour
{
    private static ViewManager s_instance;
    
    [SerializeField] private View startingView;
    [SerializeField] private MessageView messageView;
    [SerializeField] private ProfileView profileView;
    //RoundView
    [Space]
    [SerializeField] private View[] views;

    private View _currentView;

    private readonly Stack<View> _history = new Stack<View>();

    private void Awake() => s_instance = this;

    public static T GetView<T>() where T : View
    {
        for (int i = 0; i < s_instance.views.Length; i++)
        {
            if (s_instance.views[i] is T tView)
            {
                return tView;
            }
        }

        return null;
    }

    public static void InitializeProfile()
    {
        s_instance.profileView.Initialize();
    }
    
    public static void ShowMessage(string text, MessagePanel.Type messageType, int lifeTime = 3)
    {
        s_instance.messageView.CreateMessage(text, messageType, lifeTime);
    }
    
    public static void Show<T>(bool remember = true) where T : View
    {
        for (int i = 0; i < s_instance.views.Length; i++)
        {
            if (s_instance.views[i] is T)
            {
                if (s_instance._currentView != null)
                {
                    if (remember)
                    {
                        s_instance._history.Push(s_instance._currentView);
                    }

                    s_instance._currentView.Hide();
                }

                s_instance.views[i].Show();

                s_instance._currentView = s_instance.views[i];
            }
        }
    }

    public static void ShowOnly<T>() where T : View
    {
        for (int i = 0; i < s_instance.views.Length; i++)
        {
            if (s_instance.views[i] is T)
            {
                s_instance.views[i].Show();
                s_instance._currentView = s_instance.views[i];
            }
        }
    }

    public static void Show(View view, bool remember = true)
    {
        if (s_instance._currentView != null)
        {
            if (remember)
            {
                s_instance._history.Push(s_instance._currentView);
            }

            s_instance._currentView.Hide();
        }

        view.Show();

        s_instance._currentView = view;
    }

    public static void ShowLast()
    {
        if (s_instance._history.Count != 0)
        {
            Show(s_instance._history.Pop(), false);
        }
    }

    private void Start()
    {
        foreach (var view in views)
        {
            view.Initialize();
            view.Hide();
        }
        
        if(messageView != null)
            messageView.Initialize();

        if (startingView != null)
            Show(startingView);
    }
}