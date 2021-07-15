using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_score : MonoBehaviour
{
    [SerializeField] private float speed = 0.75f;
    [SerializeField] private float fadingSpeed = 0.0085f;
    public SpriteRenderer spriteRenderer;

    private float spriteAlpha = 1f;


    // Update is called once per frame
    void Update()
    {

        // Alpha
        spriteAlpha -= fadingSpeed;

        Color currentColor = spriteRenderer.color;
        currentColor = new Color(currentColor.r, currentColor.g, currentColor.b, spriteAlpha);

        spriteRenderer.color = currentColor;

        // Y

        transform.Translate(Vector3.up * speed * Time.deltaTime);

        if (spriteAlpha <= 0.05) {
           Destroy(this.gameObject);
        }
        
    }
}
