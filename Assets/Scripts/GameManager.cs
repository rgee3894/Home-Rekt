using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject house;

    public GameObject player;
    public GameObject announcerPanel; 
   

    // Update is called once per frame
    void Update()
    {
        if(house == null)
        {
            announcerPanel.SetActive(true);
            this.GetComponent<Spawner>().keepSpawning=false;
            player.GetComponent<PlayerMovement>().canMove=false;
            player.GetComponent<PlayerShooting>().canShoot=false;
        }
        
    }

    public void Quit()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void Restart()
    {
        Scene scene = SceneManager.GetActiveScene(); 
        SceneManager.LoadScene(scene.name);
    }
}
