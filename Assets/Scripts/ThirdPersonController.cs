using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonController : MonoBehaviour
{
    public Rigidbody rb;
    public Transform cam;
    public AnimationController anim;

    public float walkSpeed = 5f;
    public float RunSpeed = 9f;

    float turnSmoothTime = 0.1f;
    float smoothVelocity;

    private bool _customClipPlaying;

        // Update is called once per frame
        void FixedUpdate()
    {
        if (_customClipPlaying || SceneManager.Instance.ScreenShotMode) return;

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vetical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0, vetical).normalized;

        if (direction.magnitude >= 0.01f)
        {
            float targetAngle = Mathf.Atan2(direction.x , direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle,ref smoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, smoothAngle, 0f);

            Vector3 inputs = new Vector3(horizontal, 0.0f, vetical);
            Vector3 moveDir = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;

            float movementSpeed = 0;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                movementSpeed = RunSpeed;
                anim.Run();
            }
            else
            {
                movementSpeed = walkSpeed;
                anim.Walk();
            }

            rb.MovePosition(transform.position + moveDir.normalized * movementSpeed * Time.fixedDeltaTime);

        }else
        {
            anim.Idle();
        }


    }

    private void Update()
    {
        if(SceneManager.Instance.ScreenShotMode) return;

        if (Input.GetKeyDown(KeyCode.P))
        {

            if (!_customClipPlaying)
            {
                anim.Point();
                _customClipPlaying = true;
            }
            else
            {
                anim.StopPoint();
                if (!anim.AnimatorIsPlaying("StopPointing"))
                {
                    _customClipPlaying = false;
                }
            }

            var pMode = _customClipPlaying ? "Cancel Pointing" : "Pointing";
            var scaptureMode = SceneManager.Instance.ScreenShotMode ? "Play mode" : "Screenshot mode";
            var modeDescription = SceneManager.Instance.ScreenShotMode ? "(In this mode you can move the\n camera freely using WSAD)" : "(In this mode you can move the\n player using WSAD)";
            SceneManager.Instance.SetGuideText($"P = {pMode}\nTab = {scaptureMode}\n<color=blue><size=40>{modeDescription}</size></color> ");

        }
            
    }

}
