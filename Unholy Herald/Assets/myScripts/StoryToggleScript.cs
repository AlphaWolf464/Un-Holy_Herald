using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryToggleScript : MonoBehaviour
{
    public GameObject StoryLong;
    public GameObject StoryShort;

    private void Start()
    {
        StoryLong.SetActive(true);
        StoryShort.SetActive(false);
    }

    public void ToggleStoryWindows()
    {
        if(StoryLong.activeSelf == true)
        {
            StoryLong.SetActive(false);
            StoryShort.SetActive(true);
        }
        else
        {
            StoryLong.SetActive(true);
            StoryShort.SetActive(false);
        }
    }
}
