using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    public Sound [] sounds;
    public static AudioManager SharedInstance() {return instance;}
    
    // Start is called before the first frame update
    void Awake()
    {
        if(instance != null && instance != this) {
            Destroy(this.gameObject);
        } else {
            instance = this;
        }
        
        
        
        foreach(Sound s in sounds) {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }
    }
    public void PlaySound(string clip)
    {
        Sound s = Array.Find(sounds,sounds=>sounds.soundName==clip);
        s.source.PlayOneShot(s.clip);
        

    }
}
