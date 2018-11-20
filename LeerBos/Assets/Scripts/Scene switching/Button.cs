using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button : SceneSwitchable
{
    public Sprite ImpactSprite;
    private Sprite DefaultSprite;
    private Image ButtonImage;

    private void Start()
    {
        ButtonImage = GetComponent<Image>();
        DefaultSprite = ButtonImage.sprite;
    }

    protected override void Click(Vector3 clickposition)
    {
        StartCoroutine(_Press());
        base.Click(clickposition);
    }

    IEnumerator _Press()
    {
        ButtonImage.sprite = ImpactSprite;
        yield return new WaitForSeconds(0.25f);
        ButtonImage.sprite = DefaultSprite;
    }

}
