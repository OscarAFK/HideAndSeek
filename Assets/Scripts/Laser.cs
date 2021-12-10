using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.Netcode.Samples;
using UnityEngine;

public class Laser : NetworkBehaviour
{

    [SerializeField] private LineRenderer lr;
    [SerializeField] private Transform camera;

    public NetworkVariable<bool> netLaunchLaser = new NetworkVariable<bool>();
    public NetworkVariable<Vector3> netOriginLaser = new NetworkVariable<Vector3>();
    public NetworkVariable<Vector3> netLaserForward = new NetworkVariable<Vector3>();
    bool laserHasBeenDrawn = false;

    public LayerMask laserMask = 0;

    [SerializeField] private GameObject prefabCrackDecal;
    private GameObject decal;

    [SerializeField] GameObject prefabLaserImpact;
    [SerializeField] GameObject LaserImpact;
    [SerializeField] GameObject LaserLight;
    [SerializeField] ParticleSystem LaserGlow;
    [SerializeField] ParticleSystem prefabLaserGlowOnVision;
    [SerializeField] ParticleSystem LaserGlowOnVision;



    // Start is called before the first frame update
    void Start()
    {
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
            
            //lr.SetPosition(0, transform.position);
            lr.SetPosition(0, netOriginLaser.Value + camera.forward * 0.3f);
            RaycastHit hit;
            if (Physics.Raycast(netOriginLaser.Value, netLaserForward.Value, out hit, laserMask))
            {
                if (hit.collider)
                {
                    lr.SetPosition(1, hit.point);
                    if (!LaserImpact)
                    {
                        LaserImpact = Instantiate(prefabLaserImpact);
                        LaserLight = LaserImpact.transform.Find("LaserLight").gameObject;
                        LaserGlow = LaserImpact.transform.Find("Glow").GetComponent<ParticleSystem>();
                        LaserGlowOnVision = Instantiate(prefabLaserGlowOnVision);
                    }
                    LaserImpact.transform.position = hit.point + hit.normal * 0.1f;
                    LaserGlowOnVision.transform.position = netOriginLaser.Value + camera.forward * 0.3f;
                    LaserLight.SetActive(true);
                    LaserGlow.Play();
                    LaserGlowOnVision.Play();
                    if (hit.collider.gameObject.layer == LayerMask.NameToLayer("GroundAndWall"))
                    {
                        SpawnDecal(hit);
                    }

                    if (IsServer && hit.collider.gameObject.layer == LayerMask.NameToLayer("Prey"))
                    {
                        var hitGameObject = hit.collider.gameObject;
                        var player = hit.transform.gameObject.GetComponent<Player>();
                        if (player)
                        {
                            player.GetComponent<NetworkObject>().RemoveOwnership();
                            player.RemoveCoin();
                            player.MoveToNewLocation();
                            player.GetComponent<NetworkObject>().ChangeOwnership((ulong)player.netId.Value);
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
        if (LaserLight)
        {
            LaserLight.SetActive(false);
            LaserGlow.Stop();
            LaserGlowOnVision.Stop();
        }
        lr.enabled = false;
    }

    void SpawnDecal(RaycastHit hitInfo)
    {
        if(decal == null) decal = Instantiate(prefabCrackDecal);
        decal.transform.position = hitInfo.point;
        decal.transform.position += hitInfo.normal * 0.001f;
        decal.transform.forward = hitInfo.normal * - 1f;
        //decal.transform.Rotate(decal.transform.forward, UnityEngine.Random.Range(0,360));
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
