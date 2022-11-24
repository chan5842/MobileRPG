using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinimapCursorOnOff : MonoBehaviour
{
    Image PanelImg;
    float timePrev;

    void Start()
    {
        PanelImg = GetComponent<Image>();
        timePrev = Time.time;
    }

    void Update()
    {
        if (Time.time - timePrev > 0.3f)
        {
            timePrev = Time.time;
            PanelImg.enabled = !PanelImg.enabled;
        }
    }
}
