using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void SceneName(string Scenename)
    {
        SceneManager.LoadScene(Scenename);
    }
    public void asf()
    {
        Application.Quit();

    }
}
