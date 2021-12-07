using System.Collections;
using System.Collections.Generic;
using Unity.Netcode.Transports.UNET;
using UnityEngine;
using Unity.Netcode;
using Cinemachine;
using System;

public class SceneLoader : MonoBehaviour
{
    public UNetTransport uNetTransport;

    public NetworkObject localPlayer;

    // Start is called before the first frame update
    void Start()
    {
        uNetTransport.ConnectAddress = ConnexionInfo.ipAdress;
        uNetTransport.ConnectPort = ConnexionInfo.port;
        if (ConnexionInfo.ipAdress.Length == 0)
        {
            Debug.Log("Invalid address, default address and port used");
            uNetTransport.ConnectAddress = "127.0.0.1";
            uNetTransport.ConnectPort = 7777;
        }
        if (ConnexionInfo.isHosting)
        {
            NetworkManager.Singleton.StartHost();
            var playerObject = NetworkManager.Singleton.SpawnManager.GetLocalPlayerObject();
            playerObject.ChangeOwnership(NetworkManager.Singleton.LocalClientId);
            playerObject.gameObject.GetComponent<PlayerType>().BecomeHunter();
            GameManager.Instance.Initialize();

        }else
        {

            NetworkManager.Singleton.StartClient();
            var networkObjects = FindObjectsOfType<NetworkObject>();
            for (int i = 0; i < networkObjects.Length; i++)
            {
                if (networkObjects[i].IsLocalPlayer)
                {
                    networkObjects[i].GetComponent<PlayerType>().BecomePrey();
                    localPlayer = networkObjects[i];
                }
            }
        }

    }

}
