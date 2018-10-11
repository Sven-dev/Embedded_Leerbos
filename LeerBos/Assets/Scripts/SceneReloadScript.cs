using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneReloadScript : Interactable
{
    protected override void Click(Vector3 clickposition)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}