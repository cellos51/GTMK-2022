using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    public string nextLevel;
    public GameObject UI;

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -Vector3.up * 10, out hit) && hit.transform.tag == "Player")
        {
            UI.GetComponent<GameOver>().loadScene(nextLevel);
        }
    }
}
