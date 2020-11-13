using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterAnimator : MonoBehaviour
{
    public AnimationClip replaceableAttackAnim;
    public AnimationClip[] defaultAttackAnimSet;
    protected AnimationClip[] currentAttackAnimSet;

    const float LocomotionAnimationSmoothTime = .1f;
    NavMeshAgent agent;

    protected Animator animator;
    protected CharacterCombat combat;
    public AnimatorOverrideController overrideController;
    public CharacterStats stats;
    // Start is called before the first frame update
    protected virtual void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        combat = GetComponent<CharacterCombat>();
        stats = GetComponent<CharacterStats>();
        if (overrideController == null)
        {
            overrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
        }
        animator.runtimeAnimatorController = overrideController;
        currentAttackAnimSet = defaultAttackAnimSet;
        combat.OnAttack += OnAttack;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (stats.isDead)
        {
            return;
        }
        float speedPercent = agent.velocity.magnitude / agent.speed;
        animator.SetFloat("speedPercent", speedPercent, LocomotionAnimationSmoothTime, Time.deltaTime);
        animator.SetBool("inCombat", combat.InCombat);
    }

    protected virtual void OnAttack()
    {
        animator.SetTrigger("attack");
        int attackIndex = Random.Range(0, currentAttackAnimSet.Length);
        overrideController[replaceableAttackAnim.name] = currentAttackAnimSet[attackIndex];
    }
}
