using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private bool isAttached = false;
    public GameObject cameraHolder;
    public bool removeFromParent = true;

    // Update is called once per frame
    void Update()
    {
        if (!isAttached)
        {
            var player = GetComponentInParent<Player>();
            if (player.IsOwner)
            {
                cameraHolder.SetActive(true);
                isAttached = true;
                if(removeFromParent) cameraHolder.transform.parent = null;
            }
        }
    }
}
