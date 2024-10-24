using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MeasuringStickResizer : MonoBehaviour
{
    public Camera TopDownCamera;
    public RectTransform MeasuringStick;
    public TMP_Text MetersDisplayed;
    private int Meters;
    // Start is called before the first frame update
    void Start()
    {
        Meters = 20;
    }

    // Update is called once per frame
    void Update()
    {
        ScaleMeasuringStick();
        MeasuringStick.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Meters * 80 / TopDownCamera.orthographicSize);
    }

    void ScaleMeasuringStick()
    {
        if (MeasuringStick.sizeDelta.x < 100)
        {
            SetMeters(Meters * 2);
        }
        else if (MeasuringStick.sizeDelta.x > 200)
        {
            SetMeters(Meters / 2);
        }
    }

    void SetMeters(int NewValue)
    {
        Meters = NewValue;
        UpdateText();
    }

    void UpdateText()
    {
        string NewText = Meters.ToString() + 'm';
        MetersDisplayed.text = NewText;
    }
}
