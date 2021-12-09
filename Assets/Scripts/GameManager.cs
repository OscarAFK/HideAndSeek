using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class GameManager : MonoBehaviour
{
    static GameManager instance = null;
    
    public Piece prefabPiece;
    public int nbPieceTotal;
    int nbPiece;


    List<SpawnPoint> spawnPoints;
    List<SpawnPoint> emptySpawnPoints;

    public static GameManager Instance
    {
        get
        {
            return GameManager.instance;
        }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (NetworkManager.Singleton.IsServer)
        {
            int nbIteration = 0;
            while (nbPieceTotal > nbPiece && emptySpawnPoints.Count >= 1 && nbIteration < 2 * nbPieceTotal)
            {
                nbIteration++;
                int index = Random.Range(0, emptySpawnPoints.Count);
                if (emptySpawnPoints[index].SpawnPiece())
                {
                    emptySpawnPoints.RemoveAt(index);
                    nbPiece++;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (NetworkManager.Singleton.IsServer)
        {
            
        }
    }

    public Vector3 GetEmptySpawnPoint()
    {
        int nbIter = 0;
        while (nbIter < emptySpawnPoints.Count*2)
        {
            int randomIndex = UnityEngine.Random.Range(0, emptySpawnPoints.Count);
            if (!emptySpawnPoints[randomIndex].PlayerIsNear())
            {
                var returnPos = emptySpawnPoints[randomIndex].transform.position;
                emptySpawnPoints.RemoveAt(randomIndex);
                return returnPos;
            }
            nbIter++;
        }
        return new Vector3(-10,-100,-10);       //Il faudra mieux gérer ce cas là
    }

    public void EmptySpawnPoint(SpawnPoint spawnP)
    {
        emptySpawnPoints.Add(spawnP);
        nbPiece--;
    }

    public void Initialize()
    {
        GameObject[] tmp = GameObject.FindGameObjectsWithTag("spawnPiece");
        spawnPoints = new List<SpawnPoint>();
        foreach (GameObject sp in tmp)
        {
            spawnPoints.Add(sp.GetComponent<SpawnPoint>());
        }
        emptySpawnPoints = spawnPoints;
        nbPiece = 0;
        int nbIteration = 0;
        while (nbPieceTotal > nbPiece && emptySpawnPoints.Count >= 1 && nbIteration < 2 * nbPieceTotal)
        {
            nbIteration++;
            int index = Random.Range(0, emptySpawnPoints.Count);
            if (emptySpawnPoints[index].SpawnPiece())
            {
                emptySpawnPoints.RemoveAt(index);
                nbPiece++;
            }
        }
    }
}
