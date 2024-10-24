using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventManager : MonoBehaviour
{
    public delegate void ButtonAction(int objectId);
    public static ButtonAction buttonPressed;

    public delegate void ResetAction();
    public static ResetAction sceneReset;

    public delegate void ObjectiveCompletionAction();
    public static ObjectiveCompletionAction objectivesCompleted;

    private void OnEnable()
    {
        sceneReset += ResetScene;
        objectivesCompleted += GameEnd;
    }

    private void OnDisable()
    {
        sceneReset -= ResetScene;
        objectivesCompleted -= GameEnd;
    }

    void GameEnd()
    {
        // Show UI on both cameras to indicate that the user is done
        // Disable input?
    }

    void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
