﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menu : MonoBehaviour
{
    public GameObject playButton, modifyButton, exitButton, CategoryMenu;

    private void Start()
    {
        playButton.SetActive(true);
        modifyButton.SetActive(true);
        exitButton.SetActive(true);
        CategoryMenu.SetActive(false);
    }
    public void PlayButton()
    {
        CategoryMenu.SetActive(true);
        playButton.SetActive(false);
        modifyButton.SetActive(false);
        exitButton.SetActive(false);
    }

   public void ModifyButton()
    {
        playButton.SetActive(false);
        modifyButton.SetActive(false);
        exitButton.SetActive(false);
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}