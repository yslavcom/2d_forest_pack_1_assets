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
        if (Input.GetKeyDown(KeyCode.T))
        {
            SceneManager.LoadScene(sceneName: "scene_2");
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            if (null != orange_skybox)
            {
                RenderSettings.skybox = orange_skybox;
            }
        }
#if false
        if (Input.GetKeyDown(KeyCode.G))
        {
            ppProfile.grain.enabled = false;
        }
#endif
    }
}
