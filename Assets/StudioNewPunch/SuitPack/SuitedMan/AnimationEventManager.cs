using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventManager : MonoBehaviour
{
    public void PointingStopped()
    {
        var player = GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonController>();
        player.CustomClipPlaying = false;
        var pMode = player.CustomClipPlaying ? "Cancel Pointing" : "Pointing";
        var scaptureMode = SceneManager.Instance.ScreenShotMode ? "Play mode" : "Screenshot mode";
        var modeDescription = SceneManager.Instance.ScreenShotMode ? "(In this mode you can move the\n camera freely using WSAD)" : "(In this mode you can move the\n player using WSAD)";
        SceneManager.Instance.SetGuideText($"P = {pMode}\nTab = {scaptureMode}\n<color=blue><size=40>{modeDescription}</size></color> ");

    }
}
