using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private FloatingJoystick joystick;
    [SerializeField] private Animator animator;

    private Vector3 direct;
    private float speed;
    private bool isMoving;
    void Start()
    {
        animator.SetBool("IsIdle", true);
        isMoving = false;
        speed = 5;
    }

    void Update()
    {
        if (!isMoving && Input.GetMouseButtonDown(0))
        {
            //animator.SetBool("IsAttack", true);
            Debug.Log(0);
        }
        direct = new Vector3(joystick.Horizontal,0,joystick.Vertical);
        //Debug.Log(direct);

        
        if (Vector3.Distance(direct, Vector3.zero) >= 0.00001f)
        {
            transform.position += direct * speed * Time.deltaTime;
            transform.rotation = Quaternion.LookRotation(direct);
            animator.SetBool("IsUlti", true);
            animator.SetBool("IsIdle", false);
            isMoving = true;
        }
        else
        {
            animator.SetBool("IsUlti", false);
            animator.SetBool("IsIdle", true);       
            isMoving = false;
        }
    }
}
