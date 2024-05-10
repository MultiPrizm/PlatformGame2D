using UnityEngine;
using UnityEngine.UI;

public class SceneManagerMENU : MonoBehaviour
{
    [SerializeField] private string[] names;
    [SerializeField] private GameObject[] contexParts;
    [SerializeField] private int maxLevelsCount = 6;
    private int setPose = 1;
    private int LastLevel = 1;
   

    private void Start()
    {
        if(PlayerPrefs.GetInt("LevelNumber") > 1 && PlayerPrefs.GetInt("LevelNumber") <= maxLevelsCount) setPose = PlayerPrefs.GetInt("LevelNumber");
        if (PlayerPrefs.GetInt("LevelNumber") > maxLevelsCount) setPose = maxLevelsCount;
        setMenuLevel();
    }
    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.L))
        {
            PlayerPrefs.DeleteAll();
        }
        
    }
    private void setMenuLevel()
    {
        for (int i = 0; i < setPose; i++)
        {
            contexParts[i].transform.Find("Stage").transform.Find(names[0]).gameObject.SetActive(true);
            contexParts[i].transform.Find("Stage").transform.Find(names[1]).gameObject.SetActive(false);
            contexParts[i].transform.Find("Stage").transform.Find(names[2]).gameObject.SetActive(false);

            if (PlayerPrefs.GetInt("StarsOfLevelNumber" + i) > 0) { 
                for (int j = 0; j < PlayerPrefs.GetInt("StarsOfLevelNumber" + i); j++)
                {
                    Debug.Log(PlayerPrefs.GetInt("StarsOfLevelNumber" + i + "LEVEL"));
                    contexParts[i-1].transform.Find("Stage").transform.Find(names[0]).Find("Star").GetChild(j).GetComponent<Image>().color = new Color(255, 255, 255);
                }
            }
        }
        contexParts[setPose - 1].transform.Find("Stage").transform.Find(names[0]).gameObject.SetActive(false);
        contexParts[setPose - 1].transform.Find("Stage").transform.Find(names[1]).gameObject.SetActive(true);
        contexParts[setPose - 1].transform.Find("Stage").transform.Find(names[2]).gameObject.SetActive(false);
        //ShowStars
        
    }
    private void CheckSettingMenuLevel()
    {

    }
}