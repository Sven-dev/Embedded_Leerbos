using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneSwitcher : MonoBehaviour
{
    public float FadeSpeed;

    private Camera Camera;
    private Image Transition;
    private string TargetScene;

    private void Start()
    {
        Camera = GetComponent<Camera>();
        Transition = transform.GetChild(0).GetChild(1).GetComponent<Image>();
        StartCoroutine(_FadeIn());
    }

    public void Switch(Object target, Sprite image)
    {
        TargetScene = target.name;
        Transition.sprite = image;

        StartCoroutine(_FadeOut());
    }

    public void Switch(string target, Sprite image)
    {
        TargetScene = target;
        Transition.sprite = image;

        StartCoroutine(_FadeOut());
    }

    IEnumerator _FadeIn()
    {
        Transition.color = new Color(Transition.color.r, Transition.color.g, Transition.color.b, 1);
        while (Transition.color.a > 0)
        {
            Transition.color = new Color(Transition.color.r, Transition.color.g, Transition.color.b, Transition.color.a - Time.deltaTime * FadeSpeed);
            yield return null;
        }
    }

    IEnumerator _FadeOut()
    {
        Transition.color = new Color(Transition.color.r, Transition.color.g, Transition.color.b, 0);
        while (Transition.color.a < 1)
        {
            Transition.color = new Color(Transition.color.r, Transition.color.g, Transition.color.b, Transition.color.a + Time.deltaTime * FadeSpeed);
            yield return null;
        }

        SceneManager.LoadScene(TargetScene);
    }
}