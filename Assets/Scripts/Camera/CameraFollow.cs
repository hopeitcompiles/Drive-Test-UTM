using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public static CameraFollow instance;
    private CarsManager cars_manager;
    private bool should_follow;
    public bool Should_follow
    {
        set { should_follow = value; }
    }
    public static CameraFollow Instance()
    {
        return instance;
    }
    void Awake()
    {
        instance = this;
        should_follow = true;
    }
    private void Start()
    {
        cars_manager = CarsManager.instance;
    }
    void FixedUpdate()
    {
        if(should_follow)
            set_middle_point();
    }
    
    void set_middle_point()
    {
        transform.SetPositionAndRotation(
            cars_manager.get_middle_point_between_cars(),
            transform.rotation);
    }
    
}
