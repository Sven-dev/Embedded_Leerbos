using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MainCard : MonoBehaviour, I_SmartwallInteractable {
    [SerializeField] private SceneController controller;
    [SerializeField] private GameObject Card_Back;

    public void Hit(Vector3 hitposition)
    {
        Debug.Log("Active?");
        if (Card_Back.activeSelf && controller.canReveal)
        {
            Debug.Log("Active?");
            StartCoroutine(Vals());
            controller.CardRevealed(this);
        }
    }
    private IEnumerator Vals()
    {
        transform.GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(0.5f);
        Card_Back.SetActive(false);

    }


    private int _id;
    public int id
    {
        get { return _id; }
    }


    public void ChangeSprite(int id, Sprite image)
    {
        _id = id;
        GetComponent<SpriteRenderer>().sprite = image;
    }

    public void Unreveal()
    {
        Card_Back.SetActive(true);
        transform.GetComponent<BoxCollider2D>().enabled = true;
    }
}
