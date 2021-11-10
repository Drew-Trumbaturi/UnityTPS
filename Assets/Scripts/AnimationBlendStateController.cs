using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationBlendStateController : MonoBehaviour
{
    Animator animator;
    float velocity = 0.0f;
    public float acceleration = 0.1f;
    public float deceleration = 0.5f;
    int VelocityHash;

    // Start is called before the first frame update
    void Start()
    {
        // Set referance for the animator
        animator = GetComponent<Animator>();

        // increases performance
        VelocityHash = Animator.StringToHash("Velocity");
    }

    // Update is called once per frame
    void Update()
    {
        // Get key input from player
        bool walkPressed = Input.GetKey("w");
        bool runPressed = Input.GetKey("left shift");

        if (walkPressed && velocity <= 1.0f)
        {
            velocity += Time.deltaTime * acceleration;
        }
        
        if (!walkPressed && velocity >= 0.0f)
        {
            velocity -= Time.deltaTime * deceleration;
        }

        if (!walkPressed && velocity < 0.0f)
        {
            velocity = 0.0f;
        }

        animator.SetFloat(VelocityHash, velocity);
    }
}
