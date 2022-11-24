using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControlMenu : MonoBehaviour
{
    [Header("�Ͻ� ���� ����")]
    public GameObject pauseImg;
    public RectTransform pauseMenu;
    public GameObject Player;

    [Header("���� ���� ����")]
    public RectTransform soundMenu;
    public RectTransform screenMenu;
    
    void Awake()
    {
        pauseImg = GameObject.Find("Canvas").transform.GetChild(3).gameObject;
        pauseMenu = pauseImg.transform.GetChild(0).GetComponent<RectTransform>();
        soundMenu = pauseImg.transform.GetChild(1).GetComponent<RectTransform>();
        screenMenu = pauseImg.transform.GetChild(2).GetComponent<RectTransform>();
        Player = GameObject.FindGameObjectWithTag("Player").gameObject;
    }

    void Update()
    {
        if(Application.platform == RuntimePlatform.Android)
        {
            // ����Ͽ����� �ڷΰ��� ��ư
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Pause();
            }
        }
        if(Application.platform == RuntimePlatform.WindowsEditor)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Pause();
            }
        }
    }

    public void Sound(bool isOpen)
    {
        if (isOpen)
        {
            soundMenu.gameObject.SetActive(true);
            screenMenu.gameObject.SetActive(false);
            pauseMenu.gameObject.SetActive(false);
        }
            
        else
        {
            soundMenu.gameObject.SetActive(false);
            screenMenu.gameObject.SetActive(false);
            pauseMenu.gameObject.SetActive(true);
        }
    }

    public void Pause()
    {
        // ĵ���� �ȿ� RectTransform Ȱ��ȭ ����
        if(pauseImg.gameObject.activeInHierarchy == false)
        {
            if(pauseMenu.gameObject.activeInHierarchy == false)
            {
                pauseMenu.gameObject.SetActive(true);
            }
            pauseImg.SetActive(true);
            Time.timeScale = 0f;
            Player.SetActive(false);
        }
        else
        {
            pauseImg.SetActive(false);
            screenMenu.gameObject.SetActive(false);
            soundMenu.gameObject.SetActive(false);
            Time.timeScale = 1f;
            Player.SetActive(true);
        }
    }
    public void ScreenSetining(bool isOpen)
    {
        if (isOpen)
        {
            screenMenu.gameObject.SetActive(true);
            soundMenu.gameObject.SetActive(false);
            pauseMenu.gameObject.SetActive(false);
        }

        else
        {
            screenMenu.gameObject.SetActive(false);
            soundMenu.gameObject.SetActive(false);
            pauseMenu.gameObject.SetActive(true);
        }
    }
}
