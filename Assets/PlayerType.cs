using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerType : NetworkBehaviour
{


    public NetworkVariable<bool> networkIsPrey = new NetworkVariable<bool>();

    public GameObject prey;
    public GameObject hunter;

    public void Update()
    {
        if(networkIsPrey.Value == true)
        {
            prey.SetActive(true);
            hunter.SetActive(false);
            var cinemachineFreeLook = FindObjectOfType<CinemachineFreeLook>();
            cinemachineFreeLook.Follow = prey.transform;
            cinemachineFreeLook.LookAt = prey.transform.Find("HeadTransf");
        }else
        {
            prey.SetActive(false);
            hunter.SetActive(true);
            var cinemachineFreeLook = FindObjectOfType<CinemachineFreeLook>();
            cinemachineFreeLook.Follow = hunter.transform;
            cinemachineFreeLook.LookAt = hunter.transform;
        }
    }

    public void BecomePrey()
    {
        if (gameObject.GetComponent<NetworkObject>().IsOwner) UpdatePlayerTypeServerRPC(true);
    }

    public void BecomeHunter()
    {
        if (gameObject.GetComponent<NetworkObject>().IsOwner) UpdatePlayerTypeServerRPC(false);
    }

    [ServerRpc]
    public void UpdatePlayerTypeServerRPC(bool isPrey)
    {
        networkIsPrey.Value = isPrey;
    }
}
