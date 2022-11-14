using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public static PlayerInfo instance;

    public TextMeshProUGUI bestScoreText;
    public string playerName;
    public int playerScore = 0;
    public int bestScore = 0;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        LoadInfo();
    }

    [System.Serializable]
    class SaveData
    {
        public string playerName;
        public int playerScore;
    }

    public void SaveInfo()
    {
        SaveData data = new SaveData();
        if (data.playerScore < playerScore)
        {
            data.playerName = playerName;
            data.playerScore = playerScore;

            string json = JsonUtility.ToJson(data);
            File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
        }
    }

    public void LoadInfo()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            if (data.playerName != "" && data.playerScore != 0)
            {
                bestScoreText.text = "Best Score: " + data.playerName + ": " + data.playerScore;
            }

            bestScore = data.playerScore;
        }
    }
}
