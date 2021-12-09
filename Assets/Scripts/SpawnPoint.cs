using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class SpawnPoint : MonoBehaviour
{

    public bool SpawnPiece()
    {
        if (PlayerIsNear()) return false;
        Vector3 pos = transform.position + Vector3.up;
        Piece piece = Instantiate(GameManager.Instance.prefabPiece);
        piece.transform.position = pos;
        piece.spawnPoint = this;
        piece.gameObject.GetComponent<NetworkObject>().Spawn();
        return true;
    }

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
