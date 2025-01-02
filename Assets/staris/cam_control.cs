using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cam_control : MonoBehaviour
{
    public float speed = 5.0f;
    public float rot_speed = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.A))
            transform.position += new Vector3(-speed, 0, 0) * Time.deltaTime;
        if (Input.GetKey(KeyCode.D))
            transform.position += new Vector3(speed, 0, 0) * Time.deltaTime;
        if (Input.GetKey(KeyCode.W))
            transform.position += new Vector3(0, 0, speed) * Time.deltaTime;
        if (Input.GetKey(KeyCode.S))
            transform.position += new Vector3(0, 0, -speed) * Time.deltaTime;

        if (Input.GetKey(KeyCode.Space))
            transform.position += new Vector3(0, speed, 0) * Time.deltaTime;
        if (Input.GetKey(KeyCode.LeftShift))
            transform.position += new Vector3(0, -speed, 0) * Time.deltaTime;

        if (Input.GetKey(KeyCode.Q))
            transform.Rotate(new Vector3(0, -rot_speed, 0) * Time.deltaTime);
        if (Input.GetKey(KeyCode.E))
            transform.Rotate(new Vector3(0, rot_speed, 0) * Time.deltaTime);

        if (Input.GetKey(KeyCode.R))
            transform.Rotate(new Vector3(-rot_speed, 0, 0) * Time.deltaTime);
        if (Input.GetKey(KeyCode.F))
            transform.Rotate(new Vector3(rot_speed, 0, 0) * Time.deltaTime);
    }
}
