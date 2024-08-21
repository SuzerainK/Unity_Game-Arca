using UnityEngine;

public class CameraScaleChanger : MonoBehaviour
{
    private Camera camera_GameObject;

    bool StartChange = false;
    int targetScale;

    void Start()
    {
        if (PlayerPrefs.GetInt("InquisitorBoss") > 0)
        {
            CameraFieldOfView(40);
        }
        camera_GameObject = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (StartChange == true)
        {
            
            if(camera_GameObject.fieldOfView < targetScale)
            {
                
                camera_GameObject.fieldOfView += 1;
            }
            if(camera_GameObject.fieldOfView > targetScale)
            {
                camera_GameObject.fieldOfView -= 1;
            }
            else if (camera_GameObject.fieldOfView == targetScale)
            {
                
                StartChange = false;
            }
        }
    }

    public void CameraFieldOfView(int scale)
    {
        StartChange = true;
        targetScale = scale;
    }

}
