using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerMovement : MonoBehaviour

{

    public float moveSpeed = 5;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()

    {

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
            animator.SetInteger("change", 4);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
            animator.SetInteger("change", 5);
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(Vector2.up * moveSpeed * Time.deltaTime);
            animator.SetInteger("change", 3);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(Vector2.down * moveSpeed * Time.deltaTime);
            animator.SetInteger("change", 2);
        }
        else
        {
            animator.SetInteger("change", 1);
        }
    }

}