using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePoint : MonoBehaviour
{
    private float HorizontalInput;
    public int speedrt; 
    // Start is called before the first frame update
    void Start()
    {
         
    }

    // Update is called once per frame
    void Update()
    {
        HorizontalInput=Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up*HorizontalInput * Time.deltaTime * speedrt);
    }
}
