using System.Collections;
using UnityEngine.InputSystem; 
using UnityEngine;

public class PointManager : MonoBehaviour
{
    private const int POINTS_GOAL = 30;
    private const float TEST_TIME = 25;

    public TMPro.TextMeshProUGUI ui_timer;
    public TMPro.TextMeshProUGUI ui_points;
    public TMPro.TextMeshProUGUI ui_objective;
    public TMPro.TextMeshProUGUI ui_start_timer;
    public TMPro.TextMeshProUGUI ui_start_points;

    public GameObject pause_panel;
    public Panel start_panel;
    private bool is_game_paused;
    public bool is_game_stopped;
    CarInput input;
    private float timer_start = 0, timer_test = 0;
    private float points;
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
        PointPanel.instance.gameObject.SetActive(false);
        is_game_stopped = true;
        ui_objective.text = "Objetivo: " + POINTS_GOAL.ToString() + " puntos";
        points = 0; 
        ui_points.text = "Puntos: " + points.ToString();
        input = InputManager.Instance();
        start_panel.gameObject.SetActive(true);
        input.UI.Pause.performed += Pause_performed;
    }
    public void add_point(bool should_add)
    {
        if (is_game_stopped || is_game_paused)
            return;
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
        ui_timer.text = "Tiempo";
        ui_points.text = "Puntos";
        PointPanel.instance.gameObject.SetActive(true);
        points = 0;
        start_panel.gameObject.SetActive(false);
        start_panel.set_text_secondary("");
        ui_start_timer.transform.parent.gameObject.SetActive(true);
        timer_start = 3;
        InvokeRepeating("Countdown_Start", 0f, 1f);
    }
    void Load_information()
    {
        ui_timer.text = timer_test > 0 ? "Has chocado!" : "Terminado!";
        start_panel.set_text(points < POINTS_GOAL ? "Buen intento!" : "Buen trabajo!");
        start_panel.set_text_secondary(points < POINTS_GOAL ? "Vuelve a intentarlo "
            : "Has aprobado el test!");
        ui_start_points.text = "Has obtenido " + points.ToString()+" puntos"+ (points < POINTS_GOAL?" de "+POINTS_GOAL.ToString():"");
    }
    public void Countdown_Start()
    {
        ui_start_timer.text = timer_start.ToString();
        timer_start -= 1;
        if (timer_start < 0)
        {
            timer_test = TEST_TIME;
            is_game_stopped = false;
            ui_start_timer.transform.parent.gameObject.SetActive(false);
            CancelInvoke("Countdown_Start");
            InvokeRepeating("Test_Run", 0f, 1f);
        }
    }
    public void stop_game()
    {
        is_game_stopped = true;
        Audioprovider.instance.EndGameSound();
        CancelInvoke("Test_Run");
        StartCoroutine(ActivateAfterTime(1.5f));
        Load_information();
    }
    IEnumerator ActivateAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        PointPanel.instance.gameObject.SetActive(false);
        start_panel.SetActive(true);
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
