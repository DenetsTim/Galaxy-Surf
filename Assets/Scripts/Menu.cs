using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private Text ship_name;
    [SerializeField] private Text ship_description;

    private GameObject player;
    private int num;
    private int selected_num = 0;
    private GameObject ship;

    private int max_num;
    private int max_score;

    [SerializeField] private Text max_score_txt;
    [SerializeField] private GameObject score_txt;

    [SerializeField] private GameObject select_btn;

    private void Start()
    {
        num = PlayerPrefs.GetInt("Num", -1);
        selected_num = num + 1;
        max_score = PlayerPrefs.GetInt("MaxScore", 0);
        max_score_txt.text = "Max score:\n" + max_score.ToString();

        showSkin(false);
    }

    public void startGame()
    {
        Player.player.Fly(selected_num);

        this.gameObject.SetActive(false);
        score_txt.SetActive(true);
    }

    public void showSkin(bool isLeft)
    {
        player = Player.player.gameObject;
        max_num = player.transform.childCount - 3;

        num += isLeft ? -1 : 1;
        num = num < 0 ? max_num : num;
        num %= max_num + 1;

        ship = player.transform.GetChild(num).gameObject;
        ship.SetActive(true);
        player.transform.GetChild(isLeft ? num == 2 ? 0 : num + 1 : num == 0 ? 2 : num - 1).gameObject.SetActive(false);

        ship_name.text = ship.GetComponent<ShipStats>().Name;
        ship_description.text = ship.GetComponent<ShipStats>().Description;

        select_btn.SetActive(max_score >= ship.GetComponent<ShipStats>().Price);
    }

    public void selectScin(bool isBack)
    {
        if (isBack)
        {
            selected_num = max_score >= ship.GetComponent<ShipStats>().Price ? num : selected_num;
            ship.SetActive(selected_num == num);
            ship = player.transform.GetChild(selected_num).gameObject;
            ship.SetActive(true);

            num = selected_num;
            ship_name.text = ship.GetComponent<ShipStats>().Name;
            ship_description.text = ship.GetComponent<ShipStats>().Description;
        }
        else
            selected_num = num;

        PlayerPrefs.SetInt("Num", selected_num - 1);
    }
}
