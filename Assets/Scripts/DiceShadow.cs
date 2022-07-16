using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceShadow : MonoBehaviour
{
    public GameObject shadowProjector;

    // Update is called once per frame
    void Update()
    {
        shadowProjector.transform.position = transform.position;

        shadowProjector.transform.eulerAngles = new Vector3(90,0,0);
    }
}
