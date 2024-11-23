using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadSceneView : View
{
    public string SceneName { get; set; }
    
    [SerializeField] private Image progressImage;
    [SerializeField] private TextMeshProUGUI progressText;
    
    public override void Initialize()
    {
        
    }

    public override void Show()
    {
        base.Show();
        StartCoroutine(LoadingScene());
    }
    
    private IEnumerator LoadingScene()
    {
        yield return new WaitForSeconds(1f);
        
        var asyncOperation = SceneManager.LoadSceneAsync(SceneName);

        while (asyncOperation.isDone == false)
        {
            var progress = asyncOperation.progress / 0.9f;
            progressImage.fillAmount = progress;
            progressText.text = $"{progress * 100:0}%";
            
            yield return 0;
        }
    }
}