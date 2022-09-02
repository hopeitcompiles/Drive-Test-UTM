using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraShake : MonoBehaviour
{
    private CinemachineVirtualCamera cinemachine;
    public static CameraShake instance;
    public static CameraShake Instance()
    {
        return instance;
    }
    private void Awake()
    {
        instance = this;
        cinemachine = GetComponent<CinemachineVirtualCamera>();
    }
    public void shake_camera(float intensity, float time)
    {
        CinemachineBasicMultiChannelPerlin channelPerlin =
        cinemachine.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        channelPerlin.m_AmplitudeGain = intensity;
        Invoke("stop_shake", time);
    }
    public void stop_shake()
    {
        CinemachineBasicMultiChannelPerlin channelPerlin =
       cinemachine.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        channelPerlin.m_AmplitudeGain = 0;
    }
}
