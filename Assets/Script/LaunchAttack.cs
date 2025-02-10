using UnityEngine;

public class LaunchAttack : MonoBehaviour
{
    private Animator attackAnimator;
    private Attack attackScript;

    void Start()
    {
        //instantiate component
        attackAnimator = GetComponent<Animator>();
        attackScript = GetComponentInChildren<Attack>();

        if (attackAnimator != null)
        {
            Debug.Log("Animator found on: " + attackAnimator.gameObject.name);
            Debug.Log("Attack script found on: " + attackScript.gameObject.name);
        } else {
            Debug.LogError("Animator not found on Weapon");
        }

    }

    void Update()
        {
            if (Input.GetMouseButtonDown(0)) {
                attackScript.AimMousePoint();
                attackAnimator.SetTrigger("Attack");
            }
        }
}
