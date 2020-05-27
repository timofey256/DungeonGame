using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackSystem : MonoBehaviour
{
    [SerializeField] private Transform attackPoint;
    [SerializeField] [Range(0.1f, 1f)] private float attackRange;
    [SerializeField] private LayerMask hitObjectsLayer;
    
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        PlayerAttack();
    }

    // Function that realize all attack system:
    private void PlayerAttack()
    {
        StaticAttack();
    }

    // Used when player staying on place:
    private void StaticAttack()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            StartCoroutine(SetFirstAttackAnimation());         
            AttackDetection();
        }
    }

    // Detection enemies which player hitted:
    private void AttackDetection()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, hitObjectsLayer); 
        
        foreach (Collider2D enemy in hitEnemies)
        {
            print($"Player hit: " + enemy.GetComponent<Collider>().name);
        }
    }

    // Function that realize first attack animation:
    private IEnumerator SetFirstAttackAnimation()
    {
        animator.SetBool("isAttack1", true);
        yield return new WaitForSeconds(0.45f);
        animator.SetBool("isAttack1", false);
    }

    // Used to show attackRange radius for setting up this field:
    private void OnDrawGizmosSelected()
    {
        if (!attackPoint)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);   
    }
}
