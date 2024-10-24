using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[System.Serializable]
public class RayHapticSettings
{
    [Range(0f, 1f)]
    public float intensity;
    public float duration;

    public void TriggerHaptic(BaseInteractionEventArgs eventArgs)
    {
        if (eventArgs.interactorObject is XRRayInteractor rayInteractor)
        {
            TriggerHaptic(rayInteractor);
        }
        else
        {
            Debug.Log("Interactor is not XRRayInteractor: " + eventArgs.interactorObject.GetType().ToString());
        }
    }

    public void TriggerHaptic(XRRayInteractor rayInteractor)
    {
        if (rayInteractor != null)
        {
            XRBaseController controller = rayInteractor.GetComponent<XRBaseController>();
            if (controller != null && intensity > 0)
            {
                controller.SendHapticImpulse(intensity, duration);
            }
            else if (controller == null)
            {
                Debug.LogError("Could not find XRBaseController on the XRRayInteractor");
            }
        }
        else
        {
            Debug.LogError("RayInteractor is null");
        }
    }
}

public class RayHaptic : MonoBehaviour
{
    [Header("Haptic Settings")]
    public RayHapticSettings onHoverEnter;
    public RayHapticSettings onHoverExit;
    public RayHapticSettings selectEntered;
    public RayHapticSettings selectExited;

    void Start()
    {
        XRBaseInteractable interactable = GetComponent<XRBaseInteractable>();
        if (interactable != null)
        {
            interactable.hoverEntered.AddListener(onHoverEnter.TriggerHaptic);
            interactable.hoverExited.AddListener(onHoverExit.TriggerHaptic);
            interactable.selectEntered.AddListener(selectEntered.TriggerHaptic);
            interactable.selectExited.AddListener(selectExited.TriggerHaptic);
        }
        else
        {
            Debug.LogError("XRBaseInteractable component not found on this GameObject");
        }
    }
}