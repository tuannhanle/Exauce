using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Management;

public class DetectPlatform : MonoBehaviour
{
    [SerializeField] XRGeneralSettings xRGeneralSettings;
    [SerializeField] XRLoader xRLoader;
    [SerializeField] bool isTestVrHeadset;

    void Awake()
    {


#if (UNITY_EDITOR)
        Debug.Log("UNITY EDITOR");
        if (isTestVrHeadset)
        {

            SceneController.instance.LoadVideoScene(SceneType.Vr360Scene);

        }
        else
        {

            SceneController.instance.LoadVideoScene(SceneType.MainScene);


        }
#endif

#if (!UNITY_EDITOR) 
        
        Debug.Log("UNITY_ANDROID");

        Debug.Log(SystemInfo.deviceName);
        var hashDeviceName = SystemInfo.deviceName.GetHashCode();

        if (hashDeviceName == "Oculus Quest".GetHashCode() || 
            hashDeviceName == "Oculus Quest 2".GetHashCode() || 
            hashDeviceName == "Rift".GetHashCode() || 
            hashDeviceName == "Rift S".GetHashCode() || 
            hashDeviceName == "Quest Link".GetHashCode() )
        {
            Debug.Log("___VR HEADSET___");
            xRGeneralSettings.Manager.loaders.Clear();
            xRGeneralSettings.Manager.loaders.Add(xRLoader);
            xRGeneralSettings.Manager.InitializeLoader();

            SceneController.instance.LoadVideoScene(SceneType.Vr360Scene);

        }
        else
        {
            xRGeneralSettings.Manager.loaders.Clear();
            xRGeneralSettings.Manager.loaders.Add(xRLoader);
            xRGeneralSettings.Manager.InitializeLoaderSync();
            Debug.Log("___MOBILE HEADSET___");

            SceneController.instance.LoadVideoScene(SceneType.MainScene);


        }
#endif

    }


}
