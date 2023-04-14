using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    string actualScene = SceneManager.GetActiveScene().name;
    public void StartBtn()
    {
        if (actualScene == "InitialScene")
        {
            //SceneManager.LoadScene(1);
            SceneManager.LoadScene("MainScene"); // ("SampleScene"); //ENARA Mejor por orden
        }else if (actualScene == "VRStart")
        {
            SceneManager.LoadScene("InitialScene");
        }
    }
    public void ExitBtn()
    {
        Application.Quit();
    }
}
