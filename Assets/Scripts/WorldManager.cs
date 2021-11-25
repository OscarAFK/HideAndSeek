using Unity.Netcode;
using UnityEngine;
using Cinemachine;

public class WorldManager : MonoBehaviour
{
    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 10, 300, 300));
        if (!NetworkManager.Singleton.IsClient && !NetworkManager.Singleton.IsServer)
        {
            StartButtons();
        }
        else
        {
            StatusLabels();

            SubmitNewPosition();
        }

        GUILayout.EndArea();
    }

    static void StartButtons()
    {
        if (GUILayout.Button("Host"))
        {
            NetworkManager.Singleton.StartHost();
            var playerObject = NetworkManager.Singleton.SpawnManager.GetLocalPlayerObject();
            playerObject.GetComponent<NetworkObject>().ChangeOwnership(NetworkManager.Singleton.LocalClientId);
            GameManager.Instance.Initialize();


        }
        if (GUILayout.Button("Client"))
        {
            NetworkManager.Singleton.StartClient();
            var networkObjects = FindObjectsOfType<NetworkObject>();
            for(int i = 0; i < networkObjects.Length; i++)
            {
                if (networkObjects[i].IsLocalPlayer)
                {
                    var cinemachineFreeLook = FindObjectOfType<CinemachineFreeLook>();
                    cinemachineFreeLook.Follow = networkObjects[i].transform;
                    cinemachineFreeLook.LookAt = networkObjects[i].transform.Find("HeadTransf");
                }
            }
        }
        if (GUILayout.Button("Server")) NetworkManager.Singleton.StartServer();
    }

    static void StatusLabels()
    {
        var mode = NetworkManager.Singleton.IsHost ?
            "Host" : NetworkManager.Singleton.IsServer ? "Server" : "Client";

        GUILayout.Label("Transport: " +
            NetworkManager.Singleton.NetworkConfig.NetworkTransport.GetType().Name);
        GUILayout.Label("Mode: " + mode);
    }

    static void SubmitNewPosition()
    {
        if (GUILayout.Button(NetworkManager.Singleton.IsServer ? "Move" : "Request Position Change"))
        {
            Debug.Log("Cette fonction est à virer");
        }
    }
}