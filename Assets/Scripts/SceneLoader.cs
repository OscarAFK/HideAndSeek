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

    // Start is called before the first frame update
    void Start()
    {
        uNetTransport.ConnectAddress = ConnexionInfo.ipAdress;
        uNetTransport.ConnectPort = ConnexionInfo.port;

        if (ConnexionInfo.isHosting)
        {
            NetworkManager.Singleton.StartHost();
            var playerObject = NetworkManager.Singleton.SpawnManager.GetLocalPlayerObject();
            playerObject.GetComponent<NetworkObject>().ChangeOwnership(NetworkManager.Singleton.LocalClientId);
            GameManager.Instance.Initialize();
        }else
        {
            NetworkManager.Singleton.StartClient();
            var networkObjects = FindObjectsOfType<NetworkObject>();
            for (int i = 0; i < networkObjects.Length; i++)
            {
                if (networkObjects[i].IsLocalPlayer)
                {
                    var cinemachineFreeLook = FindObjectOfType<CinemachineFreeLook>();
                    cinemachineFreeLook.Follow = networkObjects[i].transform;
                    cinemachineFreeLook.LookAt = networkObjects[i].transform.Find("HeadTransf");
                }
            }
        }

    }

}
