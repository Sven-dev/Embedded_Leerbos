using System.Collections;
using UnityEngine;
using UnityEngine.UI;

//Updates the price label whenever the price in a Register changes
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
        #region Grow the label
        while (transform.localScale.x < 0.5f)
        {
            transform.Translate(Vector3.down * 2 * Time.deltaTime);
            transform.localScale += Vector3.one / 2.5f * Time.deltaTime;
            yield return null;
        }

        transform.localScale.Set(0.5f, 0.5f, 1);
        #endregion

        #region Display new price
        yield return new WaitForSeconds(0.2f);
        Audio.Play();
        PriceLabel.text = "€" + price.ToString("0.00");
        PriceLabel.text = PriceLabel.text.Replace(".", ",");

        //Make it move a little
        PriceLabel.transform.localScale += Vector3.one * 0.25f;
        yield return new WaitForSeconds(0.1f);
        PriceLabel.transform.localScale -= Vector3.one * 0.25f;
        yield return new WaitForSeconds(0.9f);
        #endregion

        #region Shrink the label
        while (transform.localScale.x > 0.25f)
        {
            transform.Translate(Vector3.up * 2 * Time.deltaTime);
            transform.localScale -= Vector3.one / 2.5f * Time.deltaTime;
            yield return null;
        }

        //Make sure the label is set to the correct location & price
        transform.localPosition = new Vector3(transform.localPosition.x, 0, transform.localPosition.z);
        transform.localScale.Set(0.25f, 0.25f, 1);
        #endregion
    }

    /// <summary>
    /// Unsubscribes from the OnPriceChange event
    /// needs to happen because Unity doesn't unsubscribe from raw C# events & will give a NullRefrenceException on reloading the scene
    /// </summary>
    private void OnDestroy()
    {
        Register.OnPriceChange -= UpdateUI;
    }
}