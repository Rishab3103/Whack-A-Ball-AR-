using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    public static NetworkManager Instance;
    
    public GameObject playerPrefab;
    public bool isPresent;
    public bool sceneLoad;
    public Camera ARCam;
    public Material material;
    void Start()
    {
        Instance = this;
        isPresent = false;
      

    }

    // Update is called once per frame
    void Update()
    {
        if (isPresent==false && GameObject.FindGameObjectsWithTag("arena").Length!=0)
        {
            
            PhotonNetwork.Instantiate(this.playerPrefab.name, GameObject.FindGameObjectsWithTag("arena")[0].transform.position, Quaternion.identity, 0);
            isPresent = true;
            this.playerPrefab.GetComponent<MeshRenderer>().material = material;
        }

       
        

    }

    public override void OnLeftRoom()
    {
        SceneManager.LoadScene(0);
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    void LoadArena()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            Debug.LogError("PhotonNetwork : Trying to Load a level but we are not the master Client");
        }
        Debug.LogFormat("PhotonNetwork : Loading Level : {0}", PhotonNetwork.CurrentRoom.PlayerCount);
        PhotonNetwork.LoadLevel("Room for " + PhotonNetwork.CurrentRoom.PlayerCount);
        PhotonNetwork.Destroy(GameObject.FindGameObjectsWithTag("hammmer1")[0]);

    }

    public override void OnPlayerEnteredRoom(Player other)
    {
        Debug.LogFormat("OnPlayerEnteredRoom() {0}", other.NickName); // not seen if you're the player connecting


        if (PhotonNetwork.IsMasterClient)
        {
            Debug.LogFormat("OnPlayerEnteredRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom


            LoadArena();
            
        }
        
    }


    public override void OnPlayerLeftRoom(Player other)
    {
        Debug.LogFormat("OnPlayerLeftRoom() {0}", other.NickName); // seen when other disconnects


        if (PhotonNetwork.IsMasterClient)
        {
            Debug.LogFormat("OnPlayerLeftRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom


            LoadArena();
        }
    }


    public void PlayAgain()
    {
        SceneManager.LoadScene(0);
    }
}
