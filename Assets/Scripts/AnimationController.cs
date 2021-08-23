using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public Animator anim;

    public void Idle()
    {
        PlayAnimClip("Idle");
    }


    public void Walk()
    {
        PlayAnimClip("Walk");
    }

    public void Run()
    {
        PlayAnimClip("Run");
    }

    public void Point()
    {
        PlayAnimClip("Pointing");
    }

    public void StopPoint()
    {
        PlayAnimClip("StopPointing");
    }


    void PlayAnimClip(string clip)
    {
        anim.SetBool("Idle", false);
        anim.SetBool("Walk", false);
        anim.SetBool("Run", false);
        anim.SetBool("Pointing", false);
        anim.SetBool("StopPointing", false);
        anim.SetBool(clip, true);
    }
}
