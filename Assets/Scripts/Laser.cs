using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Laser : NetworkBehaviour
{

    private LineRenderer lr;
    [SerializeField] private Transform camera;

    public NetworkVariable<bool> netLaunchLaser = new NetworkVariable<bool>();
    public NetworkVariable<Vector3> netOriginLaser = new NetworkVariable<Vector3>();
    public NetworkVariable<Vector3> netLaserForward = new NetworkVariable<Vector3>();
    bool laserHasBeenDrawn = false;

    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        if (IsOwner) DrawLaserServerRpc(false);
        DisableLaser();
    }

    // Update is called once per frame
    void Update()
    {
        if (netLaunchLaser.Value && !laserHasBeenDrawn)
        {
            laserHasBeenDrawn = true;
            lr.enabled = true;
            Debug.Log("Shoooot ! ");

            //lr.SetPosition(0, transform.position);
            lr.SetPosition(0, netOriginLaser.Value);
            RaycastHit hit;
            if (Physics.Raycast(netOriginLaser.Value, netLaserForward.Value, out hit))
            {
                if (hit.collider)
                {
                    lr.SetPosition(1, hit.point);

                    if (IsServer)
                    {
                        if (hit.transform.gameObject.GetComponent<Player>())
                        {
                            hit.transform.gameObject.GetComponent<Player>().RemoveCoin();
                            hit.transform.gameObject.GetComponent<Player>().MoveToNewLocation();
                        }
                    }
                }
            }
            else
            {
                lr.SetPosition(1, netLaserForward.Value * 5000);
            }

            CancelInvoke();
            Invoke("DisableLaser", 0.5f);


        }
        else if(!netLaunchLaser.Value)
        {
            laserHasBeenDrawn = false;
        }
    }

    public void Shoot()
    {
        if (IsOwner && !netLaunchLaser.Value)
        {
            DrawLaserServerRpc(true);
            LaserValuesServerRpc(camera.transform.position, camera.forward);
        }
        
    }

    void DisableLaser()
    {
        if (IsOwner)
        {
            DrawLaserServerRpc(false);
        }
        lr.enabled = false;
    }

    [ServerRpc]
    public void DrawLaserServerRpc(bool drawLaser)
    {
        netLaunchLaser.Value = drawLaser;
    }

    [ServerRpc]
    public void LaserValuesServerRpc(Vector3 origin, Vector3 forward)
    {
        netOriginLaser.Value = origin;
        netLaserForward.Value = forward;
    }
}
