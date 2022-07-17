using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pip1 : MonoBehaviour
{
    public GameObject player;

    public GameObject crane;

    public GameObject destination;

    public float liftHeight = 3;

    public float speed = 5;

    bool bruh = true;

    private void Start()
    {
        crane.transform.position = new Vector3(transform.position.x, liftHeight + 1, transform.position.z);
    }

    public void Crane()
    {
        if (player != null)
        {
            crane.transform.position = new Vector3(player.transform.position.x, liftHeight + 1, player.transform.position.z);
            if (Mathf.Abs(player.transform.position.y - liftHeight) > 0.01 && Vector3.Distance(new Vector3(player.transform.position.x, 0, player.transform.position.z), new Vector3(destination.transform.position.x, 0, destination.transform.position.z)) > 0.01 && bruh == true)
            {
                player.transform.position = new Vector3(player.transform.position.x, Mathf.Lerp(player.transform.position.y, liftHeight, speed * Time.deltaTime), player.transform.position.z);
            }
            else if (Mathf.Abs(player.transform.position.y - liftHeight) <= 0.01 && Vector3.Distance(new Vector3(player.transform.position.x, 0, player.transform.position.z), new Vector3(destination.transform.position.x, 0, destination.transform.position.z)) > 0.01 && bruh == true)
            {
                player.transform.position = Vector3.Lerp(new Vector3(player.transform.position.x, liftHeight, player.transform.position.z), new Vector3(destination.transform.position.x, liftHeight, destination.transform.position.z), speed * Time.deltaTime);
            }
            else
            {
                player.transform.position = Vector3.Lerp(new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z), new Vector3(destination.transform.position.x, destination.transform.position.y, destination.transform.position.z), speed * Time.deltaTime);
                
                if(Vector3.Distance(new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z), new Vector3(destination.transform.position.x, destination.transform.position.y, destination.transform.position.z)) <= 0.01)
                {
                    player.transform.position = destination.transform.position;
                    player.GetComponent<DiceController>().futurePos = player.transform.position;
                    player.GetComponent<DiceController>().inCrane = false;
                }
            }
        }
    }
}
