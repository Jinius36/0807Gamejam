using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SearchService;

public class Collider : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            
        }
        if (other.gameObject.tag == "Item")
        {

        }
        if (other.gameObject.tag == "Light")
        {

        }
        if(other.gameObject.tag == "Stairs")
        {
            DataManager.singleTon.saveData._currentStage++;
        }
    }
}
