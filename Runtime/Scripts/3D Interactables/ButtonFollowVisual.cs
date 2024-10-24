using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
public class ButtonFollowVisual : MonoBehaviour
{
    public Transform visualTarget;
    private Vector3 offset;
    private bool freeze = false;
    public float resetSpeed = 5.0f;
    private Transform pokeAttachTransform;
    private XRBaseInteractable interactable;
    private bool isFollowing = false;
    public Vector3 localAxis;
    private Vector3 initialLocalPosition;
    public float followAngleThreshold = 45f;
    public AudioSource houseToggleSound;
    public GameObject toggleGameObject;

    public bool isLightSensitive = false;
    public Light directionalLight = null;
    public Material onMaterial;
    public Material offMaterial;
    public Material inactiveMaterial;
    private MeshRenderer visualRenderer;
    private bool isActive = true;
    // Start is called before the first frame update
    void Start()
    {
        initialLocalPosition = visualTarget.localPosition;
        interactable = GetComponent<XRBaseInteractable>();
        //Debug.Log("Adding hoverEntered listener");
        interactable.hoverEntered.AddListener(Follow);

        //Debug.Log("Adding hoverExited listener");
        interactable.hoverExited.AddListener(Reset);

        //Debug.Log("Adding selectEntered listener");
        interactable.selectEntered.AddListener(Freeze);

        visualRenderer = visualTarget.GetComponent<MeshRenderer>();
        if (visualRenderer == null)
        {
            Debug.LogError("MeshRenderer not found on visualTarget");
        }
        UpdateMaterial();
    }

    public void Follow(BaseInteractionEventArgs hover)
    {
        if(hover.interactorObject is XRPokeInteractor)
        {
            //Debug.Log("Follow method called");
            XRPokeInteractor interactor = (XRPokeInteractor)hover.interactorObject;
            
            pokeAttachTransform = interactor.attachTransform;
            offset = visualTarget.position - pokeAttachTransform.position;
            float pokeAngle = Vector3.Angle(offset, visualTarget.TransformDirection(localAxis));

            if(pokeAngle < followAngleThreshold)
            {
                isFollowing = true;
                freeze = false;
            }
        }
    }
    public void Reset(BaseInteractionEventArgs hover)
    {
        if(hover.interactorObject is XRPokeInteractor)
        {
            //Debug.Log("Reset method called");
            isFollowing = false;
            freeze = false;
        }
    }
    public void Freeze(BaseInteractionEventArgs hover)
    {
        if (hover.interactorObject is XRPokeInteractor)
        {
            //Debug.Log("Freeze method called");
            freeze = true;
            // Play the freeze sound
            if (isActive)
            {
                if (houseToggleSound != null)
                {
                    houseToggleSound.PlayOneShot(houseToggleSound.clip);
                }

                Toggle();
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (freeze)
        {
            return;
        }
        if (isLightSensitive)
        {
            if (directionalLight != null)
            {
                if (directionalLight.transform.rotation.eulerAngles.x < 45 | directionalLight.transform.rotation.eulerAngles.x > 135)
                {
                    Debug.Log("low light");
                    SetIsActive(true);
                }
                else
                {
                    SetIsActive(false);
                }
            }
        }
        if (isFollowing)
        {
            
            Vector3 localTargetPosition = visualTarget.InverseTransformPoint(pokeAttachTransform.position + offset);
            Vector3 constrainedLocalTargetPosition = Vector3.Project(localTargetPosition, localAxis);
            visualTarget.position = visualTarget.TransformPoint(constrainedLocalTargetPosition);
        }
        else
        {
            visualTarget.localPosition = Vector3.Lerp(visualTarget.localPosition, initialLocalPosition, Time.deltaTime * resetSpeed);
            
        }
    }
    private void UpdateMaterial()
    {
        if (isActive)
        {
            if (visualRenderer != null && onMaterial != null && offMaterial != null)
            {
                visualRenderer.material = toggleGameObject.activeSelf ? onMaterial : offMaterial;
            }
        }
        else
        {
            if (visualRenderer != null && inactiveMaterial != null)
            {
                visualRenderer.material = inactiveMaterial;
            }
        }
    }

    private void Toggle()
    {
        if (EventManager.buttonPressed != null)
        {
            EventManager.buttonPressed(gameObject.transform.parent.gameObject.GetInstanceID());
        }

        // Toggle the GameObject's active state
        if (toggleGameObject != null)
        {
            toggleGameObject.SetActive(!toggleGameObject.activeSelf);
            UpdateMaterial();
        }
    }

    private void SetIsActive(bool newValue)
    {
        isActive = newValue;
        UpdateMaterial();
    }
}
