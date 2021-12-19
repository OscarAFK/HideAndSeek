using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPause : MonoBehaviour
{
    public bool isActive = false;
    public GameObject interfaceJoueur;

    public void SwitchMenu()
    {
        isActive = !isActive;
        Cursor.visible = isActive;
        if(isActive) Cursor.lockState = CursorLockMode.None;
        else Cursor.lockState = CursorLockMode.Locked;
        gameObject.SetActive(isActive);
        interfaceJoueur.SetActive(!isActive);
        var netObj = FindObjectsOfType<Player>();
        foreach (var obj in netObj)
        {
            if (!obj.IsOwner) continue;
            if (obj.GetComponent<ThirdPersonMovement>())
            {
                if (isActive) obj.GetComponent<ThirdPersonMovement>().controls.Disable();
                else obj.GetComponent<ThirdPersonMovement>().controls.Enable();
            }
            else if (obj.GetComponent<PlayerFirstPersonMovement>())
            {
                if (isActive) obj.GetComponent<PlayerFirstPersonMovement>().controls.Disable();
                else obj.GetComponent<PlayerFirstPersonMovement>().controls.Enable();
            }
        }
    }

    public void MenuPrincipal()
    {
        NetworkManager.Singleton.Shutdown();
        Destroy(NetworkManager.Singleton.gameObject);
        SceneManager.LoadScene("Scenes/menuPrincipal");
    }
}
