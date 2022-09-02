using UnityEngine;

public class InputManager : MonoBehaviour
{
    private static CarInput input;
    public static CarInput Instance()
    {
        if (input == null)
        {
            setInput();
        }
        return input;
    }
    
    void Awake()
    {
        setInput();
    }
    private static void setInput()
    {
        input = new CarInput();
        input.Car.Enable();
        input.UI.Enable();
    }

}
