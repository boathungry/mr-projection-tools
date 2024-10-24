using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableOnObjectivesComplete : MonoBehaviour
{
    private void Start()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        EventManager.objectivesCompleted += ObjectivesComplete;
    }

    private void OnDisable()
    {
        EventManager.objectivesCompleted -= ObjectivesComplete;
    }

    void ObjectivesComplete()
    {
        Invoke("EnableChildren", 2);
    }

    void EnableChildren()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
    }
}
