using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    Animator animator;
    int IsWalkingHash;
    int IsRunningHash;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        IsWalkingHash = Animator.StringToHash("IsWalking");
        IsRunningHash = Animator.StringToHash("IsRunning");
    }

    // Update is called once per frame
    void Update()
    {
        bool IsWalking = animator.GetBool(IsWalkingHash);
        bool IsRunning = animator.GetBool(IsRunningHash);
        bool forwardPressed = Input.GetKey("w");
        bool runPressed = Input.GetKey("left shift");

        // If player is pressing w key
        if (!IsWalking && forwardPressed)
        {
            // Then set the IsWalking bool to true
            animator.SetBool(IsWalkingHash, true);
        }

        // If player is not pressing w key
        if (IsWalking && !forwardPressed)
        {
            // Then set bool to false
            animator.SetBool(IsWalkingHash, false);
        }

        // If player is walking and not running and presses left shift
        if (!IsRunning && forwardPressed && runPressed)
        {
            // then set the IsRunning bool to be true
            animator.SetBool(IsRunningHash, true);
        }

        // If player is running and stops running or walking
        if (IsRunning && (!forwardPressed || !runPressed))
        {
            // Then set the IsRunning bool to be false
            animator.SetBool(IsRunningHash, false);
        }
    }
}
