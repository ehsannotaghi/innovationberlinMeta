using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;
using TMPro;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using System.IO.Compression;
using System.IO;
using System.Runtime.InteropServices;

public class DownloadNft : MonoBehaviour
{



    [DllImport("__Internal")]
    private static extern void FetchData(string WalletAddress);
    [DllImport("__Internal")]
    private static extern string GotData();
    private string Data = "";


    // Start is called before the first frame update
     void Start()
    {
        //without opensea x-api-key
        var walletAddress = PlayerPrefs.GetString("Account");
        FetchData(walletAddress);
        OnGotData();


        //Works with opensea x-api-key
        //   StartCoroutine(sendRequestJordiMasks());

    }
    async private void OnGotData()
    {

        while (Data == "")
        {
            await new WaitForSeconds(1f);
            Data = GotData();

        };
        //After fecthing data from opensea
        StartCoroutine(FetchFunctionWithoutAPIkey(Data));

    }


    IEnumerator FetchFunctionWithoutAPIkey(string _data) {

        JSONNode data = JSON.Parse(_data);
        for (int i = 0; i < data.Count; i++)
        {
            // requests for texture by provided url in privous object
            UnityWebRequest request2 = UnityWebRequestTexture.GetTexture(data[i]["image_url"]);

            yield return request2.SendWebRequest();
            if (request2.isNetworkError || request2.isHttpError)
            {
                // Any error during getting texture from url
                Debug.Log("error : " + request2.isNetworkError);
            }
            else
            {
                // Set each art texture and name to an scene object
                GameObject.FindGameObjectsWithTag("ArtWorks")[i].
                    GetComponent<MeshRenderer>().material.mainTexture = ((DownloadHandlerTexture)request2.downloadHandler).texture;
                GameObject.FindGameObjectsWithTag("ArtWorks")[i].
                    transform.GetChild(0).GetComponent<TextMeshPro>().text = data[i]["name"];
            }
        }

    }
    


    //Works with Opensea x-api-key
    //IEnumerator sendRequestJordiMasks()
    //{


    //   // Getting wallet address from player prefs or cache(Provided by wallet connect )
    //    string walletaddress = PlayerPrefs.GetString("Account");

    //      var FetchedDataRaw =  fetchedDataOpensea(walletaddress);
    //    Debug.Log("this is from js : " + FetchedDataRaw);
    //              // json class parse class
    //              JSONNode data = JSON.Parse(FetchedDataRaw);

    //            // loop through all asssets arrays 
    //            for (int i = 0; i < data["assets"].Count; i++)
    //            {
    //                // requests for texture by provided url in privous object
    //                UnityWebRequest request2 = UnityWebRequestTexture.GetTexture(data["assets"][i]["image_preview_url"]);

    //                yield return request2.SendWebRequest();
    //                if (request2.isNetworkError || request2.isHttpError)
    //                {
    //                    // Any error during getting texture from url
    //                    Debug.Log("error : " + request2.isNetworkError);
    //                }
    //                else
    //                {
    //                    // Set each art texture and name to an scene object
    //                    GameObject.FindGameObjectsWithTag("ArtWorks")[i].
    //                        GetComponent<MeshRenderer>().material.mainTexture = ((DownloadHandlerTexture)request2.downloadHandler).texture;
    //                    GameObject.FindGameObjectsWithTag("ArtWorks")[i].
    //                        transform.GetChild(0).GetComponent<TextMeshPro>().text = data["assets"][i]["name"];
    //                }
    //            }


    //}
}



