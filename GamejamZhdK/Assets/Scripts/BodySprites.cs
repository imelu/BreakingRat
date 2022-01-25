using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodySprites : MonoBehaviour
{
    private ColorManager colorManager;

    [SerializeField] private SpriteRenderer arm1;
    [SerializeField] private SpriteRenderer arm2;
    [SerializeField] private SpriteRenderer leg1;
    [SerializeField] private SpriteRenderer leg2;
    [SerializeField] private SpriteRenderer tail;

    void Start()
    {
        colorManager = GetComponentInParent<ColorManager>();
        colorManager.Sprites.Add(arm1);
        colorManager.Sprites.Add(arm2);
        colorManager.Sprites.Add(leg1);
        colorManager.Sprites.Add(leg2);
        colorManager.Sprites.Add(tail);
        colorManager.Sprites.Add(GetComponent<SpriteRenderer>());
    }

    public void ChangeSprites(Sprite _arm1, Sprite _arm2, Sprite _leg1, Sprite _leg2, Sprite _tail)
    {
        arm1.sprite = _arm1;
        arm2.sprite = _arm2;
        leg1.sprite = _leg1;
        leg2.sprite = _leg2;
        tail.sprite = _tail;
    }
}
