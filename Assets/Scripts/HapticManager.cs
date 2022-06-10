using MoreMountains.NiceVibrations;
using UnityEngine;

public class HapticManager : MonoBehaviour
{
    public static HapticManager Instance;
    public enum HapticType { Selection, Success, Warning, Failure, LightImpact, MediumImpact, HeavyImpact, RigidImpact, SoftImpact, None }
    
    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void GenerateHaptic(HapticType type = HapticType.MediumImpact)
    {
        if (!DataManager.Vibration)
            return;

        MMVibrationManager.Haptic((HapticTypes) type);
        //MMVibrationManager.TransientHaptic(0.7f, 0.7f, true, this);
    }

    private float nextTimeToHaptic = 0f;

    public void GenerateHapticWithInterval(HapticType type = HapticType.MediumImpact, float interval = .2f)
    {
        if (!DataManager.Vibration)
            return;

        if (Time.time < nextTimeToHaptic)
            return;

        nextTimeToHaptic = Time.time + interval;

        MMVibrationManager.Haptic((HapticTypes)type);
        //MMVibrationManager.TransientHaptic(0.7f, 0.7f, true, this);
    }


    /*Do not delete this function! It's necessary for Android libraries!*/
    private void NecessaryAndroidCall()
    { 
        Handheld.Vibrate();
    }
}