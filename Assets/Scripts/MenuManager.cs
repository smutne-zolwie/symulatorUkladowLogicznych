using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;
    public List<string> PathsList = new List<string>();
    public List<GameObject> ProjectList = new List<GameObject>();
    public string nick;
    public string bNick;
    public int bestScore;
    public string newProjectName;
    public TMP_InputField NewProjectTmpInputField;
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
        public string bNick;
        public int bestScore;
    }
    /*[System.Serializable]
    class SaveProject
    {

    }*/
    public void CreateProject(string name)
    {
        File.Create(Application.persistentDataPath + "/" + name + ".json");
    }
    public void SaveDataF()
    {
        SaveData data = new SaveData();
        data.bNick = bNick;
        data.bestScore = bestScore;
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }
    public void LoadDataF()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            bNick = data.bNick;
            bestScore = data.bestScore;
        }
    }
}
