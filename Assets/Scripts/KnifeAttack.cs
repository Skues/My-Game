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
        else if (Input.GetMouseButtonDown(1)) // Left-click for attack
        {
            anim.SetTrigger("Stab"); // This activates the animation
        }
        float rotation = transform.eulerAngles.x;
        Mathf.Clamp(rotation, 352, 6);
    }
}