using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.PostProcessing;

public class SceneControl : MonoBehaviour {

    public Material orange_skybox;
    public PostProcessingProfile ppProfile;


    // Update is called once per frame
    void Update () {
        if (Input.GetKey(KeyCode.T))
        //if(Input.GetKeyDown("TestLoadNextScene"))
        {
            SceneManager.LoadScene(sceneName: "scene_2");
        }

        if (Input.GetKey(KeyCode.S))
        {
            if (null != orange_skybox)
            {
                RenderSettings.skybox = orange_skybox;
            }
        }

        if (Input.GetKey(KeyCode.G))
        {
            ppProfile.grain.enabled = false;
        }
    }
}
