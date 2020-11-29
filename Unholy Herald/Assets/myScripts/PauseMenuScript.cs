using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    private PlayerFaceMouseScript turnTo;
    private PlayerAbilityScript ability;

    public GameObject pauseMenu;
    private GameObject storyScreen;
    private bool isPaused;

    private void Start()
    {
        ability = GameObject.FindWithTag("Player").GetComponent<PlayerAbilityScript>();
        turnTo = ability.playerUI.turnTo;

        storyScreen = pauseMenu.transform.GetChild(2).gameObject;

        isPaused = false;
        pauseMenu.SetActive(false);
        storyScreen.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        turnTo.enabled = false;
        ability.enabled = false;
    }

    public void ResumeGame()
    {
        isPaused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        turnTo.enabled = true;
        ability.enabled = true;
    }

    public void RetrunToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
