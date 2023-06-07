using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBehaivour : MonoBehaviour
{
    private int vida;
    void Start()
    {
        vida = 100;
    }

    // Update is called once per frame
    public void Daño(int cant)
    {
        vida -= cant;
    }
    void Update()
    {
        if (vida <= 0)
        {
            SceneManager.LoadScene(2);// ("MuerteScene"); //ENARA mejor por orden
        }
        if (vida < 100)
        {
            StartCoroutine(RegenerateLife());
        }
    }
    private IEnumerator RegenerateLife()
    {
        yield return new WaitForSeconds(90f);
        vida += 20;
    }
}
