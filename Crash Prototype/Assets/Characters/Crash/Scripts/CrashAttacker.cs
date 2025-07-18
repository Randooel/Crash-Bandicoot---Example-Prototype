using UnityEngine;

public class CrashAttacker : PlayerMovement
{
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("Attack");
        }
    }

    protected override void EspecialAttack()
    {
        base.EspecialAttack();

        animator.SetTrigger("Kamehameha");
    }
}
