using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
  

    public void Lvl1()
    {
        SceneManager.LoadScene("Level 1");
    }
    public void Lvl2()
    {
        PlayerPrefs.SetString("HasBeenReset", "false");
        SceneManager.LoadScene("Level 2");
    }
    public void RestartLvl2()
    {
        PlayerPrefs.SetString("HasBeenReset", "true");
        SceneManager.LoadScene("Level 2");
    }
}
