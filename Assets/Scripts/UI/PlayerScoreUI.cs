using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerScoreUI : MonoBehaviour
{
    public TextMeshProUGUI pseudoDuJoueur;
    public TextMeshProUGUI scoreDuJoueur;
    public GameObject imagePrey;
    public GameObject imageHunter;

    public void SetPseudoJoueur(string pseudo)
    {
        pseudoDuJoueur.text = pseudo;
    }

    public void SetScoreJoueur(int score)
    {
        scoreDuJoueur.text = score.ToString();
    }

    public void SetToHunter()
    {
        imagePrey.SetActive(false);
        imageHunter.SetActive(true);
    }

    public void SetToPrey()
    {
        imagePrey.SetActive(true);
        imageHunter.SetActive(false);
    }
}
