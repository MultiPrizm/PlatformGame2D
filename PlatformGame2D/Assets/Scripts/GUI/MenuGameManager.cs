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
    [SerializeField] private int BlackStarsStars = 2;
    //Dead Menu
    [SerializeField] private GameObject LoseMenu;
    private bool LoseSetter = false;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            ShowWinMenu();
        }
        if (Input.GetKeyDown(KeyCode.G))
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
        for (int i = 0; i < BlackStarsStars; i++)
        {
            Stars[i].GetComponent<Image>().color = Color.black;
        }
        WinSetter = !WinSetter;
        WinMenu.SetActive(WinSetter);
    }
    private void ShowLoseMenu()
    {
        LoseSetter = !LoseSetter;
        LoseMenu.SetActive(LoseSetter);
    }
}
