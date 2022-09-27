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
public class OwnershipTransferClient : MonoBehaviourPun
{
    // Start is called before the first frame update
    void Start()
    {
        base.photonView.TransferOwnership(PhotonNetwork.LocalPlayer);
    }

    // Update is called once per frame
    void Update()
    {
       /* if (GameObject.FindGameObjectsWithTag("hammer1").
        {
            base.photonView.TransferOwnership(PhotonNetwork.LocalPlayer);
        }*/
    }
}
