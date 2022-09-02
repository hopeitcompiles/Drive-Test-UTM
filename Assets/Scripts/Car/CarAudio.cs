using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CarAudio : MonoBehaviour
{

    public AudioSource hit_audio_source;
    public AudioSource engine_audio_source;
    public AudioSource screetch_audio_source;
    private CarController car;
    private void Awake()
    {
        car = GetComponent<CarController>();
    }
    private void Update()
    {
        update_engine_audio();
    }

    void update_engine_audio()
    {
        float desired_volume =GameController.instance.Is_game_stopped?0: car.speed*2 * 0.05f;
        desired_volume = Mathf.Clamp(desired_volume, 0.2f, 1f);
        engine_audio_source.volume = Mathf.Lerp(engine_audio_source.volume, desired_volume, Time.deltaTime * 10);
        float desired_pitch = car.speed*2 * 0.2f;
        desired_pitch = Mathf.Clamp(desired_pitch, 0.5f, 2f);
        engine_audio_source.pitch = Mathf.Lerp(engine_audio_source.pitch, desired_pitch, Time.deltaTime * 1.5f);
    }

    public void stop_audio(bool stop)
    {
        if (stop)
        {
            screetch_audio_source.volume = 0;
            screetch_audio_source.pitch = 0;
        }
        
    }
    public void play_hit(Collision2D collision)
    {
        float relativeVelocity = collision.relativeVelocity.magnitude;

        float volume = relativeVelocity * 0.1f;

        hit_audio_source.pitch = Random.Range(0.95f, 1.05f);
        hit_audio_source.volume = volume;

        if (!hit_audio_source.isPlaying)
            hit_audio_source.Play();
    }

}
