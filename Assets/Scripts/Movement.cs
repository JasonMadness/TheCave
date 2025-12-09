using UnityEngine;

public class SimpleFPSMovement : MonoBehaviour
{
    [Header("Настройки движения")]
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _mouseSensitivity = 2f;
    [SerializeField] private float _maxLookAngle = 80f;
    
    [Header("Компоненты")]
    [SerializeField] private Camera _playerCamera;
    
    private float _verticalRotation = 0f;
    
    private void Start()
    {
        // Если камера не назначена в инспекторе, ищем её
        if (_playerCamera == null)
            _playerCamera = GetComponentInChildren<Camera>();
        
        // Скрываем и фиксируем курсор
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    private void Update()
    {
        HandleMouseLook();
        HandleMovement();
    }
    
    private void HandleMouseLook()
    {
        // Получаем ввод мыши
        float mouseX = Input.GetAxis("Mouse X") * _mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * _mouseSensitivity;
        
        // Поворачиваем игрока по горизонтали
        transform.Rotate(Vector3.up * mouseX);
        
        // Поворачиваем камеру по вертикали
        _verticalRotation -= mouseY;
        _verticalRotation = Mathf.Clamp(_verticalRotation, -_maxLookAngle, _maxLookAngle);
        _playerCamera.transform.localRotation = Quaternion.Euler(_verticalRotation, 0f, 0f);
    }
    
    private void HandleMovement()
    {
        // Получаем ввод с клавиатуры
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        
        // Вычисляем вектор движения
        Vector3 moveDirection = (transform.right * horizontal + transform.forward * vertical).normalized;
        
        // Перемещаем игрока
        transform.position += moveDirection * _moveSpeed * Time.deltaTime;
    }
}