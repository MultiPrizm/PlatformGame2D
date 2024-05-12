using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneMenegerSCR : MonoBehaviour
{
    
    [SerializeField] Animator EndScreenAnimator;
    public void SetScene()
    {
        if(PlayerPrefs.GetInt("LevelNumber") < SceneManager.GetActiveScene().buildIndex + 1) PlayerPrefs.SetInt("LevelNumber", SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void SceneLouder(int SceneNuber)
    {
        SceneManager.LoadScene(SceneNuber);
    }
    public void SceneLouderNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void SceneLouderRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void SceneAnimEnd()
    {
        if (EndScreenAnimator != null) EndScreenAnimator.SetTrigger("SetEnd");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
