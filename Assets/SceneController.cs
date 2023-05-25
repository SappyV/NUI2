using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    

    public void LoadLevel1()
    {
        Debug.Log("Load Level 1");
        SceneManager.LoadScene("Classroom");
    }

    public void LoadLobby()
    {
        Debug.Log("Load Lobby");
        SceneManager.LoadScene("LobbyScene");
    }
}
