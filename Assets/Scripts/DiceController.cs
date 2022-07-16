using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceController : MonoBehaviour
{
    public float speed = 120f;

    private Vector3 futurePos;

    GameObject futureTransform;

    public AudioSource sound;

    public bool gameOver = false;

    private float velocityY = 0;

    private float speedBoostLength = 0;

    // Start is called before the first frame update
    void Start()
    {
        futurePos = transform.position;

        futureTransform = new GameObject();
        futureTransform.transform.SetPositionAndRotation(transform.position, transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.up) * 10, Color.green); // 6 side
        Debug.DrawRay(transform.position, transform.TransformDirection(-Vector3.right) * 10, Color.green); // 5 side
        Debug.DrawRay(transform.position, transform.TransformDirection(-Vector3.up) * 10, Color.green); // 4 side
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 10, Color.green); // 3 side
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.right) * 10, Color.green); // 2 side
        Debug.DrawRay(transform.position, transform.TransformDirection(-Vector3.forward) * 10, Color.green); // 1 side

        RaycastHit hit;

        if (Vector3.Distance(transform.localPosition, futurePos) == 0 && gameOver == false)
        {
            futurePos = transform.position;
            if (Input.GetKey(KeyCode.UpArrow) && !Physics.Raycast(transform.position, Vector3.forward, out hit, 1))
            {
                futurePos += new Vector3(0, 0, 1f);
                futureTransform.transform.RotateAround(GetComponent<Renderer>().bounds.center, Vector3.right, 90f);
            }
            else if (Input.GetKey(KeyCode.DownArrow) && !Physics.Raycast(transform.position, -Vector3.forward, out hit, 1))
            {
                futurePos += new Vector3(0, 0, -1f);
                futureTransform.transform.RotateAround(GetComponent<Renderer>().bounds.center, Vector3.right, -90f);
            }
            else if (Input.GetKey(KeyCode.LeftArrow) && !Physics.Raycast(transform.position, -Vector3.right, out hit, 1))
            {
                futurePos += new Vector3(-1f, 0, 0);
                futureTransform.transform.RotateAround(GetComponent<Renderer>().bounds.center, Vector3.forward, 90f);
            }
            else if (Input.GetKey(KeyCode.RightArrow) && !Physics.Raycast(transform.position, Vector3.right, out hit, 1))
            {
                futurePos += new Vector3(1f, 0, 0);
                futureTransform.transform.RotateAround(GetComponent<Renderer>().bounds.center, Vector3.forward, -90f);
            }
            StartCoroutine(moveObject());

            if (!Physics.Raycast(transform.position, -Vector3.up * 10))
            {
                gameOver = true;
            }
            else if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward) * 10, out hit) && hit.transform.tag == "3 Pip") // 3 side
            {
                hit.transform.gameObject.GetComponent<Pip3>().Test();
            }
            else if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up) * 10, out hit) && hit.transform.tag == "6 Pip") // 6 side
            {
                hit.transform.gameObject.GetComponent<Pip6>().SteppedOn();
            }
            else if (Physics.Raycast(transform.position, transform.TransformDirection(-Vector3.right) * 10, out hit) && hit.transform.tag == "5 Pip") // 5 side
            {
                speedBoostLength = 30;
            }
        }

        if (speedBoostLength > 0)
        {
            speedBoostLength -= Time.deltaTime;
            print(speedBoostLength);
            speed = 240;
        }
        else
        {
            speed = 120;
        }

        if (gameOver == true)
        {
            StopAllCoroutines();
            velocityY += -0.1f * Time.deltaTime;
            transform.position = new Vector3(transform.position.x, transform.position.y + velocityY, transform.position.z);
        }
    }

    public IEnumerator moveObject()
    {
        float currentMovementTime = 0f;//The amount of time that has passed
        bool playSound = false;
        float oldY = transform.position.y;
        while (Vector3.Distance(new Vector3(transform.localPosition.x, 0, transform.localPosition.z), new Vector3(futurePos.x, 0, futurePos.z)) > 0.01)
        {
            currentMovementTime += speed * Time.deltaTime;
            transform.localPosition = Vector3.Lerp(transform.position, futurePos, currentMovementTime * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, futureTransform.transform.rotation, currentMovementTime * Time.deltaTime);

            if (speed == 120)
            {
                transform.position = new Vector3(transform.position.x, oldY + Mathf.Sin(currentMovementTime / 10) / 6, transform.position.z);
            }
            else
            {
                transform.position = new Vector3(transform.position.x, oldY + Mathf.Sin(currentMovementTime / 15) / 6, transform.position.z);
            }
            

            playSound = true;
            yield return null;
        }
        transform.position = futurePos;

        if (playSound == true)
        {
            sound.Play();
        }
    }
}
