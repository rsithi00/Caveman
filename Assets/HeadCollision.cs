using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadCollision : MonoBehaviour
{
    private Animator playerAnimator;
    private int stunRemaining = 0;
    [SerializeField] private int maxStunTime = 10;

    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = transform.parent.GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (stunRemaining > 0)
        {
            stunRemaining--;
            Debug.Log(stunRemaining);
            if (stunRemaining == 0)
                playerAnimator.SetBool("IsStunned", false);
        }
    }

    // Override this method so that it does stuff when a collision is detected
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Rock"))
        {
            playerAnimator.SetBool("IsStunned", true);  // Set the animation
            stunRemaining = maxStunTime;                // Start the stun clock
        }
    }

    // We could use these methods also
    private void OnCollisionStay2D(Collision2D collision)
    {

    }

    private void OnCollisionExit2D(Collision2D collision)
    {

    }
}
