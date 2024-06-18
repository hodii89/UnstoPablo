using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PabloRot : MonoBehaviour
{
    public GameObject orientation;
    // Update is called once per frame
    void Update()
    {
        transform.rotation = orientation.transform.rotation;
    }
}
