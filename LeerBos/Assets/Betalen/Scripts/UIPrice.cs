using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPrice : MonoBehaviour
{
    private Text PriceLabel;
    private AudioSource Audio;

    private void Start()
    {
        PriceLabel = transform.GetChild(0).GetComponent<Text>();
        Audio = GetComponent<AudioSource>();
        Register.OnPriceChange += UpdateUI;
    }

    private void UpdateUI(double price)
    {
        StartCoroutine(_UpdateUI(price));
    }

    //Grows the text field, displays a new price, and shrinks it again.
    IEnumerator _UpdateUI(double price)
    {
        while (transform.localScale.x < 0.5f)
        {
            transform.Translate(Vector3.down * Time.deltaTime);
            transform.localScale += Vector3.one / 5 * Time.deltaTime;
            yield return null;
        }

        transform.localScale.Set(0.5f, 0.5f, 1);

        yield return new WaitForSeconds(0.2f);
        Audio.Play();
        PriceLabel.text = "€" + price.ToString("0.00");
        PriceLabel.text = PriceLabel.text.Replace(".", ",");
        yield return new WaitForSeconds(1);

        while (transform.localScale.x > 0.25f)
        {
            transform.Translate(Vector3.up * Time.deltaTime);
            transform.localScale -= Vector3.one / 5 * Time.deltaTime;
            yield return null;
        }

        transform.localPosition = new Vector3(transform.localPosition.x, 0, transform.localPosition.z);
        transform.localScale.Set(0.25f, 0.25f, 1);
    }

    private void OnDestroy()
    {
        Register.OnPriceChange -= UpdateUI;
    }
}