using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public static Action attackInput;

    [SerializeField] private Animator animator;

    private Weapon weapon;

    // Start is called before the first frame update
    void Start()
    {
        weapon = FindObjectOfType<Weapon>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (GetComponent<UIHealthBar>().health > 0)
            {
                Debug.Log("Input registered");
                attackInput?.Invoke();

                StartCoroutine(DoWeaponAnim());
            }
            
        }
        else
        {
            weapon.shooting = false;
        }
    }


    // delays
    private IEnumerator DoWeaponAnim()
    {
        animator.SetBool("FireWeapon", true);

        // Gets how long the animation is and waits for that length
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        animator.SetBool("FireWeapon", false);
    }
}
