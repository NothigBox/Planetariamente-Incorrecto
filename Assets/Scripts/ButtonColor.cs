using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ButtonColor : MonoBehaviour
{
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        animator.Play("ButtonColorAnimation", 0, Random.Range(0f, 1f));
    }
}
