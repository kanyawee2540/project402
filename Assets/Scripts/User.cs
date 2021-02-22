using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class User
{
    public string username;
    public string password;

    public User()
    {
        username = Login.username;
        password = Login.userPassword;
    }
}
