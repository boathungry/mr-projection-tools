using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System;

[System.Serializable]
public class HapticSettings
{
    [Range(0f, 1f)]
    public float intensity;
    public float duration;

    public void TriggerHaptic(BaseInteractionEventArgs eventArgs)
    {
        //Debug.Log("TriggerHaptic method called");
        if (eventArgs.interactorObject is XRPokeInteractor pokeInteractor)
        {
            //Debug.Log("Interactor is XRPokeInteractor");
            TriggerHaptic(pokeInteractor);
        }
        else
        {
            Debug.Log("Interactor is not XRPokeInteractor: " + eventArgs.interactorObject.GetType().ToString());
        }
    }

    public void TriggerHaptic(XRPokeInteractor pokeInteractor)
    {
        if (pokeInteractor != null)
        {
            // Access the XRController directly from the parent
            XRBaseController controller = pokeInteractor.transform.parent.GetComponent<XRBaseController>();
            if (controller != null)
            {
                //Debug.Log($"Attempting to send haptic impulse. Intensity: {intensity}, Duration: {duration}");
                if (intensity > 0)
                {
                    controller.SendHapticImpulse(intensity, duration);
                    //Debug.Log("Haptic impulse sent");
                }
                else
                {
                    return;
                }
            }
            else
            {
                Debug.LogError("Could not find XRBaseController on the parent of XRPokeInteractor");
            }
        }
        else
        {
            Debug.LogError("PokeInteractor is null");
        }
    }
}

public class XRIUIHaptics : MonoBehaviour
{
    public HapticSettings OnHoverEnter;
    public HapticSettings OnHoverExit;
    public HapticSettings SelectEntered;
    public HapticSettings SelectExited;

    void Start()
    {
        //Debug.Log("XRIUIHaptics Start method called");
        XRBaseInteractable interactable = GetComponent<XRBaseInteractable>();
        if (interactable != null)
        {
            //Debug.Log("XRBaseInteractable found, adding listeners");
            interactable.hoverEntered.AddListener(OnHoverEnter.TriggerHaptic);
            interactable.hoverExited.AddListener(OnHoverExit.TriggerHaptic);
            interactable.selectEntered.AddListener(SelectEntered.TriggerHaptic);
            interactable.selectExited.AddListener(SelectExited.TriggerHaptic);
        }
        else
        {
            Debug.LogError("XRBaseInteractable component not found on this GameObject");
        }
    }
}