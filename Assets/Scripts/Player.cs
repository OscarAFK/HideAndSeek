
using Unity.Netcode;
using Unity.Netcode.Samples;
using UnityEngine;

public class Player : NetworkBehaviour
{
    public NetworkVariable<int> netId = new NetworkVariable<int>();
    public NetworkVariable<int> netScore = new NetworkVariable<int>();
    public NetworkVariable<bool> netShouldChangeLocation = new NetworkVariable<bool>();

    public Animator animatorCanevas;

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
        if (IsOwner && netShouldChangeLocation.Value)
        {
            ChangeLocation();
        }
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
        ShouldMoveServerRPC(true);
    }

    public void ChangeLocation()
    {
        animatorCanevas.SetTrigger("Mort");
        var preySpawns = FindObjectsOfType<PreySpawn>();
        bool foundGoodSpawn = false;
        int nbIter = 0;
        int randomIndex = 0;
        while (!foundGoodSpawn && nbIter < preySpawns.Length)
        {
            randomIndex = UnityEngine.Random.Range(0, preySpawns.Length);
            if (!preySpawns[randomIndex].playerIsNear)
            {
                var clientTransform = GetComponent<ClientNetworkTransform>();
                //transform.position = preySpawns[randomIndex].transform.position;
                clientTransform.Teleport(preySpawns[randomIndex].transform.position, transform.rotation, transform.localScale);
                foundGoodSpawn = true;
            }
            nbIter++;
        }
        if (!foundGoodSpawn)
        {
            transform.position = preySpawns[randomIndex].transform.position;
        }
        ShouldMoveServerRPC(false);
    }

    [ServerRpc]
    public void ShouldMoveServerRPC(bool shouldMove)
    {
        netShouldChangeLocation.Value = shouldMove;
    }

}