using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TimeMaster : MonoBehaviour
{

    [SerializeField] private const float REAL_SECONDS_PER_IN_GAME_DAY = 300f; //1440f нормальное игровое время, 60f время для тестирования
    [SerializeField] private Light sun;
    [SerializeField] private float sunRotationSpeed;
    [SerializeField] private Transform HourArrow;
    [SerializeField] private Transform MinuteArrow;

    private Transform hourArrowTransform;
    private Transform minuteArrowTransform;

    [SerializeField] public float day;
    [SerializeField] private float dayNormalized;
    [SerializeField] public bool isNight = true;
    [SerializeField] private float rotationDegreesPerDay = 360f;

    [SerializeField] private float hoursPerDay = 24f;

    [SerializeField] public float currentHourTime;

    [Header("LightingPreset")]
    [SerializeField] private Gradient skyColor;
    [SerializeField] private Gradient equatorColor;
    [SerializeField] private Gradient sunColor;


    void Awake()
    {
        day = 0f;
        hourArrowTransform = HourArrow;
        minuteArrowTransform = MinuteArrow;
    }
    private void nightCheck()
    {   
        if(currentHourTime > 5) isNight = false;
        if(currentHourTime > 19) isNight = true;
        
    }
    private void UpdateSunRotation()
    {
        float sunRotation = Mathf.Lerp(-90 ,270, dayNormalized);
        sun.transform.rotation = Quaternion.Euler(sunRotation, sun.transform.rotation.y, sun.transform.rotation.z);

    }

    private void UpdateLighting()
    {
        float timeFraction = dayNormalized;
        RenderSettings.ambientEquatorColor = equatorColor.Evaluate(timeFraction);
        RenderSettings.ambientSkyColor = skyColor.Evaluate(timeFraction);
        sun.color = sunColor.Evaluate(timeFraction);
    }
    void Update()
    {
        day += Time.deltaTime / REAL_SECONDS_PER_IN_GAME_DAY;
        dayNormalized = day % 1f;

        hourArrowTransform.eulerAngles = new Vector3(0, 0, -dayNormalized * rotationDegreesPerDay * 2 );
        minuteArrowTransform.eulerAngles = new Vector3(0, 0, -dayNormalized * rotationDegreesPerDay * hoursPerDay);

        currentHourTime = Mathf.Floor(dayNormalized * hoursPerDay);
        
        UpdateSunRotation();
        UpdateLighting();
        nightCheck();
    }
}
