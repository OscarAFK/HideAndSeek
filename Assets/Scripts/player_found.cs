using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player_found : MonoBehaviour
{
    [SerializeField] private Image player1;
    [SerializeField] private Image player2;
    [SerializeField] private Image player3;
    [SerializeField] private Text found1;
    [SerializeField] private Text found2;
    [SerializeField] private Text found3;
    [SerializeField] private Image player1alt;
    [SerializeField] private Image player2alt;
    [SerializeField] private Image player3alt;
    [SerializeField] private Text found1alt;
    [SerializeField] private Text found2alt;
    [SerializeField] private Text found3alt;

    private bool p1f;
    private bool p2f;
    private bool p3f;

    // Start is called before the first frame update
    void Start()
    {
        player1alt.enabled = false;
        player2alt.enabled = false;
        player3alt.enabled = false;
        found1alt.enabled = false;
        found2alt.enabled = false;
        found3alt.enabled = false;
        p1f = true;
        p2f = true;
        p3f = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            if (p1f)
            {
                p1f = false;
                player1alt.enabled = true;
                found1alt.enabled = true;
                player1.enabled = false;
                found1.enabled = false;
            }
            else
            {
                p1f = true;
                player1alt.enabled = false;
                found1alt.enabled = false;
                player1.enabled = true;
                found1.enabled = true;
            }
        }
        if (Input.GetKeyDown("2"))
        {
            if (p2f)
            {
                p2f = false;
                player2alt.enabled = true;
                found2alt.enabled = true;
                player2.enabled = false;
                found2.enabled = false;
            }
            else
            {
                p2f = true;
                player2alt.enabled = false;
                found2alt.enabled = false;
                player2.enabled = true;
                found2.enabled = true;
            }
        }
        if (Input.GetKeyDown("3"))
        {
            if (p3f)
            {
                p3f = false;
                player3alt.enabled = true;
                found3alt.enabled = true;
                player3.enabled = false;
                found3.enabled = false;
            }
            else
            {
                p3f = true;
                player3alt.enabled = false;
                found3alt.enabled = false;
                player3.enabled = true;
                found3.enabled = true;
            }
        }
    }
}
