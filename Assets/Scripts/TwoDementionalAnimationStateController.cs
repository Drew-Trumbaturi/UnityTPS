using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoDementionalAnimationStateController : MonoBehaviour
{
    Animator animator;
    float velocityZ = 0.0f;
    float velocityX = 0.0f;
    public float acceleration = 2.0f;
    public float deceleration = 1.0f;
    public float maxWalkVelocity = 0.5f;
    public float maxRunVelocity = 2.0f;

    // Increase performance
    int VelocityZHash;
    int VelocityXHash;

    // Start is called before the first frame update
    void Start()
    {
        // Set reference for the Animator in the GameObject this script is attached to
        animator = GetComponent<Animator>();

        // Increase performance
        VelocityZHash = Animator.StringToHash("Velocity Z");
        VelocityXHash = Animator.StringToHash("Velocity X");

        
    }

    void ChangeVelocity(bool walkPressed, bool backwardsPressed, bool leftStrafePressed, bool rightStrafePressed, bool runPressed, float currentMaxVelocity)
    {
        // If player input "w" is pressed
        if (walkPressed && velocityZ < currentMaxVelocity)
        {
            velocityZ += Time.deltaTime * acceleration;
        }

        // If player input is pressed for leftStrafe
        if (leftStrafePressed && velocityX > -currentMaxVelocity)
        {
            velocityX -= Time.deltaTime * acceleration;
        }

        // If player input is pressed for rightStrafe
        if (rightStrafePressed && velocityX < currentMaxVelocity)
        {
            velocityX += Time.deltaTime * acceleration;
        }

        // If player input is pressed for backwardsPressed
        if (backwardsPressed && velocityZ > -currentMaxVelocity)
        {
            velocityZ -= Time.deltaTime * acceleration;
        }

        // If no input is pressed by player this will decrease velocityZ
        if (!walkPressed && velocityZ > 0.0f)
        {
            velocityZ -= Time.deltaTime * deceleration;
        }

        // If no input is pressed by player this will increase velocityZ
        if (!backwardsPressed && velocityZ < 0.0f)
        {
            velocityZ += Time.deltaTime * deceleration;
        }

        // Increase VelocityX if left is not pressed and velocityX < 0.0f
        if (!leftStrafePressed && velocityX < 0.0f)
        {
            velocityX += Time.deltaTime * deceleration;
        }

        // Decrease VelocityX if right is not pressed and VelocityX > 0.0f
        if (!rightStrafePressed && velocityX > 0.0f)
        {
            velocityX -= Time.deltaTime * deceleration;
        }
    }

    void LockOrResetVelocity(bool walkPressed, bool backwardsPressed, bool leftStrafePressed, bool rightStrafePressed, bool runPressed, float currentMaxVelocity)
    {
        // Reset VelocityZ
        if (!backwardsPressed && !walkPressed && velocityZ != 0.0f && (velocityZ > -0.05f && velocityZ < 0.05f))
        {
            velocityZ = 0.0f;
        }

        // Reset VelocityX
        if (!leftStrafePressed && !rightStrafePressed && velocityX != 0.0f && (velocityX > -0.05f && velocityX < 0.05f))
        {
            velocityX = 0.0f;
        }

        // Lock Forward
        if (walkPressed && runPressed && velocityZ > currentMaxVelocity)
        {
            velocityZ = currentMaxVelocity;
        }
        // Decelerate to the max walk speed
        else if (walkPressed && velocityZ > currentMaxVelocity)
        {
            velocityZ -= Time.deltaTime * deceleration;
            // Round to the currentMaxVelocity if within offset
            if (velocityZ > currentMaxVelocity && velocityZ < (currentMaxVelocity + 0.05f))
            {
                velocityZ = currentMaxVelocity;
            }
        }
        else if (walkPressed && velocityZ < currentMaxVelocity && velocityZ > (currentMaxVelocity - 0.05f))
        {
            velocityZ = currentMaxVelocity;
        }

        // Lock backwards
        if (backwardsPressed && runPressed && velocityZ < -currentMaxVelocity)
        {
            velocityZ = -currentMaxVelocity;
        }
        // Decelerate to the max walk speed
        else if (backwardsPressed && velocityZ < -currentMaxVelocity)
        {
            velocityZ += Time.deltaTime * deceleration;
            // Round to the currentMaxVelocity if within offset
            if (velocityZ < -currentMaxVelocity && velocityZ > (-currentMaxVelocity - 0.05f))
            {
                velocityZ = -currentMaxVelocity;
            }
        }
        else if (backwardsPressed && velocityZ > -currentMaxVelocity && velocityZ < (-currentMaxVelocity + 0.05f))
        {
            velocityZ = -currentMaxVelocity;
        }

        // Lock right
        if (rightStrafePressed && runPressed && velocityX > currentMaxVelocity)
        {
            velocityX = currentMaxVelocity;
        }
        // Decelerate to the max walk speed
        else if (rightStrafePressed && velocityX > currentMaxVelocity)
        {
            velocityX -= Time.deltaTime * deceleration;
            // Round to the currentMaxVelocity if within offset
            if (velocityX > currentMaxVelocity && velocityX < (currentMaxVelocity + 0.05f))
            {
                velocityX = currentMaxVelocity;
            }
        }
        else if (rightStrafePressed && velocityX < currentMaxVelocity && velocityX > (currentMaxVelocity - 0.05f))
        {
            velocityX = currentMaxVelocity;
        }

        // Lock left
        if (leftStrafePressed && runPressed && velocityX < -currentMaxVelocity)
        {
            velocityX = -currentMaxVelocity;
        }
        // Decelerate to the max walk speed
        else if (leftStrafePressed && velocityX < -currentMaxVelocity)
        {
            velocityX += Time.deltaTime * deceleration;
            // Round to the currentMaxVelocity if within offset
            if (velocityX < -currentMaxVelocity && velocityX > (-currentMaxVelocity - 0.05f))
            {
                velocityX = -currentMaxVelocity;
            }
        }
        else if (leftStrafePressed && velocityX > -currentMaxVelocity && velocityX < (-currentMaxVelocity + 0.05f))
        {
            velocityX = -currentMaxVelocity;
        }
    }

    // Update is called once per frame
    void Update()
    {
        bool walkPressed = Input.GetKey(KeyCode.W);
        bool leftStrafePressed = Input.GetKey(KeyCode.A);
        bool rightStrafePressed = Input.GetKey(KeyCode.D);
        bool backwardsPressed = Input.GetKey(KeyCode.S);
        bool runPressed = Input.GetKey(KeyCode.LeftShift);
        
        // Set current maxVelocity
        float currentMaxVelocity = runPressed ? maxRunVelocity : maxWalkVelocity;

        // Handle changes in Velocity
        ChangeVelocity(walkPressed, backwardsPressed, leftStrafePressed, rightStrafePressed, runPressed, currentMaxVelocity);
        LockOrResetVelocity(walkPressed, backwardsPressed, leftStrafePressed, rightStrafePressed, runPressed, currentMaxVelocity);

        // Sets the Animator float variables
        animator.SetFloat(VelocityZHash, velocityZ);
        animator.SetFloat(VelocityXHash, velocityX);
    }
}
