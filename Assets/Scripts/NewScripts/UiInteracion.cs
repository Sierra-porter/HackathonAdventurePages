using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiInteracion : MonoBehaviour
{
[SerializeField] private List<Sprite> EnergyLvlsSprite;
[SerializeField] private List<Sprite> HealthLvlsSprite;
[SerializeField] private Sprite noneEffectActionSprite;

[SerializeField] private CharacterStats chStat;
[SerializeField] private GameStats gameStat;
[SerializeField] private TimeMaster timeMaster;
[SerializeField] private Gameover gameOver;



[SerializeField] private Animator animator;


[SerializeField] private GameObject Tutorial;
[SerializeField] private GameObject actionHolder;
[SerializeField] private GameObject SuccessPanel;
[SerializeField] private GameObject ActionPanel;
[SerializeField] private GameObject EnergyBox;
[SerializeField] private GameObject VillagerImage;
[SerializeField] private List<GameObject> PhobiaPanel;
[SerializeField] private int phobiaPanelCount = 1;
[SerializeField] public bool isSuccess = false;
//[SerializeField] private Collider CurrentActionCollider;
//[SerializeField] private Collider CharacterCollider;
//[SerializeField] public bool isVisible = false;


[SerializeField] public string currentPhobiaName = "";
[SerializeField] public string currentActionName = "";

[SerializeField] public string currentUnsuccessfulDesc = "";
[SerializeField] public string currentSuccessfulDesc = "";
[SerializeField] public string currentCryticallDesc = "";
[SerializeField] public string currentUnsuccessfulNightDesc = "";
[SerializeField] public string currentUnvisibleDesc = "";
    
[SerializeField] public float currentHealthDamage = 0;
[SerializeField] public int currentEnergyCost = 0;
[SerializeField] public bool currentIsUsed = false;
[SerializeField] public float nightDamageMultiplier = 1;
[SerializeField] public float damageDistanceMultiplier;


void FixedUpdate()
{
    energyCheckBox();
    timeOutCheck();
}
private void nightDamageCheck(int i)
{
    if(chStat.PhobiasPrefabs[i].nightMultiplyer && timeMaster.isNight)
    {
    gameStat.score += 100;
    nightDamageMultiplier = 2f;
    gameStat.regenTimeDelay = 5;
    SuccessPanel.transform.Find("Description").GetComponent<TextMeshProUGUI>().text = 
    currentCryticallDesc;
    animator.SetBool("EnergyRegenHigh", true);
    animator.SetBool("EnergyRegenNormal", false);
    animator.SetBool("EnergyRegenLow", false);
    }
    if(chStat.PhobiasPrefabs[i].nightMultiplyer == false && timeMaster.isNight == true)
    {
        gameStat.score -= 300;
        nightDamageMultiplier = 0f;
        gameStat.regenTimeDelay = 20f;
        SuccessPanel.transform.Find("Description").GetComponent<TextMeshProUGUI>().text = 
        currentUnsuccessfulNightDesc;
        animator.SetBool("EnergyRegenHigh", false);
        animator.SetBool("EnergyRegenNormal", false);
        animator.SetBool("EnergyRegenLow", true);

    }
}

private void timeOutCheck()
{
    if(timeMaster.day >= 1)
    {
        gameOver.isWin = false;
        gameOver.gameOverMethod();
    }
}
private void healthCheck()
{
    if(chStat.characterHealth<=0)
    {
        VillagerImage.GetComponent<Image>().sprite = HealthLvlsSprite[4];
        gameOver.isWin = true;
        gameOver.gameOverMethod();
    }
    if(chStat.characterHealth>0) 
        VillagerImage.GetComponent<Image>().sprite = HealthLvlsSprite[3];
    if(chStat.characterHealth>25) 
        VillagerImage.GetComponent<Image>().sprite = HealthLvlsSprite[2];
    if(chStat.characterHealth>50) 
        VillagerImage.GetComponent<Image>().sprite = HealthLvlsSprite[1];
    if(chStat.characterHealth>75) 
        VillagerImage.GetComponent<Image>().sprite = HealthLvlsSprite[0];
}
private void energyCheckPanel()
{
    if(currentEnergyCost>0) 
        actionHolder.transform.Find("ActionButton").GetComponent<Image>().sprite = EnergyLvlsSprite[0];
    if(currentEnergyCost>20) 
        actionHolder.transform.Find("ActionButton").GetComponent<Image>().sprite = EnergyLvlsSprite[1];
    if(currentEnergyCost>50) 
        actionHolder.transform.Find("ActionButton").GetComponent<Image>().sprite = EnergyLvlsSprite[2];
    if(currentEnergyCost>75) 
        actionHolder.transform.Find("ActionButton").GetComponent<Image>().sprite = EnergyLvlsSprite[3];
    if(currentEnergyCost==100) 
        actionHolder.transform.Find("ActionButton").GetComponent<Image>().sprite = EnergyLvlsSprite[4];
}
private void energyCheckBox()
{
    if(gameStat.houseEnergy < 0 )
        gameStat.houseEnergy = 0;
    if(gameStat.houseEnergy >= 0) 
        EnergyBox.transform.Find("EnergyBoxImage").GetComponent<Image>().sprite = EnergyLvlsSprite[0];
    if(gameStat.houseEnergy > 20) 
        EnergyBox.transform.Find("EnergyBoxImage").GetComponent<Image>().sprite = EnergyLvlsSprite[1];
    if(gameStat.houseEnergy > 50) 
        EnergyBox.transform.Find("EnergyBoxImage").GetComponent<Image>().sprite = EnergyLvlsSprite[2];
    if(gameStat.houseEnergy > 75) 
        EnergyBox.transform.Find("EnergyBoxImage").GetComponent<Image>().sprite = EnergyLvlsSprite[3];
    if(gameStat.houseEnergy == 100) 
        EnergyBox.transform.Find("EnergyBoxImage").GetComponent<Image>().sprite = EnergyLvlsSprite[4];
    
}

//private void isVisibleCheck(){}
public void actionPlanelInitialization()
{
    energyCheckPanel();
    SuccessPanel.SetActive(false);
    actionHolder.transform.Find("ActionButton").Find("ActionButtonText").GetComponent<TextMeshProUGUI>().text = currentActionName;
}

public void actionActivation()
{
    
       
        if(gameStat.houseEnergy >= currentEnergyCost) //проверка хватает ли энергии на действие
        {
        gameStat.houseEnergy -= currentEnergyCost;
        energyCheckBox();
        Tutorial.SetActive(false);
        
        //isVisibleCheck();
            //if (isVisible) //проверка на видимость объекта
            //{
                for (int i = 0; i < 3; i++)
                {
                    if (chStat.PhobiasPrefabs[i].Name == currentPhobiaName) //Проверка действие вообще действует ли на персонажа
                    {
                        isSuccess = true;
                        gameStat.score += 100;
                        gameStat.regenTimeDelay = 10;
                        animator.SetBool("EnergyRegenHigh", false);
                        animator.SetBool("EnergyRegenNormal", true);
                        animator.SetBool("EnergyRegenLow", false);


                        if (!chStat.PhobiasPrefabs[i].isKnown) //Проверка на новую закрытую фобию 
                        {
                            chStat.PhobiasPrefabs[i].isKnown = true;
                            PhobiaPanel[phobiaPanelCount].transform.Find("PhobiaImg").GetComponent<Image>().sprite = chStat.PhobiasPrefabs[i].Icon;
                            phobiaPanelCount++;
                            gameStat.score += 200;
                            gameStat.regenTimeDelay = 5;
                            animator.SetBool("EnergyRegenHigh", true);
                            animator.SetBool("EnergyRegenNormal", false);
                            animator.SetBool("EnergyRegenLow", false);
                           
                        }

                        SuccessPanel.transform.Find("ActionPhobiaImg").GetComponent<Image>().sprite = chStat.PhobiasPrefabs[i].Icon;
                        SuccessPanel.transform.Find("Description").GetComponent<TextMeshProUGUI>().text = currentSuccessfulDesc;
                        nightDamageCheck(i);
                        chStat.characterHealth -= currentHealthDamage * chStat.characterDurability * nightDamageMultiplier;
                        

                        ActionPanel.SetActive(false);
                        SuccessPanel.SetActive(true);
                        healthCheck();

                    }
                }
                if (!isSuccess)
                { //Если выполняется это условие, значит действие видно, но эффекта от него нет 
                    SuccessPanel.transform.Find("ActionPhobiaImg").GetComponent<Image>().sprite = noneEffectActionSprite;
                    SuccessPanel.transform.Find("Description").GetComponent<TextMeshProUGUI>().text = currentUnsuccessfulDesc;
                    gameStat.score -= 150;
                    gameStat.regenTimeDelay = 15;
                        animator.SetBool("EnergyRegenHigh", false);
                        animator.SetBool("EnergyRegenNormal", false);
                        animator.SetBool("EnergyRegenLow", true);
                    

                    ActionPanel.SetActive(false);
                    SuccessPanel.SetActive(true);
                }
            /*
            }else{
                SuccessPanel.transform.Find("ActionPhobiaImg").GetComponent<Image>().sprite = noneEffectActionSprite;
                SuccessPanel.transform.Find("Description").GetComponent<TextMeshProUGUI>().text = currentUnvisibleDesc;
                gameStat.score -= 150;

                ActionPanel.SetActive(false);
                SuccessPanel.SetActive(true);
            }
            */

        //isVisible = false;
        isSuccess = false;
        nightDamageMultiplier = 1f;
        
     
        }



}

}
