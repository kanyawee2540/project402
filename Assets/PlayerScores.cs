using System.Collections;
using System.Collections.Generic;
using FullSerializer;
using Proyecto26;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PlayerScores : MonoBehaviour
{
    /*public Text scoreText;
    public InputField getScoreText;

    public InputField firstNameText;
    public InputField lastNameText;
    public InputField emailText;
    public InputField usernameText;
    public InputField passwordText;
    public InputField confirmPasswordText;*/

    //Login variables
    [Header("Login")]
    public InputField emailLoginField;
    public InputField passwordLoginField;
    //public Text warningLoginText;
    //public Text confirmLoginText;

    //Register variables
    [Header("Register")]
    public InputField nameRegisterField;
    public InputField surnameRegisterField;
    public InputField emailRegisterField;
    public InputField usernameRegisterField;
    public InputField passwordRegisterField;
    public InputField passwordRegisterVerifyField;
    //public Text warningRegisterText;


    private System.Random random = new System.Random(); 

    User user = new User();

    private string databaseURL = "https://project-75a5c-default-rtdb.firebaseio.com/users"; 
    private string AuthKey = "AIzaSyBdvdw8w_v66y96r7ndXpKRs39lMiZwABY";
    
    public static fsSerializer serializer = new fsSerializer();
    
    
    public static int playerScore;
    public static string playerName;

    private string idToken;
    
   public static string localId;

    // private string getLocalId;
    
    /*
    private void Start()
    {
        playerScore = random.Next(0, 101);
        scoreText.text = "Score: " + playerScore;
    }

    public void OnSubmit()
    {
        PostToDatabase();
    }
    
    public void OnGetScore()
    {
        GetLocalId();
    }

    private void UpdateScore()
    {
        scoreText.text = "Score: " + user.userScore;
    }
    */

    private void PostToDatabase(bool emptyScore = false, string idTokenTemp = "")
    {
        if (idTokenTemp == "")
        {
            idTokenTemp = idToken;
        }
        
        User user = new User();
        
        RestClient.Put(databaseURL + "/" + localId + ".json?auth=" + idTokenTemp, user);
    }

    /*
    private void RetrieveFromDatabase()
    {
        RestClient.Get<User>(databaseURL + "/" + getLocalId + ".json?auth=" + idToken).Then(response =>
            {
                user = response;
                UpdateScore();
            });
    }
    */

    public void SignUpUserButton()
    {
        SignUpUser(emailRegisterField.text, usernameRegisterField.text, passwordRegisterField.text);
    }
    
  /*  public void SignInUserButton()
    {
        SignInUser(emailText.text, passwordText.text);
    }
    
    */
    private void SignUpUser(string email, string username, string password)
    {
        string userData = "{\"email\":\"" + email + "\",\"password\":\"" + password + "\",\"returnSecureToken\":true}";
        RestClient.Post<SignResponse>("https://www.googleapis.com/identitytoolkit/v3/relyingparty/signupNewUser?key=" + AuthKey, userData).Then(
            response =>
            {
                string emailVerification = "{\"requestType\":\"VERIFY_EMAIL\",\"idToken\":\"" + response.idToken + "\"}";
                RestClient.Post(
                    "https://www.googleapis.com/identitytoolkit/v3/relyingparty/getOobConfirmationCode?key=" + AuthKey,
                    emailVerification);
                localId = response.localId;
                playerName = username;
                PostToDatabase(true, response.idToken);
                
            }).Catch(error =>
        {
            Debug.Log(error);
        });
    }
    
   /* private void SignInUser(string email, string password)
    {
        string userData = "{\"email\":\"" + email + "\",\"password\":\"" + password + "\",\"returnSecureToken\":true}";
        RestClient.Post<SignResponse>("https://www.googleapis.com/identitytoolkit/v3/relyingparty/verifyPassword?key=" + AuthKey, userData).Then(
            response =>
            {
                string emailVerification = "{\"idToken\":\"" + response.idToken + "\"}";
                RestClient.Post(
                    "https://www.googleapis.com/identitytoolkit/v3/relyingparty/getAccountInfo?key=" + AuthKey,
                    emailVerification).Then(
                    emailResponse =>
                    {

                        fsData emailVerificationData = fsJsonParser.Parse(emailResponse.Text);
                        EmailConfirmationInfo emailConfirmationInfo = new EmailConfirmationInfo();
                        serializer.TryDeserialize(emailVerificationData, ref emailConfirmationInfo).AssertSuccessWithoutWarnings();
                        
                        if (emailConfirmationInfo.users[0].emailVerified)
                        {
                            idToken = response.idToken;
                            localId = response.localId;
                            GetUsername();
                        }
                        else
                        {
                            Debug.Log("You are stupid, you need to verify your email dumb");
                        }
                    });
                
            }).Catch(error =>
        {
            Debug.Log(error);
        });
    }*/

    /*
    private void GetUsername()
    {
        RestClient.Get<User>(databaseURL + "/" + localId + ".json?auth=" + idToken).Then(response =>
        {
            playerName = response.userName;
        });
    }*/
    
    /*
    private void GetLocalId(){
        RestClient.Get(databaseURL + ".json?auth=" + idToken).Then(response =>
        {
            var username = getScoreText.text;
            
            fsData userData = fsJsonParser.Parse(response.Text);
            Dictionary<string, User> users = null;
            serializer.TryDeserialize(userData, ref users);

            foreach (var user in users.Values)
            {
                if (user.userName == username)
                {
                    getLocalId = user.localId;
                    RetrieveFromDatabase();
                    break;
                }
            }
        }).Catch(error =>
        {
            Debug.Log(error);
        });
    }*/
}
