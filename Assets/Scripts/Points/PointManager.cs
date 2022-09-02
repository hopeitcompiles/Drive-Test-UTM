using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointManager : MonoBehaviour
{
    private const int POINTS_GOAL = 25;

    public TMPro.TextMeshProUGUI ui_timer;
    public TMPro.TextMeshProUGUI ui_points;
    public TMPro.TextMeshProUGUI ui_objective;
    public TMPro.TextMeshProUGUI ui_start_timer;
    public GameObject pause_panel;
    public Panel start_panel;
    private bool is_game_paused;
    CarInput input;
    private float test_time = 45;
    private float timer_start = 0, timer_test = 0;
    private float points;
    public GameObject bad_point;
    public GameObject good_point;
    public static PointManager instance;
    public static PointManager Instance()
    {
        return instance;
    }
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        ui_objective.text = "Objetivo: " + POINTS_GOAL.ToString() + " puntos";
        points = 0; 
        ui_points.text = "Puntos: " + points.ToString();
        input = InputManager.Instance();
        start_panel.gameObject.SetActive(true);
        input.UI.Pause.performed += Pause_performed;
        input.UI.Click.performed += Click_performed;
    }

    private void Click_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        throw new System.NotImplementedException();
    }

    public void add_point(bool should_add)
    {
        points += should_add ? 1 : -1;
        if (points < 0)
            points = 0;
        ui_points.text = "Puntos: " + points.ToString();
    }
    private void Pause_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (start_panel.isActiveAndEnabled)
            return;
        is_game_paused = !is_game_paused;
        pause_panel.SetActive(is_game_paused);
        Time.timeScale = is_game_paused ? 0 : 1;
        CarsManager.instance.stop_audio(!is_game_paused);
    }

    public void start_game()
    {
        start_panel.gameObject.SetActive(false);
        start_panel.set_text_secondary("");
        ui_start_timer.transform.parent.gameObject.SetActive(true);
        timer_start = 3;
        InvokeRepeating("Countdown_Start", 0f, 1f);
    }
    void Load_information()
    {
        ui_timer.text = timer_test > 0 ? "Has chocado!" : "Terminado!";
        start_panel.set_text(timer_test > 0 ? "Buen intento!" : "Buen trabajo!");
        start_panel.set_text_secondary(timer_test > 0 ? "Tiempo restante " + (timer_test + 1).ToString() + " segundos"
            : "Has aprobado el test!");
    }
    public void Countdown_Start()
    {
        ui_start_timer.text = timer_start.ToString();
        timer_start -= 1;
        if (timer_start < 0)
        {
            timer_test = test_time;
            ui_start_timer.transform.parent.gameObject.SetActive(false);
            CancelInvoke("Countdown_Start");
            InvokeRepeating("Test_Run", 0f, 1f);
        }
    }
    public void stop_game()
    {

    }
    public void Test_Run()
    {
        ui_timer.text = "Tiempo " + timer_test.ToString() + " segundos";
        timer_test -= 1;
        if (timer_test < 0)
        {
            stop_game();
        }
    }
}
