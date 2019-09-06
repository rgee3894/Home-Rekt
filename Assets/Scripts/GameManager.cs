using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject house;

    public GameObject player;
    public GameObject announcerPanel; 

    private static GameManager instance;

    public static GameManager SharedInstance() { return instance;} 

    void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    void Start()
    {
        Time.timeScale=1f;
    }
   

    // Update is called once per frame
    void Update()
    {

        if(player == null)
        {
            GameOver();
        }
        
        
    }

    private void GameOver()
    {
        SetChildrenActive(announcerPanel.transform,true);
        this.GetComponent<Spawner>().keepSpawning=false;
        GameObject announcer = announcerPanel.transform.Find("Announcer").gameObject;
        announcer.GetComponent<TextMeshProUGUI>().SetText("GAME OVER");
        Time.timeScale=0f;
    }

    public void Quit()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void Restart()
    {
        Time.timeScale=1f;
        Scene scene = SceneManager.GetActiveScene(); 
        SceneManager.LoadScene(scene.name);
    }

    public IEnumerator AnnounceWave(int num)
    {
        GameObject announcer = announcerPanel.transform.Find("Announcer").gameObject;
        announcer.GetComponent<TextMeshProUGUI>().SetText("Wave " + (num+1).ToString());
        SetChildrenActive(announcer.transform,true);
        yield return new WaitForSeconds(2f);
        SetChildrenActive(announcer.transform,false);
        yield return null;
    }

    public void SetChildrenActive(Transform obj, bool flag)
    {
        obj.gameObject.SetActive(flag);
        foreach(Transform child in obj)
        {
            child.gameObject.SetActive(flag);
            SetChildrenActive(child,flag);
        }

    }
}
