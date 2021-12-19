using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuVictoire : MonoBehaviour
{
    public bool isActive = false;
    public GameObject interfaceJoueur;
    public TextMeshProUGUI nomJoueurGagant;

    public void EndLevel()
    {
        isActive = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        gameObject.SetActive(true);
        interfaceJoueur.SetActive(false);
        var netObj = FindObjectsOfType<Player>();
        string nomDuGagnant = "";
        int plusHautScore = -1;
        foreach (var obj in netObj)
        {
            if (obj.GetComponent<ThirdPersonMovement>())
            {
                if(obj.netScore.Value > 20)
                {
                    if(obj.netScore.Value > plusHautScore)
                    {
                        nomDuGagnant = obj.netPseudo.Value;
                        plusHautScore = obj.netScore.Value;
                    }else if(obj.netScore.Value == plusHautScore)
                    {
                        nomDuGagnant += " " + obj.netPseudo.Value;
                    }
                }
                if (isActive) obj.GetComponent<ThirdPersonMovement>().controls.Disable();
            }
            else if (obj.GetComponent<PlayerFirstPersonMovement>())
            {
                if (isActive) obj.GetComponent<PlayerFirstPersonMovement>().controls.Disable();
            }
        }
        if (nomDuGagnant.Length == 0)
        {
            var netHunter = FindObjectOfType<PlayerFirstPersonMovement>();
            if(netHunter) nomDuGagnant = netHunter.GetComponent<Player>().netPseudo.Value;
        }
        nomJoueurGagant.text = nomDuGagnant;
    }

    public void MenuPrincipal()
    {
        NetworkManager.Singleton.Shutdown();
        Destroy(NetworkManager.Singleton.gameObject);
        SceneManager.LoadScene("Scenes/menuPrincipal");
    }
}
