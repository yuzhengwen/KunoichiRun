using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnims : MonoBehaviour
{
    PlayerMovement playerMovement;
    PlayerData playerData;
    Animator animator;
    public enum AnimationState
    {
        Idle, Jump, Fall, Run, Death
    }
    public AnimationState state;


    // Start is called before the first frame update
    void Start()
    {
        // initialize components
        playerMovement = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();
        playerData= GetComponent<PlayerData>();

        // subscribe to player movement events
        playerMovement.onMove += runAnim;
        playerMovement.onJump += jumpAnim;
        playerMovement.onFall += fallAnim;
        playerMovement.onLand += idleAnim;
        playerMovement.onIdle += idleAnim;
        playerMovement.onFlip += flip;
        playerMovement.onFinish += idleAnim;

        playerData.onDeath += deathAnim;
        playerData.onHealthChange += healthChangeAnim;
    }

    private void OnDestroy()
    {

        playerMovement.onMove -= runAnim;
        playerMovement.onJump -= jumpAnim;
        playerMovement.onFall -= fallAnim;
        playerMovement.onLand -= idleAnim;
        playerMovement.onIdle -= idleAnim;
        playerMovement.onFlip -= flip;
        playerMovement.onFinish -= idleAnim;

        playerData.onDeath -= deathAnim;
        playerData.onHealthChange -= healthChangeAnim;

    }
    private void Update()
    {
    }

    // main animation functions ----------------
    private bool facingRight = true;
    private void flip(bool currDir)
    {
        // flip according to direction player is facing
        if (facingRight != currDir)
        {
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            facingRight = !facingRight;
        }
    }
    private void runAnim(PlayerMovement playerMovement)
    {
        // play run animation
        changeAnimationState(AnimationState.Run);
        Collider2DUtils.TryUpdateShapeToAttachedSprite(GetComponent<PolygonCollider2D>());
    }

    private void jumpAnim(PlayerMovement playerMovement)
    {
        // play jump animation only if playing is not moving horizontally
        if (playerMovement.getFacingDirection() == 0)
            changeAnimationState(AnimationState.Jump);
        Collider2DUtils.TryUpdateShapeToAttachedSprite(GetComponent<PolygonCollider2D>());
    }
    private void fallAnim(PlayerMovement playerMovement)
    {
        changeAnimationState(AnimationState.Fall);
        Collider2DUtils.TryUpdateShapeToAttachedSprite(GetComponent<PolygonCollider2D>());
    }

    private void idleAnim()
    {
        changeAnimationState(AnimationState.Idle);
        Collider2DUtils.TryUpdateShapeToAttachedSprite(GetComponent<PolygonCollider2D>());
    }

    private void deathAnim()
    {
        changeAnimationState(AnimationState.Death);
        Collider2DUtils.TryUpdateShapeToAttachedSprite(GetComponent<PolygonCollider2D>());
        // no more animations after death
        Destroy(this);
    }

    private void healthChangeAnim(GameObject @object, float hp, int change)
    {
        if (change == -1)
            StartCoroutine("FlashRed");
    }
    private IEnumerator FlashRed()
    {
        SpriteRenderer r = GetComponent<SpriteRenderer>();
        r.color = new Color(1f, 0f, 0f, .8f);
        yield return new WaitForSeconds(.2f);
        r.color = Color.white;
    }

    // utility functions -------------
    IEnumerator queueAnimation(AnimationState current, AnimationState next, float delay)
    {
        changeAnimationState(current);
        yield return new WaitForSeconds(delay);
        changeAnimationState(next);
    }

    private void changeAnimationState(AnimationState state)
    {
        if (this.state == state) return;

        animator.Play(state.ToString());
        this.state = state;

    }
    private bool isAnimationPlaying(AnimationState state)
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName(state.ToString())
            && animator.GetNextAnimatorStateInfo(0).normalizedTime < 1;
    }
    private bool isAnimationPlaying()
    {
        return animator.GetNextAnimatorStateInfo(0).normalizedTime < 1;
    }
}
