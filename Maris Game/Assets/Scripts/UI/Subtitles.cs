using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Subtitles : MonoBehaviour
{
    public TextMeshProUGUI subtitleText;
    public float delay;
    
    public void SetSubtitle(string subtitle) {
        subtitleText.text = subtitle;
        StartCoroutine("Subtitle");
    } 

    IEnumerator Subtitle() {
        subtitleText.gameObject.transform.parent.gameObject.SetActive(true);
        yield return new WaitForSeconds(delay);
        subtitleText.gameObject.transform.parent.gameObject.SetActive(false);
    }
}
