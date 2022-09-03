using UnityEngine;
using UnityEngine.UI;

public class PointClick : MonoBehaviour
{
    private bool is_active;
    private Image image;
    void Start()
    {
        is_active = false;
        image = gameObject.GetComponent<Image>();
        image.color = Color.gray;
    }

    public void toggle_active()
    {
        is_active = !is_active;
        image.color =is_active? Color.green: Color.gray;
    }

    public void clicked()
    {
        if (!is_active ||PointManager.instance.is_game_stopped)
            return;
        Audioprovider.instance.PointSound();
        PointPanel.instance.reset_timer();
        toggle_active();
        PointManager.instance.add_point(true);
    }
}
