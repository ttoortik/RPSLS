﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class User
{

    public string userName;
    public string userChoice;
    public User()
    {
        userName = GameController.playerName;
        userChoice = GameController.playerChoice;
    }
}
