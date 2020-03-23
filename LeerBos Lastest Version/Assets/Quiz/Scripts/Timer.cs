using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMesh _text;
    [SerializeField] private SceneController Einde;
    float Tijd;
    public float Snelheid = 1;
    bool playing = true;
    // Use this for initialization
    void Start()
    {
        //_text = GetComponent<TextMesh.text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playing == true)
        {
            Tijd += Time.deltaTime * Snelheid;
            string Seconden = (Tijd % 60).ToString("0");
            _text.text = "Sec: " + Seconden;
        }
    }

    void StopPlaying()
    {
        playing = false;
    }
}