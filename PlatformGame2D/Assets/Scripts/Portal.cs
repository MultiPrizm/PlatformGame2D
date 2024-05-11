using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [SerializeField] private GameObject WinPanel;
    [SerializeField] private bool BossLevel = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            WinPanel.SetActive(true);
            collision.gameObject.GetComponent<PlayerScript>().Move(false);
            if(PlayerPrefs.GetInt("StarsOfLevelNumber" + SceneManager.GetActiveScene().buildIndex) < collision.gameObject.GetComponent<PlayerScript>().GetStars() && BossLevel == false)
                PlayerPrefs.SetInt("StarsOfLevelNumber" + SceneManager.GetActiveScene().buildIndex, collision.gameObject.GetComponent<PlayerScript>().GetStars());
            else if (BossLevel == true) PlayerPrefs.SetInt("StarsOfLevelNumber" + SceneManager.GetActiveScene().buildIndex, 3); 
        }
    }
}
