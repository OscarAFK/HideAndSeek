
using Unity.Collections;
using Unity.Netcode;
using Unity.Netcode.Samples;
using UnityEngine;
using UnityEngine.UI;

public class Player : NetworkBehaviour
{
    public NetworkVariable<int> netId = new NetworkVariable<int>();
    public NetworkVariable<int> netScore = new NetworkVariable<int>();
    public NetworkVariable<bool> netShouldChangeLocation = new NetworkVariable<bool>();
    public NetworkVariable<NetworkString> netPseudo = new NetworkVariable<NetworkString>();

    public Animator animatorCanevas;

    ScoresUI scoresUI;

    public override void OnNetworkSpawn()
    {
        if (IsServer) netScore.Value = 0;
        if (IsOwner)
        {
            SetPseudoServerRPC(ConnexionInfo.pseudo);
        }
        scoresUI = FindObjectOfType<ScoresUI>();
        scoresUI.AddPlayerUI(this);
        scoresUI.UpdateRoleUI(this);
        LayoutRebuilder.ForceRebuildLayoutImmediate(scoresUI.GetComponent<RectTransform>());
    }

    public override void OnDestroy()
    {
        scoresUI.RemovePlayerUI(this);
        LayoutRebuilder.ForceRebuildLayoutImmediate(scoresUI.GetComponent<RectTransform>());
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
            if (!preySpawns[randomIndex].PlayerIsNear())
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

    [ServerRpc]
    public void SetPseudoServerRPC(string pseudo)
    {
        netPseudo.Value = pseudo;
    }

}

public struct NetworkString : INetworkSerializable
{
    private FixedString32Bytes info;

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref info);
    }

    public override string ToString()
    {
        return info.ToString();
    }

    public static implicit operator string(NetworkString s) => s.ToString();
    public static implicit operator NetworkString(string s) => new NetworkString() { info = new FixedString32Bytes(s) };
}