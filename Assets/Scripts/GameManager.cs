using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class GameManager : NetworkBehaviour
{
    static GameManager instance = null;
    
    public Piece prefabPiece;
    public int nbPieceTotal;
    int nbPiece;
    List<SpawnPoint> spawnPoints;
    List<SpawnPoint> emptySpawnPoints;

    TimeLeftUI timeLeftUI;
    float timeLeft=-1;
    private NetworkVariable<float> netTimeLeft = new NetworkVariable<float>(-1);
    NetworkVariable<bool> netTimeOver = new NetworkVariable<bool>(false);

    public MenuPause menuPause;
    public MenuVictoire menuVictoire;

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

    // Update is called once per frame
    void Update()
    {
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;

            timeLeftUI.SetTimeLeft(timeLeft);
            if (IsServer) netTimeLeft.Value = timeLeft;
        }
        if(timeLeft <= 0)
        {
            if (IsServer && timeLeft!=-1) netTimeOver.Value = true;
            if (!netTimeOver.Value) timeLeft = netTimeLeft.Value;
            else
            {
                if(!menuVictoire.isActive) menuVictoire.EndLevel();
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(!menuVictoire.isActive) menuPause.SwitchMenu();
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
        if (IsServer)
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

            netTimeLeft.Value = 180;
        }
        timeLeft = netTimeLeft.Value;
        timeLeftUI = FindObjectOfType<TimeLeftUI>();
    }
}
