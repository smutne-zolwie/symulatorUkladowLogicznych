using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuUIManager : MonoBehaviour
{
    public GameObject ProjectPrefab;
    public GameObject Content;

    private GameObject NewProjectPanel;
    // Start is called before the first frame update
    void Awake()
    {
        NewProjectPanel = GameObject.Find("NewProject");
        NewProjectPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewProject()
    {
        string name = NewProjectPanel.GetComponentInChildren<TMP_InputField>().textComponent.text;
        if (name != null && !MenuManager.Instance.PathsList.Contains(name))
        {
            GameObject newProject = Instantiate(ProjectPrefab, Content.transform);
            Button deleteButton = null, openButton = null;
            foreach (Transform child in newProject.transform)
            {
                if (child.gameObject.name == "Open")
                    openButton = child.gameObject.GetComponent<Button>();
                else if (child.gameObject.name == "Delete")
                    deleteButton = child.gameObject.GetComponent<Button>();
            }
            deleteButton.onClick.AddListener(() => { Delete(deleteButton.gameObject); });
            newProject.GetComponentInChildren<TMP_Text>().text = name;
            RectTransform transform = newProject.GetComponent<RectTransform>();
            if (MenuManager.Instance.ProjectList.Count > 0)
            {
                RectTransform lasTransform = MenuManager.Instance.ProjectList[MenuManager.Instance.ProjectList.Count - 1]
                    .GetComponent<RectTransform>();
                transform.anchorMax = new Vector2(0.98f, lasTransform.anchorMin.y);
                transform.anchorMin = new Vector2(0.02f, transform.anchorMax.y - 0.02f);
            }
            else
            {
                transform.anchorMax = new Vector2(0.98f, 0.99f);
                transform.anchorMin = new Vector2(0.02f, 0.97f);
            }
            transform.offsetMax = new Vector2(0, 0);
            transform.offsetMin = new Vector2(0, 0);
            MenuManager.Instance.ProjectList.Add(newProject);
            MenuManager.Instance.PathsList.Add(name.ToString());
            MenuManager.Instance.CreateProject(name);
        }
    }

    public void Delete(GameObject buttonGameObject = null)
    {
        GameObject project = buttonGameObject.transform.parent.gameObject;
        string name = project.GetComponentInChildren<TMP_Text>().text;
        File.Delete(Application.persistentDataPath + "/" + name + ".json");
        int index = MenuManager.Instance.PathsList.FindIndex(a => a == name);
        MenuManager.Instance.PathsList.Remove(name);
        print(index);
        GameObject gameObject = MenuManager.Instance.ProjectList[index];
        MenuManager.Instance.ProjectList.RemoveAt(index);
        if (index > 0)
        {
            for (int i = index; i < MenuManager.Instance.ProjectList.Count; i++)
            {
                RectTransform transform = MenuManager.Instance.ProjectList[i].GetComponent<RectTransform>();
                RectTransform lasTransform = MenuManager.Instance.ProjectList[i - 1]
                    .GetComponent<RectTransform>();
                transform.anchorMax = new Vector2(0.98f, lasTransform.anchorMin.y);
                transform.anchorMin = new Vector2(0.02f, transform.anchorMax.y - 0.02f);
                transform.offsetMax = new Vector2(0, 0);
                transform.offsetMin = new Vector2(0, 0);
            }
        }
        else if (index == 0 && MenuManager.Instance.ProjectList.Count >= 1)
        {
            RectTransform transform = MenuManager.Instance.ProjectList[index].GetComponent<RectTransform>();
            transform.anchorMax = new Vector2(0.98f, 0.99f);
            transform.anchorMin = new Vector2(0.02f, 0.97f);
            transform.offsetMax = new Vector2(0, 0);
            transform.offsetMin = new Vector2(0, 0);
        }
        Destroy(gameObject);
    }
    public void OpenPanel(GameObject panel)
    {
        panel.SetActive(true);
    }

    public void ClosePanel(GameObject panel)
    {
        panel.SetActive(false);
    }
}
