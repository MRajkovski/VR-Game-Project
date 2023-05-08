using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class DayCycleController : MonoBehaviour
{

    [Range(0, 24)]
    public float timeOfDay;
    public float orbitSpeed = 1.0f;
    public Light sun;
    public Light moon;
    public Volume skyVolume;
    private bool isNight;
    public Material skyMat;
    private Color currentColor;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timeOfDay += Time.deltaTime * orbitSpeed;
        if(timeOfDay > 24)
        {
            timeOfDay = 0;
        }
        UpdateTime();
        

    }


    private void OnValidate()
    {
        UpdateTime();
    }

    private void UpdateTime()
    {
        float alpha = timeOfDay / 24.0f;
        float sunRotation = Mathf.Lerp(-90, 270, alpha);
        float moonRotation = sunRotation - 180;
        sun.transform.rotation = Quaternion.Euler(sunRotation, -150.0f, 0);
        moon.transform.rotation = Quaternion.Euler(moonRotation, -150.0f, 0);

        float exposureLerp = Mathf.PingPong(alpha + 0.5f, 1.0f);
        skyMat.SetFloat("_Exposure", exposureLerp);


        CheckNightDayTransition();
    }

    private void CheckNightDayTransition()
    {
        if (isNight)
        {
            if (moon.transform.rotation.eulerAngles.x > 180)
                
            {
                StartDay();
            }
        }
        else
        {
            if(sun.transform.rotation.eulerAngles.x > 180)
            {
                StartNight();
            }
            
        }
    }

    private void StartDay()
    {
        isNight = false;
        sun.enabled = true;
        moon.enabled = false;
    }

    private void StartNight()
    {
        isNight = true;
        sun.enabled = false;
        moon.enabled = true;
    }



}
