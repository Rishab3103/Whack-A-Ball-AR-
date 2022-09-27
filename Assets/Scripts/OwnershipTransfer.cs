using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using Photon.Pun.UtilityScripts;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR;
using UnityEngine.XR.ARSubsystems;
public class OwnershipTransfer : MonoBehaviourPun
{
    // Start is called before the first frame update
   

    private void Start()
    {
        base.photonView.TransferOwnership(PhotonNetwork.MasterClient);

    }

    private void Update()
    {
      
    }

}
