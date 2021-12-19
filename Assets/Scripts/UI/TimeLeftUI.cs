using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeLeftUI : MonoBehaviour
{
    public TextMeshProUGUI timeLeftText;

    public void SetTimeLeft(float timeLeft)
    {
        float minutes = Mathf.FloorToInt(timeLeft / 60);
        float seconds = Mathf.FloorToInt(timeLeft % 60);

        timeLeftText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
