using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Gameover : MonoBehaviour
{

    [SerializeField] List<AsyncOperation> scenesToLoad = new List<AsyncOperation>();
    [SerializeField] private GameObject LoadingMenu;
    [SerializeField] private Image LoadingProgressBar;

    [SerializeField] private Sprite WinEnding;
    [SerializeField] private GameStats gameStat;
    [SerializeField] private CharacterStats chStat;

    [SerializeField] private GameObject GameOverScreen;
    [SerializeField] private GameObject GamePauseScreen;
    [SerializeField] private GameObject SuccessPanel;
    [SerializeField] private GameObject ActionPanel;
    [SerializeField] private GameObject VillagerBox;
    [SerializeField] private GameObject EnergyBox;
    [SerializeField] private GameObject ClockBox;

    [SerializeField] public bool isWin = false;

    
    public void quitGame()
    {
        Application.Quit();
    }
    public void restartLvl()
    {
         SceneManager.LoadScene("Scenes/MainScene");
    }
    public void continueGame()
    {
         LoadingMenu.SetActive(true);
         scenesToLoad.Add(SceneManager.LoadSceneAsync("MainMenu"));
         StartCoroutine(LoadingScreen());
    }



    public void gameOverMethod()
    {
        GamePauseScreen.SetActive(false);
        SuccessPanel.SetActive(false);
        ActionPanel.SetActive(false);
        EnergyBox.SetActive(false);
        ClockBox.SetActive(false);
        VillagerBox.SetActive(false);

        if(isWin){
            GameOverScreen.transform.Find("GameOverText").
            GetComponent<TextMeshProUGUI>().text = "Победа";
            GameOverScreen.transform.Find("GameOverStatsBox").Find("VillagerPortret").GetComponent<Image>().sprite
         = WinEnding;
        }else{
            GameOverScreen.transform.Find("GameOverText").
            GetComponent<TextMeshProUGUI>().text = "Поражение";
            GameOverScreen.transform.Find("GameOverStatsBox").Find("VillagerPortret").GetComponent<Image>().sprite
         = VillagerBox.transform.Find("VillagerImage").GetComponent<Image>().sprite;
        }
        

        GameOverScreen.transform.Find("GameOverStatsBox").Find("ScoreText").
        GetComponent<TextMeshProUGUI>().text = "cчет: " + gameStat.score;

        GameOverScreen.transform.Find("GameOverStatsBox").Find("VilaggerHPText").
        GetComponent<TextMeshProUGUI>().text = "ментальное здоровье: " + chStat.characterHealth;

        GameOverScreen.transform.Find("GameOverStatsBox").Find("HouseEnergyText").
        GetComponent<TextMeshProUGUI>().text = "энергия дома: " + gameStat.houseEnergy;



        GameOverScreen.SetActive(true);

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
