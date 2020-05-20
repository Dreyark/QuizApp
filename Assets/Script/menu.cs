using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class menu : MonoBehaviour
{
    public GameObject playButton, modifyButton, exitButton, CategoryMenu, scoreboard, quiz;
    public string Uzytkownik;

    public void Menu()
    {
        playButton.SetActive(true);
        modifyButton.SetActive(true);
        exitButton.SetActive(true);
        CategoryMenu.SetActive(false);
        quiz.SetActive(false);
        scoreboard.SetActive(false);
    }
    public void LoggedIn(string user)
    {
        Uzytkownik = user;
        Menu();
    }
    public void PlayButton()
    {
        CategoryMenu.SetActive(true);
        playButton.SetActive(false);
        modifyButton.SetActive(false);
        exitButton.SetActive(false);
        quiz.SetActive(false);
    }

   public void ModifyButton()
    {
        playButton.SetActive(false);
        modifyButton.SetActive(false);
        exitButton.SetActive(false);
        quiz.SetActive(false);
    }

    public void ScoreboardButton()
    {
        playButton.SetActive(false);
        modifyButton.SetActive(false);
        exitButton.SetActive(false);
        CategoryMenu.SetActive(false);
        quiz.SetActive(false);
        scoreboard.SetActive(true);
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
