using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        GameObject [] tmp = GameObject.FindGameObjectsWithTag("spawnPiece");
        spawnPoints = new List<SpawnPoint>();
        foreach (GameObject sp in tmp)
        {
            spawnPoints.Add(sp.GetComponent<SpawnPoint>());
        }
        emptySpawnPoints = spawnPoints;
        nbPiece = 0;
        int nbIteration = 0;
        while (nbPieceTotal > nbPiece && emptySpawnPoints.Count>=1 && nbIteration < 2 * nbPieceTotal)
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

    // Update is called once per frame
    void Update()
    {
        int nbIteration = 0;
        while (nbPieceTotal > nbPiece && emptySpawnPoints.Count >= 1 && nbIteration<2*nbPieceTotal)
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

    public void EmptySpawnPoint(SpawnPoint spawnP)
    {
        emptySpawnPoints.Add(spawnP);
        nbPiece--;
    }
}
