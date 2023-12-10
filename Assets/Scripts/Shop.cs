using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private GameObject play_menu;
    [SerializeField] private float coff = 5f;
    private bool shopping = false;
    private RectTransform rt;
    private float end_x = -3000;

    public void Shopping(bool open)
    {
        shopping = open;
    }

    private void Update()
    {
        if (shopping)
        {
            rt = play_menu.GetComponent<RectTransform>();

            rt.anchoredPosition -= (rt.anchoredPosition - new Vector2(end_x, 0)) * Time.deltaTime * coff;

            if(rt.anchoredPosition.x < -2997)
                rt.anchoredPosition = new Vector2(end_x, rt.anchoredPosition.y);
        }
        if (!shopping)
        {
            rt = play_menu.GetComponent<RectTransform>();

            rt.anchoredPosition -= rt.anchoredPosition * Time.deltaTime * coff;

            if (rt.anchoredPosition.x > -3)
                rt.anchoredPosition = new Vector2(0, rt.anchoredPosition.y);
        }
    }
}
