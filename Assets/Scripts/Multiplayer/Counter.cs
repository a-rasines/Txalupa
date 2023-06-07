using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Counter : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public static int score = 0;

    void Update()
    {
        scoreText.text = ("Score: ") + score.ToString();
    }
}
