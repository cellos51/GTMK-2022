using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOver : MonoBehaviour
{
    float currentMovementTime = 0f;
    public float transitionSpeed = 1;

    public GameObject screenTranistion;

    public GameObject timer;

    public float time = 90;

    private bool ourGameover = false;

    private bool once = true;

    Scene scene;

    // Start is called before the first frame update
    void Start()
    {
        scene = SceneManager.GetActiveScene();
    }

    // Update is called once per frame
    void Update()
    {
        if (time > 0)
        {
            time -= Time.deltaTime;
        }

       

        if ( (Mathf.FloorToInt(Mathf.Log10(Mathf.FloorToInt(time % 60))) + 1) < 2)
        {
            timer.GetComponent<TextMeshProUGUI>().SetText("Time: " + Mathf.FloorToInt(time / 60) + ":" + "0" +Mathf.FloorToInt(time % 60));
        }
        else
        {
            timer.GetComponent<TextMeshProUGUI>().SetText("Time: " + Mathf.FloorToInt(time / 60) + ":" + Mathf.FloorToInt(time % 60));
        }

        if (GameObject.Find("Player") != null)
        {
            ourGameover = GameObject.Find("Player").GetComponent<DiceController>().gameOver;
        }

        if (!ourGameover)
        {
            if (Vector3.Distance(screenTranistion.GetComponent<RectTransform>().anchoredPosition, new Vector3(0, -Screen.height - (Screen.height / 2), 0)) > 0.01)
            {
                currentMovementTime += transitionSpeed * Time.deltaTime;
                screenTranistion.GetComponent<RectTransform>().anchoredPosition = Vector3.Lerp(screenTranistion.GetComponent<RectTransform>().anchoredPosition, new Vector3(0, -Screen.height - (Screen.height / 2), 0), currentMovementTime * Time.deltaTime);
            }
            else
            {
                screenTranistion.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, -Screen.height, 0);
            }
        }
        else
        {
            loadScene(scene.name);
        }

        if (time <= 0)
        {
            loadScene(scene.name);
        }
    }
    public void loadScene(string sceneName)
    {
        if (once == true)
        {
            screenTranistion.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, Screen.height * 2, 0);
            once = false;
            currentMovementTime = 0;
            screenTranistion.GetComponent<RectTransform>().transform.localScale = new Vector3(1, 2, 1);
        }

        if (Vector3.Distance(screenTranistion.GetComponent<RectTransform>().anchoredPosition, new Vector3(0, -(Screen.height / 2), 0)) > Screen.height / 2)
        {
            currentMovementTime += transitionSpeed * Time.deltaTime;
            screenTranistion.GetComponent<RectTransform>().anchoredPosition = Vector3.Lerp(screenTranistion.GetComponent<RectTransform>().anchoredPosition, new Vector3(0, -(Screen.height / 2), 0), (currentMovementTime * Time.deltaTime));
        }
        else
        {
            screenTranistion.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
            SceneManager.LoadScene(sceneName);
        }
    }
}
