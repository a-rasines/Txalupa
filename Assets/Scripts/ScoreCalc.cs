using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreCalc : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    private int score;
    private Vector3 startPosition;
    private Quaternion startRotation;
    // Start is called before the first frame update

    void Start()
    {
        startPosition = this.transform.position;
        startRotation = this.transform.rotation;
    }

    void Update()
    {
        scoreText.text = ("Score: ") + score.ToString();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer.Equals("Cube"))
        {
            score++;
            this.transform.position = startPosition;
            this.transform.rotation = startRotation;
        }
    }
}
