using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;
    public MenuUIManager Menu;
    public List<string> PathsList = new List<string>();
    public List<GameObject> ProjectList = new List<GameObject>();
    public string nick;
    public string bNick;
    public int bestScore;
    public string newProjectName;
    public string activeProject;
    public TMP_InputField NewProjectTmpInputField;
    public Scene LoadingScene, MainScene;

    private void Awake()
    {
        LoadDataF();
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
        public string[] pathList;
        public GameObject[] projectLIst;
    }
    [System.Serializable]
    class SaveProject
    {
        public GameObject[] gates;
        public GameObject[] gatesPrefab;
        public GameObject[] gatesButtons;
    }

    public void Load()
    {

    }
    public void CreateProject(string name)
    {
        File.Create(Application.persistentDataPath + "/" + name + ".json");
    }
    public void SaveDataF()
    {
        SaveData data = new SaveData();
        data.pathList = PathsList.ToArray();
        data.projectLIst = ProjectList.ToArray();
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
            PathsList = data.pathList.ToList();
            ProjectList = data.projectLIst.ToList();
        }
    }

    void OnApplicationQuit()
    {
        SaveDataF();
    }
}
