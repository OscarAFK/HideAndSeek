using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class Piece : MonoBehaviour
{
    [HideInInspector]
    public SpawnPoint spawnPoint;

    public int score = 1;

    void GetPiece()
    {
        GameManager.Instance.EmptySpawnPoint(spawnPoint);
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider  other)
    {
       if (other.gameObject.tag == "proie" && NetworkManager.Singleton.IsServer)
        {
            GetPiece();
            other.GetComponent<Player>().AddScore(score);
        }
    }
}
