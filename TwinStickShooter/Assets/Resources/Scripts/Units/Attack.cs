using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Attack : MonoBehaviour
{

    [SerializeField]
    private GameObject attackVolume;
    private NavMeshAgent agent;
    private Animator animator;
    private float attackSpeed = 1.0f;

    [SerializeField]
    private float attackRange = 2.0f;
    public float RANGE { get { return attackRange; } set { attackRange = value; } }
    [SerializeField]
    private float damageValue = 1.0f;
    public float DAMAGE { get { return damageValue; } set { damageValue = value; } }

    
    // Use this for initialization
    void Start()
    {
        agent = transform.root.gameObject.GetComponentInChildren<NavMeshAgent>();
        animator = transform.root.gameObject.GetComponentInChildren<Animator>();

        attackVolume.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DoAttack()
    {
        StartCoroutine("attack");
    }

    IEnumerator attack()
    {
        if (agent)
            agent.Stop();

        if (attackVolume)
            attackVolume.SetActive(true);

        if (animator)
        {
            animator.SetTrigger("attack01");

            AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);
            attackSpeed = info.length;
        }

        yield return new WaitForSeconds(attackSpeed);

        if (attackVolume)
            attackVolume.SetActive(false);

        if (agent)
            agent.Resume();

        yield return null;
    }

}
