using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour
{
    public List<SpriteRenderer> Sprites = new List<SpriteRenderer>();

    public float speed = 0.3f;

    private Color rainbowColor;
    private Color setColor;
    private bool colorReady = false;

    public Stats stats;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {/*
        if(stats == null)
        {
            stats = gameObject.GetComponent<Stats>();
        }*/
        if(Sprites.Count >= 0)
        {
            if (stats.shiny)
            {
                rainbowColor = HSBColor.ToColor(new HSBColor(Mathf.PingPong(Time.time * speed, 1), 1, 1));
                foreach (SpriteRenderer sprite in Sprites)
                {
                    sprite.material.SetColor("_Color", rainbowColor);
                }
            }
            else if (colorReady)
            {
                foreach (SpriteRenderer sprite in Sprites)
                {
                    sprite.material.SetColor("_Color", setColor);
                }
            }
        }
    }

    public void SetColor(Color _color)
    {
        setColor = _color;
        colorReady = true;
    }
}
