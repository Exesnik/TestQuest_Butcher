using DG.Tweening;
using UnityEngine;

public class RotateAndMove : MonoBehaviour
{
    public float rotationSpeed = 360f; // Скорость вращения в градусах в секунду
    public float moveDistance = 0.5f; // Расстояние поднятия и опускания
    public float moveDuration = 1f; // Время для поднятия и опускания

    void Start()
    {
        // Начинаем бесконечное вращение
        transform.DORotate(new Vector3(0, rotationSpeed, 0), 1f, RotateMode.LocalAxisAdd)
                 .SetLoops(-1, LoopType.Incremental);

        // Начинаем колебание вверх и вниз
        float originalY = transform.position.y;
        transform.DOMoveY(originalY + moveDistance, moveDuration)
                 .SetEase(Ease.InOutSine)
                 .SetLoops(-1, LoopType.Yoyo);
    }
}
