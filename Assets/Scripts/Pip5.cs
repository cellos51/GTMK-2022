using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pip5 : MonoBehaviour
{

    public AudioSource sound;
    public int ripheadphoneusersconunter = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ripheadphoneusersconunter == 0){
            sound.Play();
            ripheadphoneusersconunter++;
        }
    }
}
