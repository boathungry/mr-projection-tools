using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;
using UnityEngine.Events;

namespace MRProjectionTools
{
    [Serializable]
    public class StartingDate
    {
        public int day = 11;
        public int month = 10;
        public int year = 1996;
    }
    [Serializable]
    public class StartingTime
    {
        public int hour = 12;
        public int minute = 30;
    }
    public class SunControl : MonoBehaviour
    {
        public float latitude;
        public float longitude;
        public StartingDate date;
        public StartingTime time;
        public Light sun;

        private DateTime dateTime;
        private SunCalculations sunCalc;
        private SunInput sunInput;
        private InputAction moveSun;

        void SetSunPosition(DateTime dateTime)
        {
            Vector2 sunPos = sunCalc.GetSunPosition(dateTime, latitude, longitude);
            Debug.Log("Azimuth: " + sunPos.x + "| Altitude: " + sunPos.y);
            sun.transform.eulerAngles = new Vector3(sunPos.y, sunPos.x);
        }
        
        // Start is called before the first frame update
        void Start()
        {
            dateTime = new DateTime(date.year, date.month, date.day, time.hour, time.minute, 0);
            sunCalc = gameObject.AddComponent<SunCalculations>();
            SetSunPosition(dateTime);

            sunInput = new SunInput();
            moveSun = sunInput.Sun.Move;
            moveSun.Enable();
            Debug.Log(moveSun.enabled);
        }

        // Update is called once per frame
        void Update()
        {
            if (moveSun.IsPressed())
            {
                dateTime = dateTime.AddMinutes(moveSun.ReadValue<float>());
                SetSunPosition(dateTime);
                Debug.Log("Time:" + dateTime.ToShortTimeString() + "| Altitude: " + sun.transform.eulerAngles.y);
            }
        }
    }
}
