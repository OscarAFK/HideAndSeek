using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class Piece : MonoBehaviour
{
    [HideInInspector]
    public SpawnPoint spawnPoint;

    public ParticleSystem particle;

    private bool toReplace = false;

    public int score = 1;

    private void Update()
    {
        if (NetworkManager.Singleton.IsServer && toReplace)
        {
            Replace();
        }
    }

    void GetPiece()
    {
        particle.Emit(20);
        GameManager.Instance.EmptySpawnPoint(spawnPoint);
        Vector3 newPos = GameManager.Instance.GetEmptySpawnPoint();
        if (newPos.y < -10)
        {
            toReplace = true;
        }
        transform.position = newPos + new Vector3(0,0.5f,0);
        //gameObject.GetComponent<NetworkObject>().Despawn();
        //Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider  other)
    {
       if (other.gameObject.tag == "proie" && NetworkManager.Singleton.IsServer)
        {
            GetPiece();
            other.GetComponent<Player>().AddScore(score);
        }
    }

    void Replace()
    {
        Vector3 newPos = GameManager.Instance.GetEmptySpawnPoint();
        if (newPos.y < -10)
        {
            toReplace = true;
        }else
        {
            toReplace = false;
            transform.position = newPos + new Vector3(0, 0.5f, 0);
        }
    }
}
