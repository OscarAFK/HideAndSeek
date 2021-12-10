using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class piece_number : MonoBehaviour
{
    [SerializeField] private Text text_piece1;
    [SerializeField] private Text text_piece2;
    [SerializeField] private Text text_piece3;

    private int current_piece1 = 0;
    private int current_piece2 = 0;
    private int current_piece3 = 0;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("a"))
        {
            current_piece1 += 1;
            text_piece1.text = current_piece1.ToString();
        }
        if (Input.GetKeyDown("q"))
        {
            if (current_piece1 < 5 && current_piece1 > 0)
            {
                current_piece1 = 0;
                text_piece1.text = current_piece1.ToString();
            }
            else
            {
                if (current_piece1 > 0)
                {
                    current_piece1 -= 5;
                    text_piece1.text = current_piece1.ToString();
                }
            }
            if (current_piece2 > 0)
            {
                current_piece2 -= 1;
                text_piece2.text = current_piece2.ToString();
            }
            if (current_piece3 > 0)
            {
                current_piece3 -= 1;
                text_piece3.text = current_piece3.ToString();
            }
        }

        if (Input.GetKeyDown("z"))
        {
            current_piece2 += 1;
            text_piece2.text = current_piece2.ToString();
        }
        if (Input.GetKeyDown("s"))
        {
            if (current_piece2 < 5 && current_piece2 > 0)
            {
                current_piece2 = 0;
                text_piece2.text = current_piece2.ToString();
            }
            else
            {
                if (current_piece2 > 0)
                {
                    current_piece2 -= 5;
                    text_piece2.text = current_piece2.ToString();
                }
                
            }
            if (current_piece1 > 0)
            {
                current_piece1 -= 1;
                text_piece1.text = current_piece1.ToString();
            }
            if (current_piece3 > 0)
            {
                current_piece3 -= 1;
                text_piece3.text = current_piece3.ToString();
            }
        }

        if (Input.GetKeyDown("e"))
        {
            current_piece3 += 1;
            text_piece3.text = current_piece3.ToString();
        }
        if (Input.GetKeyDown("d"))
        {
            if (current_piece3 < 5 && current_piece3 > 0)
            {
                current_piece3 = 0;
                text_piece3.text = current_piece3.ToString();
            }
            else
            {
                if (current_piece3 > 0)
                {
                    current_piece3 -= 5;
                    text_piece3.text = current_piece3.ToString();
                }
            }
            if (current_piece2 > 0)
            {
                current_piece2 -= 1;
                text_piece2.text = current_piece2.ToString();
            }
            if (current_piece1 > 0)
            {
                current_piece1 -= 1;
                text_piece1.text = current_piece1.ToString();
            }
        }
    }
}
