using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuPrincipal : MonoBehaviour
{
    public Canvas jouerQuitter;
    public Canvas hebergerRejoindre;
    public TextMeshProUGUI addrEtPort;
    public TextMeshProUGUI pseudoField;
    public Image adressInputField;

    private void Start()
    {
        jouerQuitter.gameObject.SetActive(true);
        hebergerRejoindre.gameObject.SetActive(false);
    }

    public void Quitter()
    {
        Application.Quit();
    }

    public void Jouer()
    {
        jouerQuitter.gameObject.SetActive(false);
        hebergerRejoindre.gameObject.SetActive(true);
    }
    
    public void Retour()
    {
        jouerQuitter.gameObject.SetActive(true);
        hebergerRejoindre.gameObject.SetActive(false);
    }

    bool RecupereConnexionInfo()
    {
        ConnexionInfo.pseudo = pseudoField.text;
        string[] s = addrEtPort.text.Split(':');
        if (s.Length != 2) return false;
        ConnexionInfo.ipAdress = s[0];
        string tmp = new string(s[1].Where(p => char.IsDigit(p)).ToArray());    //On ne garde que de nombre pour le port.
        if (int.TryParse(tmp, out int result))
        {
            ConnexionInfo.port = result;
            return true;
        }
        return false;
    }

    public void Heberger()
    {
        if (RecupereConnexionInfo())
        {
            ConnexionInfo.isHosting = true;
            SceneManager.LoadScene("layout with fourniture");
        }else
        {
            adressInputField.color = Color.red;
        }
    }

    public void Rejoindre()
    {
        if (RecupereConnexionInfo())
        {
            ConnexionInfo.isHosting = false;
            SceneManager.LoadScene("layout with fourniture");
        }
        else
        {
            adressInputField.color = Color.red;
        }
    }

}
