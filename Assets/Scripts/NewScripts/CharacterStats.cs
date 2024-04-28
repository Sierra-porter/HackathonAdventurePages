using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
     public List<Phobia> PhobiasPrefabs = new();
     public float characterHealth = 100;
     public float characterDurability = 1;

     void Awake()
     {
          PhobiasPrefabs[0].isKnown = true;
          PhobiasPrefabs[1].isKnown = false;
          PhobiasPrefabs[2].isKnown = false;
     }
}


