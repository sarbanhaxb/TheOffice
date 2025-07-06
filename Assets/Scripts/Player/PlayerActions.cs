using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerActions : MonoBehaviour
{
    [SerializeField] private InputActionReference _clickAction;
    [SerializeField] private LayerMask _npcLayer; // Слой для NPC (опционально)
    private Canvas _targetCanvas;
    private Camera _mainCamera;
    private NPC_Visual _selectedObject;

    private void Awake()
    {
        _mainCamera = Camera.main;
        _clickAction.action.Enable();
    }

    private void OnEnable() => _clickAction.action.performed += OnClick;
    private void OnDisable() => _clickAction.action.performed -= OnClick;

    private void OnClick(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            // Получаем позицию мыши в мировых координатах (X, Y)
            Vector2 mouseWorldPos = _mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());

            // Проверяем, есть ли коллайдер в этой точке
            Collider2D hit = Physics2D.OverlapPoint(mouseWorldPos, _npcLayer);

            // Визуализация для отладки (появляется в Scene View)
            Debug.DrawRay(mouseWorldPos, Vector2.right * 0.1f, Color.green, 1f);

            if (hit != null && hit.CompareTag("Selectable"))
            {
                _targetCanvas = hit.GetComponentInChildren<Canvas>(true);
                _targetCanvas.enabled = true;
                _selectedObject = hit.GetComponent<NPC_Visual>();
                _selectedObject.HighLightObjectOn();

                //Debug.Log("NPC clicked: " + hit.GetComponentsInChildren<Canvas>();
                // Здесь можно вызвать метод NPC, например:
                // hit.GetComponent<NPC>().OnClick();
            }
            else
            {
                if (_targetCanvas != null)
                {
                    _targetCanvas.enabled = false;
                    _targetCanvas = null;
                    _selectedObject.HighLightObjectOff();
                }
                Debug.Log("Клик не попал в NPC. Pos: " + mouseWorldPos);
            }
        }
    }
}