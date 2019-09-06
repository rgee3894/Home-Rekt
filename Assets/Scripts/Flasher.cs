using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flasher : MonoBehaviour
{
    public float flashTime=0.25f;
    private Color [] originalColors;
    private SpriteRenderer[] spriteRenderers;

    public Color flashColor;//Color that will flash
    void Start()
    {
        spriteRenderers = this.GetComponentsInChildren<SpriteRenderer>(true);
        originalColors = new Color[spriteRenderers.Length];

        int i = 0;
        foreach(SpriteRenderer sr in spriteRenderers)
        {
            originalColors[i]=sr.color;
            i++;
        }
        
        
    }
    public void Flash()
    {
        foreach(SpriteRenderer sr in spriteRenderers)
        {
            sr.color = flashColor; 
        }
        Invoke("ResetColor", flashTime);
        
    }
    void ResetColor()
    {
        for(int i = 0; i < spriteRenderers.Length; i++)
        {
            spriteRenderers[i].color = originalColors[i];
        }
    }  
}
