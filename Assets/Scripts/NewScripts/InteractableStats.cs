using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableStats : MonoBehaviour
{
    [SerializeField] public string phobiaName = "";
    [SerializeField] public string actionName = "";

    [SerializeField] public string unsuccessfulDesc = "";
    [SerializeField] public string successfulDesc = "";
    [SerializeField] public string cryticallDesc = "";
    [SerializeField] public string unsuccessfulNightDesc = "";
    [SerializeField] public string unvisibleDesc = "";
    
    [SerializeField] public float healthDamage = 10f;
    [SerializeField] public int energyCost = 10;
    [SerializeField] public bool isUsed = false;

}
