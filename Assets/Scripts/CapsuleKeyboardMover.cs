using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

public class CapsuleKeyboardMover : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private bool faceMoveDirection = true;

    private void Update()
    {
        Vector2 input = ReadMoveInput();
        if (input.sqrMagnitude > 1f)
        {
            input.Normalize();
        }

        Vector3 movement = new Vector3(input.x, 0f, input.y);
        transform.position += movement * moveSpeed * Time.deltaTime;

        if (faceMoveDirection && movement.sqrMagnitude > 0.0001f)
        {
            transform.rotation = Quaternion.LookRotation(movement, Vector3.up);
        }
    }

    private static Vector2 ReadMoveInput()
    {
        Vector2 input = Vector2.zero;

#if ENABLE_INPUT_SYSTEM
        Keyboard keyboard = Keyboard.current;
        if (keyboard != null)
        {
            if (keyboard.aKey.isPressed || keyboard.leftArrowKey.isPressed)
            {
                input.x -= 1f;
            }
            if (keyboard.dKey.isPressed || keyboard.rightArrowKey.isPressed)
            {
                input.x += 1f;
            }
            if (keyboard.sKey.isPressed || keyboard.downArrowKey.isPressed)
            {
                input.y -= 1f;
            }
            if (keyboard.wKey.isPressed || keyboard.upArrowKey.isPressed)
            {
                input.y += 1f;
            }
        }
#elif ENABLE_LEGACY_INPUT_MANAGER
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
#endif

        return input;
    }
}
