using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerScoreUI : MonoBehaviour
{
    public TextMeshProUGUI pseudoDuJoueur;
    public TextMeshProUGUI scoreDuJoueur;
    public Animator scoreAnimator;
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

    public void PlayScoreAnimation(bool winScore)
    {
        if (winScore) scoreAnimator.Play("gainingScore", -1,0f);
        else scoreAnimator.Play("losingScore", -1, 0f);
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
