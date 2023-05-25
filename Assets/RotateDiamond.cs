using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateDiamond : MonoBehaviour
{
    private float angle = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        angle += 200 * Time.deltaTime;
        transform.rotation= Quaternion.Euler(transform.rotation.x,  angle, transform.rotation.y);
    }
}
