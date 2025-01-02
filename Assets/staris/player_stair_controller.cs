using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class player_stair_controller : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float speed = 5.0f;
    [SerializeField] float rot_speed = 5.0f;    
    GameObject camera;
    private float x_rot = 0f;
    private float y_rot = 0f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        camera = GameObject.FindWithTag("MainCamera");
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {   
        Vector3 move_direction = Vector3.zero;

        // player movement
        if (Input.GetKey(KeyCode.W))
            move_direction += camera.transform.forward;
        if (Input.GetKey(KeyCode.S))
            move_direction -= camera.transform.forward;
        if (Input.GetKey(KeyCode.A))
            move_direction -= camera.transform.right;
        if (Input.GetKey(KeyCode.D))
            move_direction += camera.transform.right;
        move_direction.y = 0;
        Vector3 move_pos = transform.position + move_direction.normalized * speed * Time.deltaTime;
        rb.MovePosition(move_pos);

        // camera movement
        float mouse_x = Input.GetAxis("Mouse X") * rot_speed * Time.deltaTime;
        float mouse_y = Input.GetAxis("Mouse Y") * rot_speed * Time.deltaTime;
        x_rot -= mouse_y;
        y_rot += mouse_x;
        x_rot = Mathf.Clamp(x_rot, -90, 90);    
        camera.transform.localRotation = Quaternion.Euler(x_rot, y_rot, 0f);
    }
}
