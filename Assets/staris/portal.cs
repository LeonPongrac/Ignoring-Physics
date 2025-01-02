using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class portal : MonoBehaviour
{
    [SerializeField] Transform portal_to_port;

    bool active = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {            
        if (Input.GetKeyDown(KeyCode.R))
            active = !active;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && active)
        {
            Transform player = other.gameObject.transform;
            if (Vector3.Dot(transform.up, player.position - transform.position) > 0)
            {
                // position
                Vector3 delta_this_portal = player.position - transform.position;
                //delta_this_portal.x = -delta_this_portal.x;
                //delta_this_portal.z = -delta_this_portal.z;
                Vector3 new_player_pos = portal_to_port.position + delta_this_portal;
                Debug.Log(delta_this_portal);
                Debug.Log(new_player_pos);
                other.gameObject.transform.position = new_player_pos;
                

                // rotation
                //player.Rotate(Vector3.up, 180);
            } 
        }
       
    }
}
