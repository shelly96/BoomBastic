using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_heart : MonoBehaviour
{

    [SerializeField] public bool on = true;

    public Sprite onSprite;
    public Sprite offSprite;

    private SpriteRenderer sp;

    private void Start()
    {
        sp = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (on)
        {
            sp.sprite = onSprite;
        }
        else {
            sp.sprite = offSprite;
        }
        
    }
}
