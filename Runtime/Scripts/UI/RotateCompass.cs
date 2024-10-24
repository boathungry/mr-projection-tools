using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCompass : MonoBehaviour
{
    public GameObject rotationOffset;
    public RectTransform compass;
    // Start is called before the first frame update
    void Start()
    {
        float sceneRotation = this.rotationOffset.transform.eulerAngles.y;
        float compassRotation = compass.rotation.z;
        print("sceneRotation: " + sceneRotation);
        print("compassRotation: " + compassRotation);
        if (sceneRotation != compassRotation)
        {
            print("Rotation mismatch.");
            compass.Rotate(0, 0, sceneRotation);
        }
        else
        {
            print("Rotations match.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
