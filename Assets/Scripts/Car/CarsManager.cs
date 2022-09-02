using UnityEngine;

public class CarsManager : MonoBehaviour
{
    public static CarsManager instance;
    public Car carA, carB;
    private float max_distance = 4;

    public static CarsManager Instance()
    {
        return instance;
    }
    private void Awake()
    {
        Debug.Log("awake");
        instance = this;
        if (carA == null || carB == null)
        {
            Car[] cars = FindObjectsOfType<Car>();
            foreach (Car car in cars)
            {
                if (car.TryGetComponent(out CarLeft left))
                {
                    carA = car;
                }
                if (car.TryGetComponent(out CarRight right))
                {
                    carB = car;
                }
            }
        }
        if (carB.transform.position.x < carA.transform.position.x)
        {
            Vector2 pos = carA.transform.position;
            carA.transform.SetPositionAndRotation(new Vector2(carB.transform.position.x, pos.y), transform.rotation);
            carB.transform.SetPositionAndRotation(new Vector2(pos.x, pos.y), transform.rotation);
        }
        else
        {
            carB.transform.SetPositionAndRotation(new Vector2(carB.transform.position.x, carA.transform.position.y), transform.rotation);
        }
    }

 
    private void FixedUpdate()
    {
        if (GameController.instance.Is_game_stopped)
            return;
        slow_down_if_too_far();
    }
    void slow_down_if_too_far()
    {
        if (Mathf.Abs(carA.transform.position.y - carB.transform.position.y) >= max_distance)
        {
            if (carA.transform.position.y > carB.transform.position.y)
            {
                carA.slow_down();
                Debug.Log("slow car a");
            }
            else
            {
                carB.slow_down();
                Debug.Log("slow car b");
            }
        }
    }

    public void stop_cars()
    {
        carA.stop_car();
        carB.stop_car();
    }
    public void stop_audio(bool stop)
    {
        carA.stop_audio(stop);
        carB.stop_audio(stop);
    }
    public void set_cars()
    {
        carA.set_car();
        carB.set_car();
    }
    public Vector2 get_middle_point_between_cars()
    {
        if (carA != null && carB != null)
        {
            return (carA.transform.position + carB.transform.position) / 2;
        }
        if (carA == null)
        {
            return carB.transform.position;
        }
        if (carB == null)
        {
            return carA.transform.position;
        }
        return new Vector2(0, 0);
    }

}
