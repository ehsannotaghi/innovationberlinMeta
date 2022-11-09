using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun.Demo.Asteroids;
using System.Collections;
using UnityEngine.Networking;

using Photon.Pun;


public class WebLogin : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void Web3Connect();

    [DllImport("__Internal")]
    private static extern string ConnectAccount();

    [DllImport("__Internal")]
    private static extern void SetConnectAccount(string value);

    private int expirationTime;
    private string account;

    public void OnLogin()
    {
        if (!Application.isEditor)
            Web3Connect();
        OnConnected();
    }

    async private void OnConnected()
    {
        if (!Application.isEditor)
            account = ConnectAccount();
        else
     // shahab main  
  //   account = "0x95817a76d90623f8a9c5dfb3005f1a7147972966";
        //ehsan
          //  account = "0x02DBdC4990Af0D6749cc26aCd86Ed37a34FDc89d";
        //sajjad
        //  account = "0x3e322Eac82f37e8Cf2D4B703FEE6AB91c25DB735";

        //no mask wallet
     //  account = "0x71C7656EC7ab88b098defB751B7401B5f6d8976F";

        //shahab 2
        // account = "0x4d2B6990548B4548f7300D54e3acC6495Da0b31D";

        //andfei 
        account = "0xffaf058baa44806b0aaffd2674bd28e4944ba4d7";
        while (account == "")
        {
            await new WaitForSeconds(1f);
            account = ConnectAccount();
        };
        // save account for next scene
        PlayerPrefs.SetString("Account", account);
        // reset login message
        if (!Application.isEditor)
            SetConnectAccount("");
        // load next scene
        Debug.Log(account);
    

        FindObjectOfType<LobbyMainPanel>().OnLoginButtonClickedWallet(account.ToString());



    }

    public void OnSkip()
    {
        // burner account for skipped sign in screen
        PlayerPrefs.SetString("Account", "");
        // move to next scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }






}



