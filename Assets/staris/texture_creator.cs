using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
class portal_cam_data
{
    public Material material_output;
    public Transform portal_1;
    public Transform portal_2;
}

public class texture_creator : MonoBehaviour
{
    [SerializeField] portal_cam_data[] portal_cams;
    [SerializeField] GameObject camera_prefab;
    Transform player_camera;


    // Start is called before the first frame update
    void Start()
    {
        player_camera = GameObject.FindWithTag("Player").transform;
        
        for (int i = 0; i < portal_cams.Length; i++)
        {
            // make render texture and camera prefab
            RenderTexture render_tex = new RenderTexture(Screen.width, Screen.height, 24);
            GameObject camera_game_obj = Instantiate(camera_prefab, Vector3.zero, Quaternion.identity);
            
            // attach render texture to created camera prefab and to output material
            camera_game_obj.GetComponent<Camera>().targetTexture = render_tex;
            portal_cams[i].material_output.mainTexture = camera_game_obj.GetComponent<Camera>().targetTexture;

            // setup portal camera behaviour properties
            portal_cam portal_cam_script = camera_game_obj.GetComponent<portal_cam>();
            portal_cam_script.player_camera = player_camera;
            portal_cam_script.portal_1 = portal_cams[i].portal_1;
            portal_cam_script.portal_2 = portal_cams[i].portal_2;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
