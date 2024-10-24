using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BirdsEyeInputManager : MonoBehaviour
{
    public Camera orthographicCamera;
    private CameraInput camInput;
    private InputAction zoomAction;
    private InputAction moveAction;
    private InputAction resetAction;
    private InputAction exitAction;
    private InputAction swapAction;
    private Camera[] allCams;
    private bool multipleDisplaysActive;
    private float highPriority = 10;
    private float lowPriority = 1;

    private string MAINCAM = "MainCamera";

    // Start is called before the first frame update
    void Start()
    {
        allCams = Camera.allCameras;
        camInput = new CameraInput();
        zoomAction = camInput.BirdsEyeCamera.Zoom;
        zoomAction.Enable();
        moveAction = camInput.BirdsEyeCamera.Move;
        moveAction.Enable();
        resetAction = camInput.BirdsEyeCamera.Reset;
        resetAction.Enable();
        exitAction = camInput.BirdsEyeCamera.Exit;
        exitAction.Enable();
        swapAction = camInput.BirdsEyeCamera.Swap;
        swapAction.Enable();

        if (!orthographicCamera.orthographic)
        {
            orthographicCamera.orthographic = true;
        }

        multipleDisplaysActive = checkMultipleDisplaysActive();
        if (!multipleDisplaysActive)
        {
            Debug.Log("Only one display active");
            foreach (Camera cam in allCams)
            {
                cam.targetDisplay = 0;
                if (cam.CompareTag(MAINCAM))
                {
                    cam.depth = highPriority;
                    Debug.Log("Main cam set to high priority");
                }
                else
                {
                    cam.depth = lowPriority;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (zoomAction.IsPressed())
        {
            orthographicCamera.orthographicSize += (zoomAction.ReadValue<float>() * 0.1f);
        }
        if (moveAction.IsPressed()) 
        {
            Vector3 translation;
            Vector2 inputValue = moveAction.ReadValue<Vector2>() * 0.1f;
            translation = new Vector3(inputValue.x, 0, inputValue.y);
            orthographicCamera.transform.localPosition += translation;
        }
        if (resetAction.WasPressedThisFrame())
        {
            EventManager.sceneReset();
        }
        if (exitAction.WasPressedThisFrame())
        {
            Application.Quit();
        }
        if (swapAction.WasPressedThisFrame())
        {
            Debug.Log("Swap pressed");
            SwapDisplay();
        }
    }

    void SwapDisplay()
    {
        if (allCams.Length < 2)
        {
            Debug.Log("Only one camera enabled");
            return;
        }

        for (int i = 0; i < allCams.Length; i++)
        {
            Camera cam = allCams[i];

            if (multipleDisplaysActive)
            {
                // NOTE: This solution assumes each camera has its own target display.
                if (cam.targetDisplay < allCams.Length - 1)
                {
                    cam.targetDisplay++;
                }
                else
                {
                    cam.targetDisplay = 0;
                }
            }
            else
            {
                if (cam.depth == highPriority)
                {
                    cam.depth = lowPriority;
                    if (i < allCams.Length - 1)
                    {
                        allCams[i + 1].depth = highPriority;
                    }
                    else
                    {
                        allCams[0].depth = highPriority;
                    }
                    return;
                }
            }
        }
    }

    bool checkMultipleDisplaysActive()
    {
        if (Display.displays.Length < 2) return false;
        else return true;
    }
}
