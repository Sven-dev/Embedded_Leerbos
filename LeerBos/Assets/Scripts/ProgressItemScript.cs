using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressItemScript : MonoBehaviour {

    public Image progressImg;
    public float FillSpeed;
    public Button ButtonToShow;
    private bool move;
    private Vector3 targetPos;
    
	void Start () {
        targetPos = transform.position;
        transform.position = new Vector3(0, 10, 0);
	}

    public void Show(float targetFill)
    {
        StartCoroutine(_Show(targetFill));
    }

    private IEnumerator _Show(float targetFill)
    {
        yield return new WaitForSeconds(1);

        while (transform.position.y > targetPos.y)
        {
            transform.Translate(Vector3.down * 5 * Time.deltaTime);
            yield return null;
        }
        yield return new WaitForSeconds(1);

        while (progressImg.fillAmount < targetFill)
        {
            progressImg.fillAmount += FillSpeed * Time.deltaTime;
            yield return null;
        }
        progressImg.fillAmount = targetFill;
        yield return new WaitForSeconds(0.5f);
        ButtonToShow.FadeIn();
    }
}
