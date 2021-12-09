
using Unity.Netcode;
using UnityEngine;

public class Player : NetworkBehaviour
{
    public NetworkVariable<int> netId = new NetworkVariable<int>();
    public NetworkVariable<int> netScore = new NetworkVariable<int>();

    public override void OnNetworkSpawn()
    {
        if (IsServer) netScore.Value = 0;
    }

    public void AddScore(int score)
    {
        if (IsServer) netScore.Value += score;
    }

    private void Update()
    {
        //Debug.Log("Score du joueur "+ gameObject.GetComponent<NetworkObject>().OwnerClientId + ": "+ netScore.Value);
    }

    public void RemoveCoin()
    {
        if (IsServer)
        {
            netScore.Value -= 5;
            if (netScore.Value < 0) netScore.Value = 0;
        }
    }

    public void MoveToNewLocation()
    {
        var preySpawns = FindObjectsOfType<PreySpawn>();
        bool foundGoodSpawn = false;
        int nbIter = 0;
        while (!foundGoodSpawn && nbIter < preySpawns.Length)
        {
            int randomIndex = UnityEngine.Random.Range(0, preySpawns.Length);
            if (!preySpawns[randomIndex].playerIsNear)
            {
                transform.position = preySpawns[randomIndex].transform.position;
                foundGoodSpawn = true;
            }
            nbIter++;
        }
    }

}