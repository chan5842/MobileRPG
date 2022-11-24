using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuOption : MonoBehaviour
{
    public GameObject directionalLight;
    public Transform dropdown;

    void Start()
    {
        
    }

    void Update()
    {
        if (dropdown.GetComponent<Dropdown>().value == 0)
        {
            directionalLight.GetComponent<Light>().shadows = LightShadows.Soft;
        }
        if (dropdown.GetComponent<Dropdown>().value == 1)
        {
            directionalLight.GetComponent<Light>().shadows = LightShadows.Hard;
        }
        if (dropdown.GetComponent<Dropdown>().value == 2)
        {
            directionalLight.GetComponent<Light>().shadows = LightShadows.None;
        }
    }
}
