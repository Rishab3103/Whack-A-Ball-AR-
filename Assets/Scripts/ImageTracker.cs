using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using Photon.Pun;
using Photon.Realtime;
using Photon.Pun.UtilityScripts;
using System;
public class ImageTracker : MonoBehaviour
{


    public ARTrackedImageManager m_ImageManager;
    public XRReferenceImageLibrary m_ImageLibrary;
    public GameObject arena;
    
    
    
    public int m_NumberOfTrackedImages;
    
    private void Awake()
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
        foreach (ARTrackedImage img in obj.updated)
        {
           
            arena=Instantiate(arena,img.transform,true);
            arena.transform.SetPositionAndRotation(img.transform.position, img.transform.rotation);
            
            //Debug.Log("Arena pos:" + arena.transform.position);


        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        NumberOfTrackedImages();
        Debug.Log(m_NumberOfTrackedImages);
    }

    public int NumberOfTrackedImages()
    {
        m_NumberOfTrackedImages = 0;
        foreach (ARTrackedImage image in m_ImageManager.trackables)
        {
            if (image.trackingState == TrackingState.Tracking)
            {
                m_NumberOfTrackedImages++;
            }
        }
        return m_NumberOfTrackedImages;
    }
}
