using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStats : MonoBehaviour
{
    [SerializeField] public float houseEnergy = 0;
    [SerializeField] public int score = 0;
    [SerializeField] public float regenTimeDelay;
    [SerializeField] public float regenPercentAmount = 0.10f;
    [SerializeField] private bool isRegen = false;



private void FixedUpdate()
{
    if(houseEnergy < 100 && !isRegen)
    {
        StartCoroutine(Energyregen());
    }
}

private IEnumerator Energyregen()
{    
    isRegen = true;
    yield return new WaitForSeconds(regenTimeDelay);   
    float value = 100 * regenPercentAmount;
    houseEnergy += value;
    isRegen = false;    
}

}












