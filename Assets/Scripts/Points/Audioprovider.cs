using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audioprovider : MonoBehaviour
{
    [SerializeField]
    private AudioClip end_game;
    [SerializeField]
    private AudioClip point;
    [SerializeField]
    private AudioClip[] good_point;
    private AudioSource _audio;

    public static Audioprovider instance;

    public static Audioprovider Instance()
    {
        return instance;
    }
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        _audio = GetComponent<AudioSource>();
    }

    public AudioClip Good_point
    {
        get { return good_point[(int)Random.Range(0, good_point.Length - 1)]; }
    }
    public AudioClip Point
    {
        get { return point; }
    }

    public void PointSound()
    {
        if (_audio == null || point == null)
            return;
        _audio.PlayOneShot(point);
    }
    public void EndGameSound()
    {
        if (_audio == null || end_game == null)
            return;
        _audio.PlayOneShot(end_game);
    }
}
