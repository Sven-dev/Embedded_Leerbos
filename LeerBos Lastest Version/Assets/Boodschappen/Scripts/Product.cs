using UnityEngine;
using UnityEngine.UI;

public class Product : MonoBehaviour, I_SmartwallInteractable
{
    private Rigidbody2D Rigidbody;
    private Collider2D Collider;
    private bool IncreasedGravity;

    private Text Label;

    [HideInInspector]
    public AudioSource NameAudio;
    private AudioSource HitAudio;

    public string ProductName
    {
        get { return Label.text; }
        set { Label.text = value; }
    }

	// Use this for initialization
	void Awake ()
    {
        Label = transform.GetChild(0).GetComponent<Text>();
        Rigidbody = GetComponent<Rigidbody2D>();
        Collider = GetComponent<Collider2D>();

        AudioSource[] audios = GetComponents<AudioSource>();
        NameAudio = audios[0];
        HitAudio = audios[1];

        IncreasedGravity = false;
	}

    public void FallInCart(Kart k)
    {
        Destroy(Rigidbody);
        Destroy(Collider);
        Destroy(HitAudio);
        transform.SetParent(k.ProductHolder);
    }

    public void SetProduct(string name)
    {
        ProductName = name;
    }

    private void IncreaseGravity(int amount)
    {
        if (!IncreasedGravity)
        {
            Rigidbody.gravityScale *= amount;
            IncreasedGravity = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        IncreaseGravity(4);
    }

    public void Hit(Vector3 clickposition)
    {
        IncreaseGravity(50);
        if (HitAudio != null)
        {
            HitAudio.Play();
        }
    }
}