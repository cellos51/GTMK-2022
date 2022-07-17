using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public Button button;

    public GameObject MenuMusic;
    public GameObject levelMusic;

    // Update is called once per frame
    void Start()
    {
        button.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        Destroy(MenuMusic);
        Instantiate(levelMusic);
    }
}
