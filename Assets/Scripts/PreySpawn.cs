using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreySpawn : MonoBehaviour
{
    public bool playerIsNear = false;

    //FONCTIONS A RETRAVAILLER
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "proie" || other.gameObject.tag == "hunter")
        {
            playerIsNear = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "proie" || other.gameObject.tag == "hunter")
        {
            playerIsNear = false;
        }
    }

}
