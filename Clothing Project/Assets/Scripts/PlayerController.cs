using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Character character;

    void Update()
    {
        if (character.health > 0)
        {
            Vector2 movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            character.Move(movementInput.normalized, Input.GetKey(KeyCode.LeftShift));

            if (Input.GetMouseButton(0))
            {
                character.Attack();
            }
        }
    }
}
