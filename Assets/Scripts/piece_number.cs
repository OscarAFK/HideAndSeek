using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class piece_number : MonoBehaviour
{
    [SerializeField] private Text text_piece;
    [SerializeField] private Text max_piece;

    private int current_piece = 0;
    private int current_max = 10;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("q"))
        {
            current_max += 1;
            max_piece.text = current_max.ToString();
        }
        if (Input.GetKeyDown("s") && current_max > 0)
        {
            current_max -= 1;
            if (current_max < current_piece)
            {
                current_piece -= 1;
                text_piece.text = current_piece.ToString();
            }
            max_piece.text = current_max.ToString();
        }
        if (Input.GetKeyDown("a")&& current_piece < int.Parse(max_piece.text))
        {
            current_piece += 1;
            text_piece.text = current_piece.ToString();
        }
        if (Input.GetKeyDown("z")&& current_piece>0)
        {
            current_piece -= 1;
            text_piece.text = current_piece.ToString();
        }
    }
}
