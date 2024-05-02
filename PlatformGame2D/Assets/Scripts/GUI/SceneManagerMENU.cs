using UnityEngine;

public class SceneManagerMENU : MonoBehaviour
{
    [SerializeField] private string[] names;
    [SerializeField] private GameObject[] contexParts;
    [SerializeField] private int maxLevelsCount = 6;
    private int setPose = 1;
    public int LastLevel = 1;
   

    private void Start()
    {
        if(PlayerPrefs.GetInt("LevelNumber") > 1 && PlayerPrefs.GetInt("LevelNumber") <= maxLevelsCount) setPose = PlayerPrefs.GetInt("LevelNumber");
        if (PlayerPrefs.GetInt("LevelNumber") > maxLevelsCount) setPose = maxLevelsCount;
        setMenuLevel();
    }

    private void setMenuLevel()
    {
        for (int i = 0; i < setPose; i++)
        {
            contexParts[i].transform.Find("Stage").transform.Find(names[0]).gameObject.SetActive(true);
            contexParts[i].transform.Find("Stage").transform.Find(names[1]).gameObject.SetActive(false);
            contexParts[i].transform.Find("Stage").transform.Find(names[2]).gameObject.SetActive(false);
        }
        contexParts[setPose - 1].transform.Find("Stage").transform.Find(names[0]).gameObject.SetActive(false);
        contexParts[setPose - 1].transform.Find("Stage").transform.Find(names[1]).gameObject.SetActive(true);
        contexParts[setPose - 1].transform.Find("Stage").transform.Find(names[2]).gameObject.SetActive(false);
    }
    private void CheckSettingMenuLevel()
    {

    }
}