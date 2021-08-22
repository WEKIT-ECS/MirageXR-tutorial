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


    public bool AnimatorIsPlaying()
    {
        return anim.GetCurrentAnimatorStateInfo(0).length >
               anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }

    public bool AnimatorIsPlaying(string stateName)
    {
        return AnimatorIsPlaying() && anim.GetCurrentAnimatorStateInfo(0).IsName(stateName);
    }

    void PlayAnimClip(string clip)
    {
        anim.SetBool("Idle", false);
        anim.SetBool("Walk", false);
        anim.SetBool("Run", false);
        anim.SetBool("Pointing", false);
        anim.SetBool(clip, true);
    }
}
