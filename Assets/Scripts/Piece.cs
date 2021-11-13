using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    [HideInInspector]
    public SpawnPoint spawnPoint;

    void GetPiece()
    {
        GameManager.Instance.EmptySpawnPoint(spawnPoint);
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider  other)
    {
        if (other.gameObject.tag == "proie")
        {
            GetPiece();
        }
    }
}
