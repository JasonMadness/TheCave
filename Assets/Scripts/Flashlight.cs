using UnityEngine;

public class Flashlight : MonoBehaviour
{
    [Header("Параметры плавности")] 
    [SerializeField] private float _smoothSpeed = 8;   // Скорость плавности поворота фонарика

    private Vector3 _offsetFromCamera;           // Смещение фонарика от камеры
    private GameObject _followTarget;            // Объект, за которым следует фонарик (камера)
    private float _currentYaw;                   // Текущий горизонтальный угол (Y)
    private float _targetYaw;                    // Целевой горизонтальный угол
    private float _currentPitch;                 // Текущий вертикальный угол (X)
    private float _targetPitch;                  // Целевой вертикальный угол

    private void Start()
    {
        _followTarget = Camera.main.gameObject;
        _offsetFromCamera = transform.position - _followTarget.transform.position;

        // Инициализируем углы
        Vector3 eulerAngles = _followTarget.transform.eulerAngles;
        _targetYaw = eulerAngles.y;
        _currentYaw = _targetYaw;
        _targetPitch = eulerAngles.x;
        _currentPitch = _targetPitch;
    }

    private void LateUpdate()
    {
        // Обновляем целевые углы
        Vector3 eulerAngles = _followTarget.transform.eulerAngles;
        _targetYaw = eulerAngles.y;
        _targetPitch = eulerAngles.x;

        // Плавно интерполируем углы
        _currentYaw = Mathf.LerpAngle(_currentYaw, _targetYaw, _smoothSpeed * Time.deltaTime);
        _currentPitch = Mathf.LerpAngle(_currentPitch, _targetPitch, _smoothSpeed * Time.deltaTime);

        // Применяем новые углы
        Quaternion newRotation = Quaternion.Euler(_currentPitch, _currentYaw, 0);
        transform.rotation = newRotation;

        // Плавное движение позиции
        Vector3 desiredPosition = _followTarget.transform.position + _offsetFromCamera;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, _smoothSpeed * Time.deltaTime);
    }
}