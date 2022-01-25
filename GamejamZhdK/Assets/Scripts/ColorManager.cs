using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour
{
    public List<SpriteRenderer> Sprites = new List<SpriteRenderer>();

    public float speed = 0.3f;

    private Color rainbowColor;

    private Stats stats;

    // Start is called before the first frame update
    void Start()
    {
        stats = GetComponent<Stats>();
    }

    // Update is called once per frame
    void Update()
    {
        if (stats.shiny)
        {
            rainbowColor = HSBColor.ToColor(new HSBColor(Mathf.PingPong(Time.time * speed, 1), 1, 1));
            foreach (SpriteRenderer sprite in Sprites)
            {
                sprite.material.SetColor("_Color", rainbowColor);
            }
        }
    }

    public void SetColor(Color _color)
    {
        if (!stats.shiny)
        {
            
        }
    }
}
