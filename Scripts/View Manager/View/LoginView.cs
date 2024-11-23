using Network;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoginView : View
{
    [SerializeField] private TMP_InputField inputUsername;
    [SerializeField] private TMP_InputField inputPassword;
    [Space]
    [SerializeField] private Button btnLogin;
    [SerializeField] private Button btnRegistrationView;
    public override void Initialize()
    {
        btnLogin.onClick.AddListener( () => 
            LobbyManager.Instance.Authorization(inputUsername.text, inputPassword.text));
        
        btnRegistrationView.onClick.AddListener(() => ViewManager.Show<RegistView>());
    }
}