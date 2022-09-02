using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent()]
[RequireComponent(typeof(CarController))]

public class Car : MonoBehaviour
{
    protected CarController car_controller;
    protected CarInput input;
    protected Vector2 initial_position;
    protected CarAudio car_audio;
    public CarAudio Car_audio
    {
       get { return car_audio; }
    }

    private void Awake()
    {
        initial_position = transform.position;
    }
    void Start()
    {
        car_audio = GetComponent<CarAudio>();
        car_controller = GetComponent<CarController>();
        input = InputManager.Instance();

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (GameController.instance.Is_game_stopped)
        {
            CarsManager.instance.set_cars();
            return;
        }
        GameController.instance.Stop_Game();
        car_audio.play_hit(collision);
        CameraShake.instance.shake_camera(5f,0.7f);
    }
    public void star_car()
    {
        //car_controller.start_car();
    }
    public void slow_down()
    {
        car_controller.slow_down();
    }
    public void stop_car() {
        car_controller.is_car_stopped = true;
        stop_audio(true);
    }
    public void set_car()
    {
        //car_controller.rb.isKinematic = true;
        //car_controller.setSteering(Vector3.zero);
        //car_controller.rb.SetRotation(0);
        //car_controller.rb.angularVelocity = 0f;
        //car_controller.rb.velocity = Vector3.zero;
        transform.position = initial_position;
        //transform.rotation = Quaternion.identity;
        //car_controller.rb.isKinematic = false;
        this.star_car();
    }
    public void stop_audio(bool stop)
    {
        if (car_audio != null)
            car_audio.stop_audio(stop);
    }


}
