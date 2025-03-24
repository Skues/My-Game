using UnityEngine;

public class KnifeAttack : MonoBehaviour
{
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left-click for attack
        {
            anim.SetTrigger("Swing"); // This activates the animation
        }
    }
}