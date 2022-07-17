using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceController : MonoBehaviour
{
    public float speed = 120f;

    public Vector3 futurePos;

    GameObject futureTransform;

    public GameObject brokenModel;

    public AudioSource sound;

    public bool gameOver = false;

    private bool fall = false;

    private float velocityY = 0;

    private float speedBoostLength = 0;

    public float blueprintLength = 0;

    private bool teleported = false;

    public bool inCrane = false;

    private bool rotating = false;

    private GameObject craneTile;

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
        
        if (inCrane == true && craneTile != null)
        {
            craneTile.GetComponent<Pip1>().Crane();
        }

        if (Vector3.Distance(transform.localPosition, futurePos) == 0 && gameOver == false && inCrane == false)
        {
            futurePos = transform.position;
            if (Input.GetKey(KeyCode.UpArrow) && !Physics.Raycast(transform.position, Vector3.forward, out hit, 1))
            {
                futurePos += new Vector3(0, 0, 1f);
                futureTransform.transform.RotateAround(GetComponent<Renderer>().bounds.center, Vector3.right, 90f);
                teleported = false;
            }
            else if (Input.GetKey(KeyCode.DownArrow) && !Physics.Raycast(transform.position, -Vector3.forward, out hit, 1))
            {
                futurePos += new Vector3(0, 0, -1f);
                futureTransform.transform.RotateAround(GetComponent<Renderer>().bounds.center, Vector3.right, -90f);
                teleported = false;
            }
            else if (Input.GetKey(KeyCode.LeftArrow) && !Physics.Raycast(transform.position, -Vector3.right, out hit, 1))
            {
                futurePos += new Vector3(-1f, 0, 0);
                futureTransform.transform.RotateAround(GetComponent<Renderer>().bounds.center, Vector3.forward, 90f);
                teleported = false;
            }
            else if (Input.GetKey(KeyCode.RightArrow) && !Physics.Raycast(transform.position, Vector3.right, out hit, 1))
            {
                futurePos += new Vector3(1f, 0, 0);
                futureTransform.transform.RotateAround(GetComponent<Renderer>().bounds.center, Vector3.forward, -90f);
                teleported = false;
            }
            StartCoroutine(moveObject());
            StartCoroutine(rotate());

            if (!Physics.Raycast(transform.position, -Vector3.up * 10))
            {
                gameOver = true;
                fall = true;
            }
            else if (Physics.Raycast(transform.position, -Vector3.up * 10, out hit) && hit.transform.tag == "Finish")
            {
                fall = true;
            }
            else if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward) * 10, out hit) && hit.transform.tag == "3 Pip") // 3 side
            {
                rotating = true;
                if (Input.GetKeyDown(KeyCode.Alpha6))
                {
                    futureTransform.transform.eulerAngles = new Vector3(0,0,0);
                }
                else if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    futureTransform.transform.eulerAngles = new Vector3(90, 0, 0);
                }
                else if (Input.GetKeyDown(KeyCode.Alpha4))
                {
                    futureTransform.transform.eulerAngles = new Vector3(180, 0, 0);
                }
                else if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    futureTransform.transform.eulerAngles = new Vector3(270, 0, 0);
                }
                else if (Input.GetKeyDown(KeyCode.Alpha5))
                {
                    futureTransform.transform.eulerAngles = new Vector3(0, 0, -90);
                }
                else if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    futureTransform.transform.eulerAngles = new Vector3(0, 0, 90);
                }
            }
            else if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up) * 10, out hit) && hit.transform.tag == "6 Pip") // 6 side
            {
                hit.transform.gameObject.GetComponent<Pip6>().SteppedOn();
            }
            else if (Physics.Raycast(transform.position, transform.TransformDirection(-Vector3.right) * 10, out hit) && hit.transform.tag == "5 Pip") // 5 side
            {
                speedBoostLength = 30;
            }
            else if (Physics.Raycast(transform.position, transform.TransformDirection(-Vector3.up) * 10, out hit) && hit.transform.tag == "4 Pip") // 4 side
            {
                blueprintLength = 30;
            }
            else if (Physics.Raycast(transform.position, transform.TransformDirection(-Vector3.forward) * 10, out hit) && hit.transform.tag == "1 Pip") // 1 side
            {
                inCrane = true;
                craneTile = hit.transform.gameObject;
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

        if (blueprintLength > 0)
        {
            blueprintLength -= Time.deltaTime;
        }
        else
        {
            blueprintLength = 0;
        }

        if (fall == true)
        {
            StopAllCoroutines();
            velocityY += -0.05f * Time.deltaTime;
            transform.position = new Vector3(transform.position.x, transform.position.y + velocityY, transform.position.z);
        }
        else if (gameOver == true && fall == false)
        {
            StopAllCoroutines();

            var newDie = Instantiate(brokenModel);
            newDie.transform.position = transform.position;
            newDie.transform.rotation = transform.rotation;

            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        gameOver = true;
    }

    public IEnumerator rotate()
    {
        while (Vector3.Distance(futureTransform.transform.eulerAngles, transform.eulerAngles) > 0.01 && rotating == true)
        {

            transform.rotation = Quaternion.Lerp(transform.rotation, futureTransform.transform.rotation, 0.3f * Time.deltaTime);
            yield return null;
        }
        rotating = false;
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

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right) * 10, out hit) && hit.transform.tag == "2 Pip" && teleported == false) // 2 side
        {
            teleported = true;
         
            transform.position = new Vector3(hit.transform.gameObject.GetComponent<Pip2>().destination.transform.position.x, transform.position.y, hit.transform.gameObject.GetComponent<Pip2>().destination.transform.position.z);
            futurePos = transform.position;
        }


        

        if (playSound == true)
        {
            sound.Play();
        }
    }
}
