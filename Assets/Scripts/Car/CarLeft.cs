using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarLeft : Car
{
    void Update()
    {
        car_controller.setSteering(input.Car.MoveLeft.ReadValue<Vector2>());/////////inputs
    }

    //public override void reset_car()
    //{
    //    gameObject.transform.position = initial_position;
    //    gameObject.transform.localEulerAngles = Vector3.zero;
    //    //star_car();
    //}
}
