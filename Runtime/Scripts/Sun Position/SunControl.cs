using UnityEngine;
using System;
using UnityEngine.InputSystem;

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
        public GameObject compass;
        [Range(0, 360)]
        public float rotationOffset = 0;

        private DateTime dateTime;
        private SunCalculations sunCalc;
        private SunInput sunInput;
        private InputAction moveSun;
        private InputAction setDate;

        /// <summary>
        /// Sets the new date and time, then updates the sun's position based on it.
        /// </summary>
        /// <param name="newDateTime"></param>
        void SetDateTime(DateTime newDateTime)
        {
            dateTime = newDateTime;
            SetSunPosition(dateTime);
        }

        private float GetRotationOffset()
        {
            if (compass)
            {
                return compass.transform.rotation.eulerAngles.y;
            }
            else
            {
                return rotationOffset;
            }
        }

        /// <summary>
        /// Linearly interpolates the time of day between 00:00 and 23:59 based on the fraction provided.
        /// Updates sun position.
        /// Accepts values from 0 to 1.
        /// </summary>
        /// <param name="frac">A value between 0 and 1 (inclusive).</param>
        public void SetDayFraction(float frac)
        {
            float startMinutes = 0;
            float endMinutes = 60 * 23 + 59;
            float currMinutesTotal = (long)Mathf.Lerp(startMinutes, endMinutes, frac);
            float currMinutes = currMinutesTotal % 60;
            int currHours = Mathf.RoundToInt((currMinutesTotal - currMinutes) / 60);
            SetDateTime(new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, currHours, Mathf.RoundToInt(currMinutes), 0));
        }

        /// <summary>
        /// Update the sun's position based on the date and time.
        /// </summary>
        /// <param name="dateTime"></param>
        void SetSunPosition(DateTime dateTime)
        {
            Vector2 sunPos = sunCalc.GetSunPosition(dateTime, latitude, longitude);
            sun.transform.eulerAngles = new Vector3(sunPos.y, sunPos.x + GetRotationOffset());
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

            setDate = sunInput.Sun.SetDate;
            setDate.Enable();
        }

        // Update is called once per frame
        void Update()
        {
            if (moveSun.IsPressed())
            {
                SetDateTime(dateTime.AddMinutes(moveSun.ReadValue<float>()));
            }
        }
    }
}
