using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TireTrail : MonoBehaviour
{
    CarController car;
    TrailRenderer trails;
    void Start()
    {
        car = GetComponentInParent<CarController>();
        trails = GetComponent<TrailRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (car != null)
        {
            if (car.isCarScreeching())
            {
                trails.emitting = true;

            }
            else
            {
                trails.emitting = false;

            }
        }
    }
}
