using System;
using Proyecto26;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class GameController : MonoBehaviour
{
    public static string playerName;
    public InputField nameText;
    public Button fight;
    public Canvas main;
    public Canvas choiceMenu;
    public Text error;
    public Text gameName;
    public Text figure;
    public Button rock;
    public Button paper;
    public Button scissors;
    public Button lizard;
    public Button spok;
    public Button end;
    public  static string playerChoice;
    enum Figures
    {
        Rock,
        Paper,
        Scissors,
        Lizard,
        Spok
    }
    void Start()
    {

    }

    public void OnSubmit()
    {
        if (nameText.text == "")
        {
            error.gameObject.SetActive(true);

        }
        else
        {
            playerName = nameText.text;
            error.gameObject.SetActive(false);
            main.gameObject.SetActive(false);
            choiceMenu.gameObject.SetActive(true);
            gameName.text += playerName;
        }
    }

    private void SendToDatabase()
    {
        User user = new User();
        RestClient.Post("https://rockpapers-466e3.firebaseio.com/.json", user);
    }

    public void GameFlow()
    {
        Random bot = new Random() ;
        int botChoice = bot.Next(0,4);
        switch (botChoice)
        {
            case 0:
                if (playerChoice == "p" || playerChoice == "sp")
                    figure.text += "\nВы проиграли :'(";
                else if (playerChoice == "r")
                    figure.text += "\nПолучилась ничья :|";
                else
                    figure.text += "\nВы победили!!! >:)";
                break;
            case 1:
                if (playerChoice == "sc" || playerChoice == "l")
                    figure.text += "\nВы проиграли :'(";
                else if (playerChoice == "p")
                    figure.text += "\nПолучилась ничья :|";
                else
                    figure.text += "\nВы победили!!! >:)";
                break;
            case 2:
                if (playerChoice == "r" || playerChoice == "sp")
                    figure.text += "\nВы проиграли :'(";
                else if (playerChoice == "sc")
                    figure.text += "\nПолучилась ничья :|";
                else
                    figure.text += "\nВы победили!!! >:)";
                break;
            case 3:
                if (playerChoice == "sc" || playerChoice == "r")
                    figure.text += "\nВы проиграли :'(";
                else if (playerChoice == "l")
                    figure.text += "\nПолучилась ничья :|";
                else
                    figure.text += "\nВы победили!!! >:)";
                break;
            case 4:
                if (playerChoice == "p" || playerChoice == "l")
                    figure.text += "\nВы проиграли :'(";
                else if (playerChoice == "sp")
                    figure.text += "\nПолучилась ничья :|";
                else
                    figure.text += "\nВы победили!!! >:)";
                break;

        }
        end.gameObject.SetActive(true);
    }

    public void RockChoose()
    {
        playerChoice = "r";
        paper.gameObject.SetActive(false);
        scissors.gameObject.SetActive(false);
        lizard.gameObject.SetActive(false);
        spok.gameObject.SetActive(false);
        figure.text = "Ваша фигура: Камень";
        SendToDatabase();
        GameFlow();
    }

    public void PaperChoose()
    {
        playerChoice = "p";
        rock.gameObject.SetActive(false);
        scissors.gameObject.SetActive(false);
        lizard.gameObject.SetActive(false);
        spok.gameObject.SetActive(false);
        figure.text = "Ваша фигура: Бумага";
        SendToDatabase();
        GameFlow();
    }

    public void ScissorsChoose()
    {
        playerChoice = "sc";
        paper.gameObject.SetActive(false);
        rock.gameObject.SetActive(false);
        lizard.gameObject.SetActive(false);
        spok.gameObject.SetActive(false);
        figure.text = "Ваша фигура: Ножницы";
        SendToDatabase();
        GameFlow();
    }

    public void LizardChoose()
    {
        playerChoice = "l";
        paper.gameObject.SetActive(false);
        scissors.gameObject.SetActive(false);
        rock.gameObject.SetActive(false);
        spok.gameObject.SetActive(false);
        figure.text = "Ваша фигура: Ящерица";
        SendToDatabase();
        GameFlow();
    }

    public void SpokChoose()
    {
        playerChoice = "sp";
        paper.gameObject.SetActive(false);
        scissors.gameObject.SetActive(false);
        lizard.gameObject.SetActive(false);
        rock.gameObject.SetActive(false);
        figure.text = "Ваша фигура: Спок";
        SendToDatabase();
        GameFlow();
    }
    public void EndBattle()
    {
        end.gameObject.SetActive(false);
        choiceMenu.gameObject.SetActive(false);
        main.gameObject.SetActive(true);
    }
}
