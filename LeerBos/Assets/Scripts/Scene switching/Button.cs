using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button : SceneSwitchable
{
    public Sprite ImpactSprite;
    private Sprite DefaultSprite;
    private Image ButtonImage;
    private Image IconImage;
    private Transform Icon;
    private bool Pressed;

    private void Awake()
    {
        ButtonImage = GetComponent<Image>();
        DefaultSprite = ButtonImage.sprite;
        Icon = transform.GetChild(0);
        IconImage = Icon.GetComponent<Image>();
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

    public void Appear()
    {
        gameObject.SetActive(true);
        StartCoroutine(_FadeIn());
    }

    IEnumerator _FadeIn()
    {
        Color color1 = ButtonImage.color;
        Color color2 = IconImage.color;

        while (ButtonImage.color.a < 1)
        {
            color1.a += 1f * Time.deltaTime;
            color2.a += 1f * Time.deltaTime;
            ButtonImage.color = color1;
            IconImage.color = color2;
            yield return null;
        }
    }
}