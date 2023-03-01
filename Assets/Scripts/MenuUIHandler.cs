using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

#if UNITY_EDITOR
using UnityEditor;
#endif

[DefaultExecutionOrder(1000)]
public class MenuUIHandler : MonoBehaviour
{
    public TextMeshProUGUI bestScore;

    // Start is called before the first frame update
    void Start()
    {
        MainManager.Instance.LoadScore();
        bestScore.text = "Best Score: " + MainManager.Instance.bestPlayer + " : " + MainManager.Instance.bestScore;
    }

    public void StartNew()
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}