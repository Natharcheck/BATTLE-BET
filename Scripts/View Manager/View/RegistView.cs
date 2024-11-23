using Network;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RegistView : View
{
    [SerializeField] private TMP_InputField inputUsername;
    [SerializeField] private TMP_InputField inputPassword;
    [SerializeField] private TMP_InputField inputRepeatPassword;
    [Space]
    [SerializeField] private Button btnRegistration;
    [SerializeField] private Button btnLoginView;


    public override void Initialize()
    {
        btnRegistration.onClick.AddListener(() => 
            LobbyManager.Instance.Registration(inputUsername.text, inputPassword.text, inputRepeatPassword.text));
        
        btnLoginView.onClick.AddListener(() => ViewManager.Show<LoginView>());
    }
}