using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreySpawn : MonoBehaviour
{
    public bool PlayerIsNear()
    {
        int layerPrey = LayerMask.GetMask("Prey");
        int layerHunter = LayerMask.GetMask("Hunter");
        int layerMask = layerPrey | layerHunter;
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 5, layerMask);
        if (hitColliders.Length > 0) return true;
        else return false;
    }
}
