using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuarterFeedbackScript : MonoBehaviour {

    private Image img;
    private int coroutineId = 0;
    private Color defaultColor;

	// Use this for initialization
	void Start () {
        img = GetComponent<Image>();
        defaultColor = img.color;
    }

    public void GiveFeedback(Color feedbackColor)
    {
        coroutineId++;
        StartCoroutine(_giveFeedback(feedbackColor,coroutineId));
    }

    IEnumerator _giveFeedback(Color feedbackColor,int id)
    {
        float i = 0;

        //lerp to public colour, then return to default
        while (img.color != feedbackColor && coroutineId == id)
        {
            i = i + 0.1f;
            img.color = Color.Lerp(defaultColor, feedbackColor, i);
            yield return null;
        }
        while (img.color != defaultColor && coroutineId == id)
        {
            i = i - 0.1f;
            img.color = Color.Lerp(defaultColor, feedbackColor, i);
            yield return null;
        }
    }
}
