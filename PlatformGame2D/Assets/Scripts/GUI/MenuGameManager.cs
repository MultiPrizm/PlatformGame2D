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
    [SerializeField] private Image[] Stars;
    [SerializeField] private GameObject[] FXStars;
    private int BlackStarsStars = 3;
    
    //Dead Menu
    [SerializeField] private GameObject LoseMenu;
    private bool LoseSetter = false;

    //[Header("Player")]
    private GameObject Player;
    private PlayerScript _PlayerScript;
    [Header("Set level boss or defoult")]
    [SerializeField] private bool BossLevel = false;

    [Header("Hearts panel")]
    [SerializeField] private Image[] HeartIcons;

    private int HeartsCount = 1;
    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        _PlayerScript = Player.GetComponent<PlayerScript>();
    }
    private void Update()
    {
        
        ShowWinMenu();
        if (Input.GetKeyDown(KeyCode.G))
        {
            ShowLoseMenu();
        }
        for(int i = 0; i<HeartIcons.Length; i++)
        {
            if(i < _PlayerScript.GetHealth())
            {
                HeartIcons[i].color = new Color(255, 255, 255);
                //HeartIcons[i].SetActive(true);
            }
            else
            {
                HeartIcons[i].color = new Color(0, 0, 0);
                //HeartIcons[i].SetActive(false);
            }
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
        if(BossLevel == false) { 
            for (int i = 0; i < _PlayerScript.GetStars(); i++)
            {
                Stars[i].color = new Color(255,255,255);
            }
            for (int i = 0; i < _PlayerScript.GetStars(); i++)
            {
                FXStars[i].SetActive(true);
            }
        }
        if (BossLevel == true)
        {
            for (int i = 0; i < 3; i++)
            {
                Stars[i].color = new Color(255, 255, 255);
            }
            for (int i = 0; i < 3; i++)
            {
                FXStars[i].SetActive(true);
            }
        }
        //WinSetter = !WinSetter;
        //WinMenu.SetActive(WinSetter);
    }
    private void ShowLoseMenu()
    {
        LoseMenu.SetActive(true);
    }
}
