using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIGeneral : MonoBehaviour
{
    public Canvas pauseCanvas;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
