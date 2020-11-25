using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour //Makes the main menu gameobject in the menu scene, manages the main menu
{
    private GameObject optionsMenu;
    private GameObject storyMenu;

    private void Start()
    {
        optionsMenu = transform.parent.transform.GetChild(2).gameObject;
        storyMenu = transform.parent.transform.GetChild(3).gameObject;

        optionsMenu.SetActive(false);
        storyMenu.SetActive(false);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Debug.Log("Game Quit");
        Application.Quit();
    }
}
