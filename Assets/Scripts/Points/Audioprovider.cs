using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audioprovider : MonoBehaviour
{
    public AudioClip bad_point;
    private AudioClip[] good_point;

    public AudioClip Good_point
    {
        get { return good_point[(int)Random.Range(0, good_point.Length - 1)]; }
    }
    public AudioClip Bad_point
    {
        get { return bad_point; }
    }
}
