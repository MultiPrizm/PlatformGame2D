using UnityEngine;
using UnityEngine.UI;

public class MenuGameManager : MonoBehaviour
{
    //Pouse Menu
    [SerializeField] private GameObject PauseMenu;
    private bool PauseSetter = false;
    //Win Menu
    [SerializeField] private GameObject WinMenu;
    private bool WinSetter = false;
    [SerializeField] private GameObject[] Stars;
    [SerializeField] private GameObject[] FXStars;
    private int BlackStarsStars = 3;
    
    //Dead Menu
    [SerializeField] private GameObject LoseMenu;
    private bool LoseSetter = false;

    //[Header("Player")]
    private GameObject Player;
    private void Start()
    {

        Player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        ShowWinMenu();
        if (Input.GetKeyDown(KeyCode.G))
        {
            ShowLoseMenu();
        }
    }
    private void LateUpdate()
    {
        if (Player.GetComponent<PlayerScript>().IsLivePlayer() == false)
        {
            ShowLoseMenu();
        }
    }
    public void PouseButton()
    {
        PauseSetter = !PauseSetter;
        PauseMenu.SetActive(PauseSetter);
    }
    private void ShowWinMenu()
    {

        for (int i = 0; i < Player.GetComponent<PlayerScript>().GetStars(); i++)
        {
            Stars[i].GetComponent<Image>().color = new Color(255,255,255);
        }
        for (int i = 0; i < Player.GetComponent<PlayerScript>().GetStars(); i++)
        {
            FXStars[i].SetActive(true);
        }
        //WinSetter = !WinSetter;
        //WinMenu.SetActive(WinSetter);
    }
    private void ShowLoseMenu()
    {
        LoseMenu.SetActive(true);
    }
}
