using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Good_Point : MonoBehaviour
{
    public Image sprite;
    private void OnEnable()
    {
        if (sprite == null)
            sprite = GetComponent<Image>();
        sprite.color= Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
    }

    public void clicked()
    {
        //ParticleSystem particles = GetComponentInChildren<ParticleSystem>();
        PointManager.instance.add_point(true);
        //particles.startColor = GetComponent<Image>().color;
        //particles.transform.localPosition = Vector3.zero;
        //particles.transform.SetParent(null);
        //particles.Play();
        gameObject.SetActive(false);
    }
}
