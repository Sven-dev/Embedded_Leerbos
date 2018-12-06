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
    private AudioSource aSource;
    
	void Start () {
        aSource = GetComponent<AudioSource>();
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


        //fucking math genius
        float clipLength = aSource.clip.length;
        aSource.Play();
        float timePassed = 0;
        float currentAmount = 0;
        while (progressImg.fillAmount < targetFill)
        {
            progressImg.fillAmount = Mathf.Lerp(0, targetFill, currentAmount);
            timePassed += Time.deltaTime;
            currentAmount = timePassed / aSource.clip.length;
            yield return null;
        }
        progressImg.fillAmount = targetFill;
        yield return new WaitForSeconds(0.5f);
        ButtonToShow.FadeIn();
    }
}
