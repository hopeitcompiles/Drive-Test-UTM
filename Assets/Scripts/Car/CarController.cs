using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CarController : MonoBehaviour
{
    private const float DEFAULT_ACCELERATION = 0.7f;
    [Header("Car Settings")]
    public float acceleration_factor = 3f;
    public float turn_factor = 3.5f;
    private float drag_factor = 2;
    public float speed = 0;
    private float acceleration = 0;//this should come from user input, but it is like it is always pressed
    private float steering = 0;
    private float rotation = 0;

    private float max_speed = 6f;
    public bool is_car_stopped = false;
    
    public Rigidbody2D rb;
    void Start()
    {
        acceleration = DEFAULT_ACCELERATION;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ApplyEngineForce();
        ApplyDrag();
        Steering();
        SlowDown();
    }
    public void start_car()
    {
        is_car_stopped = false;
        acceleration = DEFAULT_ACCELERATION;
    }
    void ApplyEngineForce()
    {
        if (is_car_stopped)
            return;
        if (speed >= max_speed)
            return;
        speed = rb.velocity.magnitude;
        if (rb.velocity.sqrMagnitude > max_speed * max_speed)
            return;
        Vector2 force = transform.up * acceleration * acceleration_factor/ (1+9*(speed/max_speed));
        rb.AddForce(force, ForceMode2D.Force);
    }

    void Steering()
    {
        float min_steering = rb.velocity.magnitude / 4.1f;
        min_steering = Mathf.Clamp01(min_steering);
        rotation -= steering * turn_factor*min_steering;
        rb.MoveRotation(rotation);
    }
    public void setSteering(Vector2 steering)
    {
        this.steering = steering.x;
        //this.acceleration = steering.y;
    }
    void ApplyDrag()
    {
        Vector2 forward_velocity = transform.up * (Vector2.Dot(rb.velocity,transform.up));
        Vector2 side_velocity = transform.right * getSideVelocity();
        rb.velocity = forward_velocity + side_velocity / drag_factor;
    }
    private float getSideVelocity()
    {
        return  Vector2.Dot(rb.velocity, transform.right);

    }
    
    private void SlowDown()
    {
        if (is_car_stopped)
            rb.drag = Mathf.Lerp(rb.drag, 3.0f, Time.fixedDeltaTime);
        else 
            rb.drag = 0;
    }
    public void slow_down()
    {
        is_car_stopped = true;
        StartCoroutine(ActivateAfterTime( .3f));
    }
    IEnumerator ActivateAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        is_car_stopped = false;
    }
    public bool isCarScreeching()
    {
        if (is_car_stopped)
            return true;
        if (acceleration < 0 && rb.velocity.magnitude > 0)
            return true;
        if (Mathf.Abs(getSideVelocity()) > 0.2f)
            return true;
        return false;
    }
}
