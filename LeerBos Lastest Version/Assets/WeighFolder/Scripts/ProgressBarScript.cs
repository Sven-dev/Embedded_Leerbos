using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarScript : MonoBehaviour
{
    //wow this script is really short
    //maybe didnt need its own script

    public Image progressBar;

    public void SetProgress(float amount)
    {
        progressBar.fillAmount = amount;
    }
}
