using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    float currentMovementTime = 0f;
    public float transitionSpeed = 1;

    public GameObject screenTranistion;

    public GameObject player;

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
        if (!player.GetComponent<DiceController>().gameOver)
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
                SceneManager.LoadScene(scene.name);
            }
        }
    }
}
