using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{

    public GameObject Menu;
    public GameObject LoadingMenu;
    public Image LoadingProgressBar;


    List<AsyncOperation> scenesToLoad = new List<AsyncOperation>();


    public void quitGame()
    {
        Application.Quit();
    }
    public void StartGame()
    {
        Menu.SetActive(false);
        LoadingMenu.SetActive(true);
        scenesToLoad.Add(SceneManager.LoadSceneAsync("MainScene"));
        StartCoroutine(LoadingScreen());
        
    }
    //Application.Quit();

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator LoadingScreen()
    {
        float totalProgress = 0;
        for (int i = 0; i < scenesToLoad.Count; i++)
        {
            while(!scenesToLoad[i].isDone)
            {
                totalProgress += scenesToLoad[i].progress;
                LoadingProgressBar.fillAmount = totalProgress / scenesToLoad.Count;
                yield return null;
            }
        }
    }
}
