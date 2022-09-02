using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bad_Point : MonoBehaviour
{
    public void clicked()
    {
        ParticleSystem particles = GetComponentInChildren<ParticleSystem>();
        PointManager.instance.add_point(false);
        particles.startColor = GetComponent<Image>().color;
        particles.transform.localPosition = Vector3.zero;
        particles.transform.localScale = Vector3.one * 20;
        particles.transform.SetParent(null);
        particles.Play();
        gameObject.SetActive(false);
    }
}
