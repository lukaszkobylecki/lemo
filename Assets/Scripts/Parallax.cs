using UnityEngine;
using System.Collections;

public class Parallax : MonoBehaviour
{
    public Transform[] backgrounds;
    private float[] parallaxScales;
    public float smoothing;
    float parallaxX, backgroundTargetPosX;
    float parallaxY, backgroundTargetPosY;
    Vector3 backgroundTargetPos;
    private Transform cam;
    private Vector3 previousCamPos;

	void Start ()
    {
        cam = Camera.main.transform;
        previousCamPos = cam.position;
        parallaxScales = new float[backgrounds.Length]; 

        for(int i = 0; i < backgrounds.Length; i++)
        {
            parallaxScales[i] = backgrounds[i].position.z * -1;                  //We can use z value of background to determine the scale of parallax efect. The camera must be set to ortographic for this to make sense.
        }

	}
	
	void LateUpdate ()
    {
        for (int i = 0; i < backgrounds.Length; i++)
        {
            parallaxX = (previousCamPos.x - cam.position.x) * parallaxScales[i];
            parallaxY = (previousCamPos.y - cam.position.y) * parallaxScales[i];
            backgroundTargetPosX = backgrounds[i].position.x + parallaxX;
            backgroundTargetPosY = backgrounds[i].position.y + parallaxY;
                        backgroundTargetPos = new Vector3(backgroundTargetPosX, backgroundTargetPosY, backgrounds[i].position.z);
           // backgroundTargetPos = new Vector3(backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);

            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
        }

        previousCamPos = cam.position;
	}
}
