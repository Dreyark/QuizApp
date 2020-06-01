using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class menu : MonoBehaviour
{
    public GameObject playButton, AddQuestionButton, AddCategoryButton, exitButton, CategoryMenu, scoreboard, quiz, LoginButton, LoginScreen, Register, RegButton;
    public GameObject AddQuestionLayout, AddCategoryLayout;
    public string Uzytkownik;

    public void Start()
    {
        LoginButton.SetActive(true);
        LoginScreen.SetActive(true);
        Register.SetActive(false);
        RegButton.SetActive(true);
        AddQuestionLayout.SetActive(false);
        AddCategoryLayout.SetActive(false);
    }

    public void Menu()
    {
        playButton.SetActive(true);
        AddQuestionButton.SetActive(true);
        AddCategoryButton.SetActive(true);
        exitButton.SetActive(true);
        CategoryMenu.SetActive(false);
        quiz.SetActive(false);
        scoreboard.SetActive(false);
        AddQuestionLayout.SetActive(false);
        AddCategoryLayout.SetActive(false);
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
        AddQuestionButton.SetActive(false);
        AddCategoryButton.SetActive(false);
        exitButton.SetActive(false);
        quiz.SetActive(false);
        AddQuestionLayout.SetActive(false);
        AddCategoryLayout.SetActive(false);
    }

    public void ModifyButton()
    {
        playButton.SetActive(false);
        AddQuestionButton.SetActive(false);
        AddCategoryButton.SetActive(false);
        exitButton.SetActive(false);
        quiz.SetActive(false);
    }

    public void ScoreboardButton()
    {
        playButton.SetActive(false);
        AddQuestionButton.SetActive(false);
        AddCategoryButton.SetActive(false);
        exitButton.SetActive(false);
        CategoryMenu.SetActive(false);
        quiz.SetActive(false);
        scoreboard.SetActive(true);
        AddQuestionLayout.SetActive(false);
        AddCategoryLayout.SetActive(false);
    }

    public void ExitButton()
    {
        Application.Quit();
    }


}
