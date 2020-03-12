using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    float quitTimer = 0;
    float timeToQuit = 2f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            if (quitTimer < timeToQuit)
            {
                quitTimer += Time.deltaTime;
            }
            else
            {
                Application.Quit();
            }

        }
    }
}
