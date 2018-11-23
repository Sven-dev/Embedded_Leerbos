using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button : SceneSwitchable
{
    public Sprite ImpactSprite;
    private Sprite DefaultSprite;
    private Image ButtonImage;
    private Transform Icon;
    private bool Pressed;

    private void Start()
    {
        ButtonImage = GetComponent<Image>();
        DefaultSprite = ButtonImage.sprite;
        Icon = transform.GetChild(0);
        Pressed = false;
    }

    protected override void Click(Vector3 clickposition)
    {
        StartCoroutine(_Press());
        if (!Pressed)
        {
            Pressed = true;
            base.Click(clickposition);
        }
    }

    IEnumerator _Press()
    {
        ButtonImage.sprite = ImpactSprite;
        Icon.localPosition = new Vector2(Icon.localPosition.x - 10, Icon.localPosition.y - 10);
        yield return new WaitForSeconds(.5f);
        ButtonImage.sprite = DefaultSprite;
        Icon.localPosition = new Vector2(Icon.localPosition.x + 10, Icon.localPosition.y + 10);
    }
}