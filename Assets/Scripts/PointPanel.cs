using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PointPanel : MonoBehaviour
{
    PointClick[] points;
    int last_index = -1,index=0;
    float max_time = 3,change_time=0;

    public AudioSource _audio;
    public AudioClip[] clips;

    public static PointPanel instance;
    public PointPanel Instance()
    {
        return instance;
    }
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        points = GetComponentsInChildren<PointClick>();
    }
    private void FixedUpdate()
    {
        if (change_time < Time.time)
        {
            Alternate();
            change_time = Time.time + max_time;
        }
    }
    public void reset_timer()
    {
        change_time = 0;
        last_index = -1;
    }
    public void Alternate()
    {
        do
        {
            index = (int)Random.Range(0, points.Length - 1);
        } while (index==last_index);
        points[index].toggle_active();
        if (last_index != -1)
            points[last_index].toggle_active();
        last_index = index;

    }

    private void OnEnable()
    {
        if (_audio == null)
            return;
        _audio.clip = clips[(int)Random.Range(0, clips.Length - 1)];
        _audio.Play();
        _audio.volume = 0.5f;
    }
    private void OnDisable()
    {
        if (_audio == null)
            return;
        _audio.Stop();
    }
}
