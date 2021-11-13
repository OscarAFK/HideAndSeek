using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{

    //Variable pour vérifier qu'il n'y ai pas de joueur proche. Il faudra changer ce système quand il y aura plusieurs joueurs
    bool playerIsNear;

    public bool SpawnPiece()
    {
        if (playerIsNear) return false;
        Vector3 pos = transform.position + Vector3.up;
        Piece piece = Instantiate(GameManager.Instance.prefabPiece);
        piece.transform.position = pos;
        piece.spawnPoint = this;
        return true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "proie")
        {
            playerIsNear = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "proie")
        {
            playerIsNear = false;
        }
    }
}
