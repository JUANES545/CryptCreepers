using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraController : MonoBehaviour
{
    private CinemachineVirtualCamera vCam;
    private CinemachineBasicMultiChannelPerlin noise;
    void Start()
    {
        vCam = GetComponent<CinemachineVirtualCamera>();
        noise = vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shake(float duration = 0.1f, float amplitude = 1.5f, float frecuency = 20)
    {
        StartCoroutine(ApplyNoiseRoutine(duration, amplitude, frecuency));
    }

    IEnumerator ApplyNoiseRoutine(float duration, float amplitude, float frecuency)
    {
        noise.m_AmplitudeGain = amplitude;
        noise.m_FrequencyGain = frecuency;
        yield return new WaitForSeconds(duration);
        noise.m_AmplitudeGain = 0;
        noise.m_FrequencyGain = 0;
    }
}
