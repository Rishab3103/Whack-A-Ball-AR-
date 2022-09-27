using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using Photon.Pun.UtilityScripts;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR;
using UnityEngine.XR.ARSubsystems;

public class GameManager : MonoBehaviourPun
{

   //GameObject playingArena;
    //public GameObject cube;
    public GameObject sphere;
    
    public Vector3 m_Min, m_Max;
    public Vector3 mol_pos;
    public DestroyOnTouch mole;
    public float countdown = 60.0f;
    bool tracking;
    public NetworkManager network;
    //public ImageTracker image;

    
    public TextMeshProUGUI score_Text;
    public TextMeshProUGUI timer;
    public TextMeshProUGUI gameOver;
    public GameObject playAgain;
    public ARRaycastManager RaycastManager;
    
    Collider m_collider;
    public int Scorecount = 0;
    float x_dim;
    float y_dim;
    bool isInRoom;
    Vector3 arena_pos = new Vector3();
    public ARPlaneManager arPlaneMananger;
    public PlaneSpawner plane;

    public static GameManager Instance;
    public ImageTracker image;

    public GameObject Hammer;
    private PhotonView PV;

    public ARAnchorManager m_AnchorManager;
    
    int m_NumberOfTrackedImages;
    List<ARRaycastHit> HitResult = new List<ARRaycastHit>();
    /* public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
     {
         if (stream.IsWriting)
         {
             // We own this player: send the others our data
             Debug.Log("writing");
             stream.SendNext(Scorecount);
             stream.SendNext(plane.transform.position);
             stream.SendNext(mol_pos);
         }
         else if (stream.IsReading)
         {
             Debug.Log("reading");
             Scorecount = (int)stream.ReceiveNext();
             plane.transform.position = (Vector3)stream.ReceiveNext();
             mol_pos = (Vector3)stream.ReceiveNext();
         }
     }
 */
    // Start is called before the first frame update


   /* private void Awake()
    {
        m_ImageManager = FindObjectOfType<ARTrackedImageManager>();
    }

    public void OnEnable()
    {

        m_ImageManager.trackedImagesChanged += OnImageChanged;
    }

    public void OnDisable()
    {
        m_ImageManager.trackedImagesChanged -= OnImageChanged;
    }

    public void OnImageChanged(ARTrackedImagesChangedEventArgs obj)
    {
        foreach (ARTrackedImage img in obj.added)
        {

           
                *//*img.destroyOnRemoval = false;*//*

                arena = Instantiate(arena, img.transform, true);
                //Instantiate(arena, image.transform.position, arena.transform.rotation);
                arena.transform.position = img.transform.position;
                //Gameplay();


            


        }
    }*/



    void Start()
    {

        //Debug.Log(HitResult.Count);

        m_collider = GameObject.FindGameObjectsWithTag("arena")[0].GetComponent<BoxCollider>();
        
        //Vector3 mol_pos = new Vector3(Random.Range(m_Min.x, m_Max.x), Random.Range(m_Min.y, m_Max.y), 0);
        //PhotonNetwork.Instantiate("Sphere", mol_pos, mole.gameObject.transform.rotation);

        score_Text = GameObject.Find("Score").GetComponent<TextMeshProUGUI>();

        timer = GameObject.Find("Timer").GetComponent<TextMeshProUGUI>();

        gameOver = GameObject.Find("GameOver").GetComponent<TextMeshProUGUI>();

        //playAgain= GameObject.Find("PlayAgain").GetComponent<Button>();
        gameOver.text = "";
        // playAgain.enabled= false;
        //Instance = this;
        //PV = GetComponent<PhotonView>();
     /*   int buildIndex = SceneManager.sceneCount;
        Debug.Log(buildIndex);
        if (PhotonNetwork.IsConnectedAndReady && buildIndex==2)
        {
            
            PhotonNetwork.Instantiate(Hammer.name, Vector3.zero, Quaternion.identity);
            Debug.Log(Hammer.transform.position);
        }
*/

    }

    // Update is called once per frame
    void Update()
    {

        /**/

        //InvokeRepeating("Timer", 1f, 2f);

        //  if(GameObject.FindGameObjectsWithTag("mole").Length > 1)
        //  {
        //      PhotonNetwork.DestroyAll(mole.gameObject);
        //      StartCoroutine(Timer());
        //   }
        //m_collider = GameObject.FindGameObjectsWithTag("arena")[0].GetComponent<BoxCollider>();
        Debug.Log("Arena pos:" + GameObject.FindGameObjectsWithTag("arena")[0].transform.position);
        ScoreCard();
        Gameplay();
        int buildIndex = SceneManager.sceneCount;
        Debug.Log(buildIndex);
        Debug.Log(PhotonNetwork.IsConnectedAndReady);
        if (GameObject.FindGameObjectsWithTag("arena").Length != 0)
        {
            arPlaneMananger.enabled = false;
            DisablePlanes();
            

            

        }

     




    }

    public void Gameplay()
    {
        
        if (countdown > 0 && GameObject.FindGameObjectsWithTag("arena").Length != 0)
        {
            
            countdown -= Time.deltaTime;
            timer.text = "Timer :" + countdown.ToString();
            StartCoroutine(Timer());
        }

        if (countdown < 0 && GameObject.FindGameObjectsWithTag("arena").Length != 0)
        {
            //SceneManager.LoadScene(0);
            gameOver.text = "Game Over";


            playAgain.SetActive(true);
        }

    }

    IEnumerator Timer()
    {

        yield return new WaitForSeconds(1f);
        
            if (PhotonNetwork.IsMasterClient)
            {
                //arena.SetActive(true);
                if (GameObject.FindGameObjectsWithTag("mole").Length == 0 )
                {

                    m_collider = GameObject.FindGameObjectsWithTag("arena")[0].GetComponent<BoxCollider>();   
                    m_Min = m_collider.bounds.min;
                    m_Max = m_collider.bounds.max;

                    Vector3 mol_pos = new Vector3(Random.Range(m_Min.x, m_Max.x), Random.Range(m_Min.y , m_Max.y ), Random.Range(m_Min.z , m_Max.z ));
                    PhotonNetwork.Instantiate("sphere", mol_pos, mole.gameObject.transform.rotation);
                    //Debug.Log("Sphere Position :" + mol_pos);

                }
            }
        

     
           
        //}
        
     
        
       






    }

/*    public void InstantiateArena()
    {
        PhotonNetwork.Instantiate("arena", plane.temp, plane.PlaceablePrefab.transform.rotation);
        Destroy(plane.PlaceablePrefab);
    }*/
  public void DisablePlanes()
    {
        foreach(var plane in arPlaneMananger.trackables)
        {
            plane.gameObject.SetActive(false);
        }
    }

    public void ScoreCard()
    {
        Player[] pList = PhotonNetwork.PlayerList;

        Player player1 = pList[0];
        Player player2 = pList[1];
        
        if (player1.GetScore() > player2.GetScore())
        {
            score_Text.text = "Score:" + player1.GetScore().ToString();
        }
        else
        {
            score_Text.text = "Score:" + player2.GetScore().ToString();
        }
    }



 /*   void UpdateInfo(ARTrackedImage trackedImage)
    {
        if(trackedImage.trackingState == UnityEngine.XR.ARSubsystems.TrackingState.Tracking)
        {
            tracking = true;
            arena = Instantiate(cube, trackedImage.transform.position, trackedImage.transform.rotation);
        }
    }*/
/*public override void OnLeftRoom()
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
    }*/
       
   

}
