using DG.Tweening;
using UnityEngine;

public class RotateAndMove : MonoBehaviour
{
    public float rotationSpeed = 360f; // �������� �������� � �������� � �������
    public float moveDistance = 0.5f; // ���������� �������� � ���������
    public float moveDuration = 1f; // ����� ��� �������� � ���������

    void Start()
    {
        // �������� ����������� ��������
        transform.DORotate(new Vector3(0, rotationSpeed, 0), 1f, RotateMode.LocalAxisAdd)
                 .SetLoops(-1, LoopType.Incremental);

        // �������� ��������� ����� � ����
        float originalY = transform.position.y;
        transform.DOMoveY(originalY + moveDistance, moveDuration)
                 .SetEase(Ease.InOutSine)
                 .SetLoops(-1, LoopType.Yoyo);
    }
}
