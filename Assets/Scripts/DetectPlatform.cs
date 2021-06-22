using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlatform : MonoBehaviour
{
    [SerializeField] bool isTestVrHeadset;

    void Start()
    {
#if UNITY_EDITOR_WIN
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
#if UNITY_ANDROID


        Debug.Log(SystemInfo.deviceName);
        var hashDeviceName = SystemInfo.deviceName.GetHashCode();

        if (hashDeviceName == "Oculus Quest".GetHashCode() || 
            hashDeviceName == "Oculus Quest 2".GetHashCode() || 
            hashDeviceName == "Rift".GetHashCode() || 
            hashDeviceName == "Rift S".GetHashCode() || 
            hashDeviceName == "Quest Link".GetHashCode() )
        {
            SceneController.instance.LoadVideoScene(SceneType.Vr360Scene);
        }
        else
        {
            SceneController.instance.LoadVideoScene(SceneType.MainScene);

        }
#endif

    }
}
