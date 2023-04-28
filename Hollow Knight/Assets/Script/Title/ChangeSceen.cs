using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceen : MonoBehaviour
{
    public void OnClickStart()
    {
        SceneManager.LoadScene("StageOne");
    }

    public void OnClickExit()
    {
        Application.Quit();
    }
}
