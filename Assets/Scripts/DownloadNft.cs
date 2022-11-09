using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;
using TMPro;

public class DownloadNft : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(sendRequestJordiMasks());
    }

    IEnumerator sendRequestJordiMasks()
    {
        // Getting wallet address from player prefs or cache (Provided by wallet connect or manual input )
        string walletaddress = PlayerPrefs.GetString("Account");

        using (UnityWebRequest www = UnityWebRequest.Get("https://api.opensea.io/api/v1/assets?owner=" + walletaddress + "&format=json"))
        {
            // Opensea auth information
            www.SetRequestHeader("accept", " application/json");
            www.SetRequestHeader("X-API-Key", "6f77ff2a8d164976af7639539e5a4688");

            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                // Any error to the opensea api
                Debug.Log(www.error);
            }
            else
            {
                // json class parse class
                JSONNode data = JSON.Parse(www.downloadHandler.text);

                // loop through all asssets arrays 
                for (int i = 0; i < data["assets"].Count; i++)
                {
                    // requests for texture by provided url in privous object
                    UnityWebRequest request2 = UnityWebRequestTexture.GetTexture(data["assets"][i]["image_preview_url"]);

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
                            transform.GetChild(0).GetComponent<TextMeshPro>().text = data["assets"][i]["name"];
                    }
                }
            }
        }
    }
}
