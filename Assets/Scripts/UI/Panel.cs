using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel : MonoBehaviour
{
    public AudioSource _audio;
    public TMPro.TextMeshProUGUI ui_text;
    public TMPro.TextMeshProUGUI ui_text_secondary;
    public AudioClip[] clips;

    public void set_text(string text)
    {
        ui_text.text = text;
    }
    public void set_text_secondary(string text)
    {
        ui_text_secondary.text = text;
    }

    public void SetActive(bool state)
    {
        gameObject.SetActive(state);
    }

    
    private void OnEnable()
    {
        if (_audio == null)
            return;
        _audio.clip = clips[(int)Random.Range(0, clips.Length - 1)];
       _audio.Play();
    }
    private void OnDisable()
    {
        if (_audio == null)
            return;
        _audio.Stop();
    }
}
