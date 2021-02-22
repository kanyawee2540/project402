using System.Collections;
using System.Collections.Generic;
using Proyecto26;
using UnityEngine;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    public Text NameText;
    public InputField usernameText;
    public InputField passwordText;

    private System.Random random = new System.Random(); 

    User user = new User();
    
    public static int playerScore;
    public static string username;
    public static string userPassword;


    // Start is called before the first frame update
    /*private void Start()
    {
        playerScore = random.Next(0, 101);
        scoreText.text = "Score: " + playerScore;
    }*/

    public void OnSubmit()
    {
        username = usernameText.text;
        userPassword = passwordText.text;
        PostToDatabase();
    }
    
    public void OnGetScore()
    {
        RetrieveFromDatabase();
    }

    private void UpdateName()
    {
        NameText.text = "Name is: " + user.username;
    }

    private void PostToDatabase()
    {
        User user = new User();
        RestClient.Put("https://project-75a5c-default-rtdb.firebaseio.com/" + username + ".json", user);

    }

    private void RetrieveFromDatabase()
    {
        RestClient.Get<User>("https://project-75a5c-default-rtdb.firebaseio.com/" + usernameText.text + ".json").Then(response =>
            {
                user = response;
                UpdateName();
            });
    }
}
