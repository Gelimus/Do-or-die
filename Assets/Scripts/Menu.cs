using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject quitMenu;
    public GameObject pauseMenu;
    public GameObject quitMenuAfterPause;
    private GameObject mainMenu;
    public GameObject gameOver;
    public GameObject quitMenuAfterGameOver;
    public GameObject bridgeScreen;
    public GameObject bridgeScreenFadeOut;
    public int activeScene;
    private float delay = 0f;

    // Start is called before the first frame update
    void Start()
    {
        GameController.menu = this;
        foreach (Transform child in transform)
        {
            switch (child.name)
            {
                case "MainMenu":
                    mainMenu = child.gameObject;   //pobranie menu głównego
                    break;
                case "QuitMenu":
                    quitMenu = child.gameObject; //pobranie menu wyjścia z gry
                    break;
                case "PauseMenu":
                    pauseMenu = child.gameObject; //pobranie menu pauzy
                    break;
                case "QuitMenuAfterPause":
                    quitMenuAfterPause = child.gameObject; //pobranie menu wyjścia z gry po pauzie
                    break;
                case "GameOver":
                    gameOver = child.gameObject; //pobranie ekranu GameOver
                    break;
                case "QuitMenuAfterGameOver":
                    quitMenuAfterGameOver = child.gameObject; //pobranie menu wyjścia z gry po GameOver
                    break;
                case "Bridge":
                    bridgeScreen = child.gameObject; //pobranie ekranu przejścia między levelami
                    break;
                case "BridgeFadeOut":
                    bridgeScreenFadeOut = child.gameObject; //jak wyżej - element ten został zduplikowany jedynie dla innej animacji
                    break;
            }
        }
        quitMenu.SetActive(false); //wyłączenie menu wyjścia z gry
        pauseMenu.SetActive(false);
        quitMenuAfterPause.SetActive(false);
        gameOver.SetActive(false);
        quitMenuAfterGameOver.SetActive(false);
        bridgeScreen.SetActive(false);
        bridgeScreenFadeOut.SetActive(false);
        Time.timeScale = 1;
        if (mainMenu.activeSelf == true) Cursor.visible = true;
        //Cursor.lockState = CursorLockMode.Confined; //odblokowanie kursora myszy  TEMPORARILY COMMENTED (everywhere below also)
    }

    // Update is called once per frame
    void Update()
    {
        if (delay > 0)
        {
            delay -= Time.deltaTime;
            if (delay <= 0)
            {
                delay = 0;
                bridgeScreenFadeOut.SetActive(false);
                SceneManager.LoadScene(activeScene);
            }
        }
    }

    public void btnPlayPressed()
    {
        //activeScene = 1;
        SceneManager.LoadScene(1);
        mainMenu.SetActive(false);
        Time.timeScale = 1; //włączenie czasu
        Cursor.visible = false; //ukrycie kursora
        //Cursor.lockState = CursorLockMode.Locked; //zablokowanie kursora
    }

    public void btnExitPressed()
    {
        quitMenu.SetActive(true); //aktywacja menu wyjścia
        mainMenu.SetActive(false); //dezaktywacja menu głównego
    }

    public void btnYesExitPressed()
    {
        Application.Quit(); //wyjście z gry
    }

    public void btnCancelExitPressed()
    {
        mainMenu.SetActive(true); //aktywacja menu głównego
        quitMenu.SetActive(false); //dezaktywacja menu wyjścia
    }

    public void showPauseMenu()
    {
        pauseMenu.SetActive(true); //aktywacja menu pauzy
        Time.timeScale = 0; //zatrzymanie czasu
        Cursor.visible = true; //odkrycie kursora
        //Cursor.lockState = CursorLockMode.Confined; //odblokowanie kursora
    }

    public void btnResumeInPauseMenu()
    {
        pauseMenu.SetActive(false);
        Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
    }

    public void btnRestartInPauseMenu()
    {
        pauseMenu.SetActive(false);
        Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;
        Scene scene = SceneManager.GetActiveScene();
        activeScene = scene.buildIndex;
        SceneManager.LoadScene(activeScene);
        Time.timeScale = 1;
    }

    public void btnExitInPauseMenu()
    {
        quitMenuAfterPause.SetActive(true); //aktywacja menu wyjścia po pauzie
        pauseMenu.SetActive(false); //dezaktywacja menu pauzy
    }

    public void btnYesInQuitMenuAfterPause()
    {
        Application.Quit();
    }

    public void btnCancelInQuitMenuAfterPause()
    {
        pauseMenu.SetActive(true); //aktywacja menu pauzy
        quitMenuAfterPause.SetActive(false); //dezaktywacja menu wyjścia po pauzie
    }

    public void showGameOverScreen()
    {
        gameOver.SetActive(true);
        Cursor.visible = true;
        //Cursor.lockState = CursorLockMode.Confined;
    }

    public void btnQuitInGameOver()
    {
        quitMenuAfterGameOver.SetActive(true);
        gameOver.SetActive(false);
    }

    public void btnCancelInQuitMenuAfterGameOver()
    {
        quitMenuAfterGameOver.SetActive(false);
        gameOver.SetActive(true);
    }

    public void showBridgeScreen()
    {
        bridgeScreen.SetActive(true);
        Cursor.visible = true;
        //Cursor.lockState = CursorLockMode.Confined;
    }

    public void btnNextLevel()
    {
        Scene scene = SceneManager.GetActiveScene();
        activeScene = scene.buildIndex;
        activeScene++;
        bridgeScreen.SetActive(false);
        bridgeScreenFadeOut.SetActive(true);
        delay = 2f; //wprowadzamy 2-sekundowe opóźnienie przed przejściem do następnego poziomu - patrz: void Update()
    }
}