using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventManager : MonoBehaviour
{
    public void PointingStopped()
    {
        var player = GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonController>();
        player.CustomClipPlaying = false;
        var scaptureMode = SceneManager.Instance.ScreenShotMode ? "Play mode" : "Screenshot mode";
        var modeDescription = SceneManager.Instance.ScreenShotMode ? "Camera movement: WSAD\nCamera rotation: Right click" : "Player movement: WSAD";
        SceneManager.Instance.SetGuideText($"P = Pointing\nTab = {scaptureMode}\n{modeDescription} ");

    }
}
