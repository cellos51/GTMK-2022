using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pip4 : MonoBehaviour
{
    public GameObject player;

    public Material translucent;

    public Material opaque;

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            if (player.GetComponent<DiceController>().blueprintLength > 0)
            {
                gameObject.GetComponent<Renderer>().material = opaque;
                gameObject.GetComponent<BoxCollider>().center = new Vector3(0, 0, 0);
            }
            else
            {
                gameObject.GetComponent<Renderer>().material = translucent;
                gameObject.GetComponent<BoxCollider>().center = new Vector3(0, 10, 0);
            }
        }
    }
}
