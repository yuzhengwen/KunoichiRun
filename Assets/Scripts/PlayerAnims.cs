using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnims : MonoBehaviour
{
    PlayerMovement playerMovement;
    Animator animator;
    public enum AnimationState
    {
        Idle, Jump, Run
    }
    public AnimationState state;


    // Start is called before the first frame update
    void Start()
    {
        // initialize components
        playerMovement = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();

        // subscribe to player movement events
        playerMovement.onJump += jumpAnim;
        playerMovement.onMove += runAnim;
        playerMovement.onLand += idleAnim;
        playerMovement.onIdle += idleAnim;
    }
    private void Update()
    {
    }

    private void runAnim(PlayerMovement playerMovement)
    {
        // flip according to direction player is facing
        if (playerMovement.getFacingDirection()>0)
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        else if (playerMovement.getFacingDirection()<0)
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        // play run animation
        changeAnimationState(AnimationState.Run);
    }

    private void jumpAnim(PlayerMovement playerMovement)
    {
        // play jump animation only if playing is not moving horizontally
        if (playerMovement.getFacingDirection() == 0)
            changeAnimationState(AnimationState.Jump);
    }

    private void idleAnim(PlayerMovement playerMovement)
    {
        changeAnimationState(AnimationState.Idle);
    }
    IEnumerator queueAnimation(AnimationState current, AnimationState next, float delay)
    {
        changeAnimationState(current);
        yield return new WaitForSeconds(delay);
        changeAnimationState(next);
    }

    public void changeAnimationState(AnimationState state)
    {
        if (this.state == state) return;

        animator.Play(state.ToString());
        this.state = state;

    }
    private bool isAnimationPlaying(AnimationState state)
    {
        Debug.Log(animator.GetNextAnimatorStateInfo(0).normalizedTime);
        return animator.GetCurrentAnimatorStateInfo(0).IsName(state.ToString())
            && animator.GetNextAnimatorStateInfo(0).normalizedTime < 1;
    }
    private bool isAnimationPlaying()
    {
        return animator.GetNextAnimatorStateInfo(0).normalizedTime < 1;
    }
}
