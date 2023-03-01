using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

[DefaultExecutionOrder(1000)]
public class MenuUIHandler : MonoBehaviour
{
    public static MenuUIHandler Instance;

    public TextMeshProUGUI PlayerNameText;
    public TextMeshProUGUI highScore;

    public string playerName;
    public string bestPlayer;
    public int bestScore;

    // Start is called before the first frame update
    void Start()
    {
        LoadScore();
        highScore.text = "Best Score: " + MenuUIHandler.Instance.bestPlayer + " : " + MenuUIHandler.Instance.bestScore;
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

    }

    [System.Serializable]
    class SaveData
    {
        public string playerName;
        public int score;
        public int bestScore;
    }

    public void SaveScore(int score)
    {
        SaveData data = new SaveData();

        if (playerName != null)
        {
            data.playerName = playerName;
        } else
        {
            data.playerName = "Player";
        }
        
        data.bestScore = score;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            bestPlayer = data.playerName;
            bestScore = data.bestScore;
        }
    }

    public void GetName(string name)
    {
        playerName = name;
    }

    public void StartNew()
    {
        GetName(PlayerNameText.text);
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