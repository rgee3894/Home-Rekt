using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioClip shoot,enemyAttack,enemyDead,houseDead;
    public static AudioSource audioSource;
    
    // Start is called before the first frame update
    void Start()
    {
        shoot = Resources.Load<AudioClip>("Audio/Shoot");
        enemyAttack = Resources.Load<AudioClip>("Audio/Enemy Attack");
        enemyDead = Resources.Load<AudioClip>("Audio/Enemy Dead");
        houseDead = Resources.Load<AudioClip>("Audio/House Dead");

        audioSource = this.GetComponent<AudioSource>();

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PlaySound(string clip)
    {
        switch(clip)
        {
            case "shoot":
                audioSource.PlayOneShot(shoot);
                break;
            case "enemyAttack":
                audioSource.PlayOneShot(enemyAttack);
                break;
            case "enemyDead":
                audioSource.PlayOneShot(enemyDead);
                break;
            case "houseDead":
                audioSource.PlayOneShot(houseDead);
                break;
        }
    }
}
