using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIGeneral : MonoBehaviour
{
    public Canvas pauseCanvas;
    public void ActivePauseCanvas()
    {
        pauseCanvas.gameObject.SetActive(!pauseCanvas.gameObject.activeSelf);
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("ValkoraScene");
    }

    public void Leave()
    {
        Application.Quit();
    }
}
