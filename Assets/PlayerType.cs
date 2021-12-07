using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerType : MonoBehaviour
{
    bool isPrey = true;
    public GameObject prey;
    public GameObject hunter;

    public ThirdPersonMovement scriptThirdPerson;
    public PlayerFirstPersonMovement scriptFirstPerson;
    public MouseLook scriptMouseLook;

    public Camera thirdPersonCamera;

    public void Awake()
    {
        thirdPersonCamera = Camera.main;
    }

    public void BecomePrey()
    {
        Debug.Log("New prey!");
        prey.SetActive(true);
        hunter.SetActive(false);
        scriptFirstPerson.enabled = false;
        scriptMouseLook.enabled = false;
        scriptThirdPerson.enabled = true;
    }

    public void BecomeHunter()
    {
        Debug.Log("New hunter!");
        prey.SetActive(false);
        hunter.SetActive(true);
        scriptFirstPerson.enabled = true;
        scriptMouseLook.enabled = true;
        scriptThirdPerson.enabled = false;
    }
}
