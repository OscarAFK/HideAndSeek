using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{

    private LineRenderer lr;
    [SerializeField] private Transform camera;



    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        DisableLaser();
    }

    // Update is called once per frame
    void Update()
    {
 
       
    }

    public void Shoot()
    {
        lr.enabled = true;
        Debug.Log("Shoooot ! ");
        lr.SetPosition(0, transform.position);
        RaycastHit hit;
        if (Physics.Raycast(camera.position, camera.forward, out hit))
        {
            if (hit.collider)
            {
                lr.SetPosition(1, hit.point);
                if (hit.transform.gameObject.GetComponent<Player>())
                {
                    hit.transform.gameObject.GetComponent<Player>().RemoveCoin();
                    hit.transform.gameObject.GetComponent<Player>().MoveToNewLocation();
                }
            }
        }
        else
        {
            lr.SetPosition(1, camera.forward * 5000);
        }

        CancelInvoke();
        Invoke("DisableLaser", 0.5f);
    }

    void DisableLaser()
    {
        lr.enabled = false;
    }
}
