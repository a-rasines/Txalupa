using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    string actualScene;
    public void Awake()
    {
        actualScene = SceneManager.GetActiveScene().name;
    }
    public void StartBtn()
    {
        SceneManager.LoadScene("MainScene");
        /*
        if (actualScene == "InitialScene")
        {
            //SceneManager.LoadScene(1);
            SceneManager.LoadScene("MainScene"); // ("SampleScene"); //ENARA Mejor por orden
        }else if (actualScene == "VRStart")
        {
            SceneManager.LoadScene("InitialScene");
        }
        */
    }

    public void MultiBtn()
    {
        SceneManager.LoadScene("VRStart"); // ("SampleScene"); //ENARA Mejor por orden
    }

    public void ExitBtn()
    {
        Application.Quit();
    }
}
