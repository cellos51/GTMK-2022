using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingWall : MonoBehaviour
{
    public float speed;

    public GameObject destination;

    private Vector3 position1;
    private Vector3 position2;
    private Vector3 medium;

    private bool changeDirection = false;

    // Start is called before the first frame update
    void Start()
    {
        position1 = new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z + 0.5f);
        position2 = new Vector3(destination.transform.position.x + 0.5f, destination.transform.position.y, destination.transform.position.z + 0.5f);
        medium = position1;
    }

    // Update is called once per frame
    void Update()
    {

        if (Vector3.Distance(medium, position2) > 0 && changeDirection == false)
        {
            medium = Vector3.MoveTowards(medium, position2, speed * Time.deltaTime);
            //transform.position = new Vector3((int)medium.x, transform.position.y, (int)medium.z);
            transform.position = Vector3.Lerp(transform.position, new Vector3((int)medium.x, transform.position.y, (int)medium.z), speed * 10 * Time.deltaTime);
        }
        else if (Vector3.Distance(medium, position2) <= 0 && changeDirection == false)
        {
            changeDirection = true;
            medium = position2;
        }

        if (Vector3.Distance(medium, position1) > 0 && changeDirection == true)
        {
            medium = Vector3.MoveTowards(medium, position1, speed * Time.deltaTime);

            transform.position = Vector3.Lerp(transform.position, new Vector3((int)medium.x, transform.position.y, (int)medium.z), speed * 10 * Time.deltaTime);
            //transform.position = new Vector3((int)medium.x, transform.position.y, (int)medium.z);
        }
        else if (Vector3.Distance(medium, position1) <= 0 && changeDirection == true)
        {
            changeDirection = false;
            medium = position1;
        }
    }
}
