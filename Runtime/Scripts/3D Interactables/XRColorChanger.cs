using UnityEngine;

public class TeleportReticleColorChanger : MonoBehaviour
{
    [Header("Color Settings")]
    public Color hoverEnterColor = Color.cyan;
    public Color hoverExitColor = Color.white;

    [Header("Teleport Reticle Settings")]
    public string teleportReticleTag = "TeleportReticle";

    private Renderer objectRenderer;
    private Color originalColor;
    private bool isHovering = false;

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        if (objectRenderer != null)
        {
            originalColor = objectRenderer.material.color;
        }
        else
        {
            Debug.LogWarning("No Renderer component found on this GameObject. Color changes will not be visible.");
        }

        // Ensure this object has a collider and it's set to trigger
        Collider collider = GetComponent<Collider>();
        if (collider == null)
        {
            collider = gameObject.AddComponent<BoxCollider>();
            Debug.Log("Added BoxCollider to the object for trigger detection.");
        }
        collider.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(teleportReticleTag) && !isHovering)
        {
            isHovering = true;
            ChangeColor(hoverEnterColor);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(teleportReticleTag) && isHovering)
        {
            isHovering = false;
            ChangeColor(hoverExitColor);
        }
    }

    private void ChangeColor(Color newColor)
    {
        if (objectRenderer != null)
        {
            objectRenderer.material.color = newColor;
        }
    }

    // Optional: Method to reset color when needed
    public void ResetColor()
    {
        if (objectRenderer != null)
        {
            objectRenderer.material.color = originalColor;
        }
        isHovering = false;
    }
}