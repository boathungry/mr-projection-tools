using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ObjectiveMarker : MonoBehaviour
{
    public Camera overheadCamera;
    public Canvas mapUI;
    public Sprite objectiveMarkerSprite;
    public Vector3 spriteScale = Vector3.one;
    public GameObject[] objectives;

    private int currentObjectiveIndex;
    private RectTransform markerTransform;
    private GameObject marker;

    private void OnEnable()
    {
        EventManager.buttonPressed += OnButtonPressed;
    }

    private void OnDisable()
    {
        EventManager.buttonPressed -= OnButtonPressed;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentObjectiveIndex = 0;
        AddObjectiveMarker();
        
    }

    // Update is called once per frame
    void Update()
    {
        SetObjectiveMarkerPosition(objectives[currentObjectiveIndex].transform.position);
    }

    void AddObjectiveMarker()
    {
        if (marker) RemoveObjectiveMarker();
        marker = new GameObject("ObjectiveMarker");

        marker.transform.SetParent(mapUI.transform);
        markerTransform = marker.AddComponent<RectTransform>();

        // Make sure objective marker is at the same offset from the camera as the UI
        marker.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);

        markerTransform.transform.SetParent(mapUI.transform);
        markerTransform.localScale = spriteScale;
        markerTransform.anchorMin = Vector2.zero;
        markerTransform.anchorMax = Vector2.zero;
        markerTransform.anchoredPosition = Vector2.zero;

        Image image = marker.AddComponent<Image>();
        image.sprite = objectiveMarkerSprite;
    }

    void RemoveObjectiveMarker()
    {
        if (!marker) return;
        Destroy(marker);
    }

    void SetObjectiveMarkerPosition(Vector3 objectiveWorldPosition)
    {
        Vector3 screenPos = overheadCamera.WorldToScreenPoint(objectiveWorldPosition);

        // Add a sine wave offset to make objective marker "float" up and down
        Vector3 sinOffset = new Vector3(0, Mathf.Sin(Time.time * 5) * Time.deltaTime * 500, 0);

        markerTransform.anchoredPosition = screenPos + sinOffset;
    }

    void NextObjective()
    {
        if (currentObjectiveIndex < objectives.Length - 1)
        {
            currentObjectiveIndex++;
        }
        else
        {
            if (EventManager.objectivesCompleted != null)
            {
                EventManager.objectivesCompleted();
            }
            else
            {
                currentObjectiveIndex = 0;
            }
        }
    }

    void OnButtonPressed(int buttonID)
    {
        Debug.Log("Current objective: " + objectives[currentObjectiveIndex].GetInstanceID());
        if (buttonID == objectives[currentObjectiveIndex].GetInstanceID())
        {
            Debug.Log("Current objective achieved");
            NextObjective();
        }
    }
}
