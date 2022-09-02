using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public TMPro.TextMeshProUGUI ui_timer;
    public TMPro.TextMeshProUGUI ui_start_timer;

    private float test_time = 45;
    private float timer_start =0,timer_test=0;

    private bool is_game_paused;
    private bool is_game_stopped;

    public Panel start_panel;
    public GameObject pause_panel;

    public static GameController instance;

    CarInput input;
    public bool Is_game_stopped
    {
        get { return is_game_stopped; }
    }
    public static GameController Instance()
    {
        return instance;
    }
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        timer_test = 0;
        pause_panel.gameObject.SetActive(false);
        start_panel.set_text_secondary("");
        is_game_stopped = true;
        is_game_paused = false;
        input = InputManager.Instance();
        input.UI.Pause.performed += Pause_performed;
        if (Context.first_run)
        {
            start_panel.gameObject.SetActive(true);
        }
        else
        {
            Start_Test();
        }
        Context.first_run = false;
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

    public void Star_Game()
    {
        is_game_stopped = false;
        CarsManager.instance.set_cars();
        start_panel.SetActive(false);
        if (timer_test == 0)
        {
            Start_Test();
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
    
    public void Stop_Game()
    {
        is_game_stopped = true;
        CancelInvoke("Test_Run");
        CarsManager.instance.stop_cars();
        StartCoroutine(ActivateAfterTime(1.5f));
        Load_information();
    }
    IEnumerator ActivateAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        start_panel.SetActive(true);
    }
    void Load_information()
    {
        ui_timer.text = timer_test>0? "Has chocado!":"Terminado!";
        start_panel.set_text(timer_test>0?"Buen intento!":"Buen trabajo!");
        start_panel.set_text_secondary(timer_test > 0 ? "Tiempo restante "+(timer_test + 1).ToString()+" segundos" 
            : "Has aprobado el test!");
    }
    private void Start_Test()
    {
        ui_start_timer.transform.parent.gameObject.SetActive(true);
        timer_start = 3;
        InvokeRepeating("Countdown_Start", 0f, 1f);
        CarsManager.instance.set_cars();
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
    public void Test_Run()
    {
        ui_timer.text = "Tiempo " + timer_test.ToString() + " segundos";
        timer_test -= 1;
        if (timer_test < 0)
        {
            Stop_Game();
        }
    }
}
