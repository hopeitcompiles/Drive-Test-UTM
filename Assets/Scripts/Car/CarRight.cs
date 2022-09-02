using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CarRight : Car
{
    void Update()
    {
        car_controller.setSteering( input.Car.MoveRight.ReadValue<Vector2>());/////////inputs
    }

    
}
