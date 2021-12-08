using System.Collections;
using System.Collections.Generic;
using Unity.Netcode.Transports.UNET;
using UnityEngine;
using Unity.Netcode;
using Cinemachine;
using System;
using Unity.Netcode.Samples;

public class SceneLoader : NetworkBehaviour
{
    public UNetTransport uNetTransport;

    public GameObject preyPrefab;
    public GameObject hunterPrefab;

    private NetworkVariable<int> playersInGame = new NetworkVariable<int>();

    // Start is called before the first frame update
    void Start()
    {
        NetworkManager.Singleton.OnClientConnectedCallback += Singleton_OnClientConnectedCallback;
        NetworkManager.Singleton.OnClientDisconnectCallback += Singleton_OnClientDisconnectCallback;


        uNetTransport.ConnectAddress = ConnexionInfo.ipAdress;
        uNetTransport.ConnectPort = ConnexionInfo.port;
        if (ConnexionInfo.ipAdress==null)
        {
            Debug.Log("Invalid address, default address and port used");
            uNetTransport.ConnectAddress = "127.0.0.1";
            uNetTransport.ConnectPort = 7777;
            NetworkManager.Singleton.StartHost();
            return;
        }

        if (ConnexionInfo.isHosting)
        {
            NetworkManager.Singleton.StartHost();
            playersInGame.Value = 1;
            GameManager.Instance.Initialize();

        }
        else
        {
            NetworkManager.Singleton.StartClient();
            
        }
    }

    private void Singleton_OnClientDisconnectCallback(ulong obj)
    {
        if (IsServer)
        {
            Debug.Log(obj + " just disconnected");
            var players = FindObjectsOfType<Player>();
            foreach (Player p in players)
            {
                if(p.netId.Value == (int)obj)
                {
                    //p.GetComponent<NetworkObject>().RemoveOwnership();
                    //p.GetComponent<NetworkObject>().Despawn();
                    break;
                }
            }
            playersInGame.Value--;
        }
    }

    private void Update()
    {
        Debug.Log(playersInGame.Value);
    }

    private void Singleton_OnClientConnectedCallback(ulong obj)
    {
        if (IsServer)
        {
            Debug.Log(obj + " just connected");

            Player p;
            if (playersInGame.Value==1) p = CreateHunter(obj);
            else p = CreatePrey(obj);

            playersInGame.Value++;
            p.netId.Value = (int)obj;
        }
    }

    Player CreatePrey(ulong id)
    {
        GameObject player = Instantiate(preyPrefab);
        player.transform.position = new Vector3(36, 0, -22);
        player.gameObject.GetComponent<NetworkObject>().SpawnAsPlayerObject(id);
        return player.GetComponent<Player>();
    }

    Player CreateHunter(ulong id)
    {
        GameObject player = Instantiate(hunterPrefab);
        player.transform.position = new Vector3(0, 1, 0);
        player.gameObject.GetComponent<NetworkObject>().SpawnAsPlayerObject(id);
        return player.GetComponent<Player>();
    }
}
