using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceExtraction : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
       if(other.CompareTag("Forest"))
        {
            Debug.Log("ForestForestForestForest");
            
        }
    }
   

}