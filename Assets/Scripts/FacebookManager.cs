//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using Facebook.Unity;
//
//public class FacebookManager : MonoBehaviour
//{
//#if UNITY_IOS || UNITY_ANDROID
//    private void Awake()
//    {
//        if (!FB.IsInitialized)
//        {
//            FB.Init(InitCallback, OnHideUnity);
//        }
//        else
//        {
//            FB.ActivateApp();
//        }
//    }
//
//    private void InitCallback()
//    {
//        if (FB.IsInitialized)
//        {
//            FB.ActivateApp();
//            Debug.Log("Facebook SDK Initialized");
//        }
//        else
//        {
//            Debug.Log("Failed to Initialize the Facebook SDK");
//        }
//    }
//
//    private void OnHideUnity(bool isGameShown)
//    {
//        if (!isGameShown)
//        {
//            Time.timeScale = 0;
//        }
//        else
//        {
//            Time.timeScale = 1;
//        }
//    }
//#endif
//}
