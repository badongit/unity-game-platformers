using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMenuController : MonoBehaviour
{
    public void PlayGame()
    {
        
            SceneManager.LoadScene("Level2");
        
    }
    public void PlayLevel1()
    {
            SceneManager.LoadScene("Level1");
    }
    public void PlayLevel2()
    {
            SceneManager.LoadScene("Level2");
    }
    public void PlayLevel3()
    {
            SceneManager.LoadScene("Level3");
    }
    public void PlayLevel4()
    {
            SceneManager.LoadScene("Level4");
    }
    public void PlayLevel5()
    {
            SceneManager.LoadScene("Level5");
    }
    public void BackGame()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
