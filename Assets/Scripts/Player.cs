
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
        // TODO
        Debug.Log("Remove Coin");
    }

    public void MoveToNewLocation()
    {
        // TODO
        Debug.Log("Move to new location");
    }

}