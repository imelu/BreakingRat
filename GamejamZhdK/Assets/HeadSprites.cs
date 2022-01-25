using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadSprites : MonoBehaviour
{
    [SerializeField] private SpriteRenderer eye1;
    [SerializeField] private SpriteRenderer eye2;
    [SerializeField] private SpriteRenderer ear1;
    [SerializeField] private SpriteRenderer ear2;
    [SerializeField] private SpriteRenderer mouth;

    public void ChangeSprites(Sprite _eye1, Sprite _eye2, Sprite _ear1, Sprite _ear2, Sprite _mouth)
    {
        eye1.sprite = _eye1;
        eye2.sprite = _eye2;
        ear1.sprite = _ear1;
        ear2.sprite = _ear2;
        mouth.sprite = _mouth;
    }
}