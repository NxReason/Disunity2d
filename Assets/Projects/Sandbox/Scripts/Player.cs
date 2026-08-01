using Shared.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Sandbox2d.Sandbox {
  public class Player : MonoBehaviour {
    Rigidbody2D rb;
    BoxCollider2D col;

    // Controls
    private TopdownControls controls;

    // Movement
    public Vector2 Move { get; private set; }
    [SerializeField] private float speed;
    ContactFilter2D filter = new ContactFilter2D();

    void Awake() {
      controls = new TopdownControls();

      filter.useLayerMask = false;
      filter.useTriggers = false;
    }
    void OnEnable() {
      controls.Player.Enable();

      controls.Player.Move.performed += OnMove;
      controls.Player.Move.canceled += OnMove;
    }

    void Start() {
      rb = GetComponent<Rigidbody2D>();
      if (rb == null) Debug.LogWarning("Rigidbody2D wasn't found on Player");
      col = GetComponent<BoxCollider2D>();
      if (col == null) Debug.LogWarning("BoxCollider2D wasn't found on Player");
    }

    void FixedUpdate() {
      if (Move != Vector2.zero) {
        Vector2 delta = Move * speed * Time.fixedDeltaTime;
        RaycastHit2D[] hits = new RaycastHit2D[5];
        var cast = col.Cast(delta, filter, hits, delta.magnitude);
        if (cast == 0) {
          transform.Translate(Move * speed * Time.deltaTime);
        }
      }
    }

    private void OnMove(InputAction.CallbackContext ctx) {
      Move = ctx.ReadValue<Vector2>();
    }
  }
}