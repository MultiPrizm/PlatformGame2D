using UnityEngine;

public class SceneManagerMENU : MonoBehaviour
{
    [SerializeField] private string[] names;
    [SerializeField] private GameObject[] contexParts;
    public int setPose = 3;
    public int LastLevel = 1;

    
    private void Start()
    {
        setMenuLevel();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            for (int i = 0; i < contexParts.Length; i++)
            {
                contexParts[i].transform.Find("Stage").transform.Find(names[0]).gameObject.SetActive(false);
                contexParts[i].transform.Find("Stage").transform.Find(names[1]).gameObject.SetActive(false);
                contexParts[i].transform.Find("Stage").transform.Find(names[2]).gameObject.SetActive(true);
            }
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