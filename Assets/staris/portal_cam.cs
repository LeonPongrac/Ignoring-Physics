using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portal_cam : MonoBehaviour
{
    public Transform player_camera;
    public Transform portal_1;
    public Transform portal_2;

    // Start is called before the first frame update
    void Start()
    {
        player_camera = GameObject.FindWithTag("MainCamera").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Normal_Update();
    }

    void Normal_Update()
    {
        // position
        Vector3 player_portal_1_distance = player_camera.position - portal_1.position;
        Vector3 new_transform = portal_2.position + player_portal_1_distance;
        //new_transform.y = -portal_2.position.y + player_portal_1_distance.y;
        transform.position = new_transform;

        // rotation
        float angular_portals_delta = Quaternion.Angle(portal_2.rotation, portal_1.rotation);
        Quaternion portal_21_rotation = Quaternion.AngleAxis(angular_portals_delta, Vector3.up);
        Vector3 new_forward = player_camera.forward;
        Vector3 new_self_rot = portal_21_rotation * new_forward;
        transform.rotation = Quaternion.LookRotation(new_self_rot, Vector3.up);
    }

    void Inverse_Update()
    {
        // position
        Vector3 player_portal_1_distance = -player_camera.position + portal_1.position;    
        //player_portal_1_distance.y = player_camera.position.y - portal_1.position.y;
        Vector3 new_transform = portal_2.position + player_portal_1_distance;
        new_transform.y = portal_2.position.y - player_portal_1_distance.y;
        transform.position = new_transform;
        
        // rotation
        float angular_portals_delta = Quaternion.Angle(portal_2.rotation, portal_1.rotation);
        Quaternion portal_21_rotation = Quaternion.AngleAxis(angular_portals_delta, Vector3.up);
        Vector3 new_forward = player_camera.forward;
        new_forward.x = -new_forward.x;
        new_forward.z = -new_forward.z;
        Vector3 new_self_rot = portal_21_rotation * new_forward;
        transform.rotation = Quaternion.LookRotation(new_self_rot, Vector3.up);
    }
}
