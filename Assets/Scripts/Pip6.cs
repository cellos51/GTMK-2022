using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pip6 : MonoBehaviour
{
    public GameObject gate;

    public AudioSource sound;
    public int ripheadphoneusersconunter = 0;

    public bool gateDown = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gateDown == true && gate != null)
        {
            if (Vector3.Distance(gate.transform.position, new Vector3(gate.transform.position.x, -0.5f, gate.transform.position.z)) > 0.01)
            {
                gate.transform.position = Vector3.Lerp(gate.transform.position, new Vector3(gate.transform.position.x, -0.5f, gate.transform.position.z), 5 * Time.deltaTime);
            }
            else
            {
                Destroy(gate);
            }
        }
    }

    public void SteppedOn()
    {
        gateDown = true;

        if (ripheadphoneusersconunter == 0){
            sound.Play();
            ripheadphoneusersconunter++;
        }
    }    
}
