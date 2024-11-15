using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField]
    private Image ProgressImage;
    [SerializeField]
    public float DefaultSpeed = 1f;
    [SerializeField]
    private Gradient ColorGradient;
    [SerializeField]
    private UnityEvent<float> OnProgress;
    [SerializeField]
    private UnityEvent OnCompleted;

    private Coroutine AnimationCoroutine;


    private void Start()
    {
        if (ProgressImage.type != Image.Type.Filled)
        {
            Debug.LogError($"{name}'s ProgressImage is not of type \"Filled\" so it cannot be used as a progress bar. Disabling this Progress Bar.");
            enabled = false;
#if UNITY_EDITOR
            EditorGUIUtility.PingObject(this.gameObject);
#endif
        }
    }

    public void SetProgress(float Progress, float Speed = -1f)
    {
        if (!gameObject.activeInHierarchy)
        {
            Debug.LogWarning("ProgressBar is inactive. Cannot start progress animation.");
            return;
        }

        if (Progress < 0 || Progress > 1)
        {
            Debug.LogWarning($"Invalid progress passed, expected value is between 0 and 1, got {Progress}. Clamping.");
            Progress = Mathf.Clamp01(Progress);
        }

        if (Progress != ProgressImage.fillAmount)
        {
            if (AnimationCoroutine != null)
            {
                StopCoroutine(AnimationCoroutine);
            }

            float actualSpeed = Speed > 0 ? Speed : DefaultSpeed; // Если скорость не указана, берём DefaultSpeed
            AnimationCoroutine = StartCoroutine(AnimateProgress(Progress, actualSpeed));
        }
    }


    private IEnumerator AnimateProgress(float Progress, float Speed)
    {
        float time = 0;
        float initialProgress = ProgressImage.fillAmount;

        while (time < 1)
        {
            ProgressImage.fillAmount = Mathf.Lerp(initialProgress, Progress, time);
            time += Time.deltaTime * Speed;

            ProgressImage.color = ColorGradient.Evaluate(1 - ProgressImage.fillAmount);

            OnProgress?.Invoke(ProgressImage.fillAmount);
            yield return null;
        }

        ProgressImage.fillAmount = Progress;
        ProgressImage.color = ColorGradient.Evaluate(1 - ProgressImage.fillAmount);

        OnProgress?.Invoke(Progress);
        OnCompleted?.Invoke();
    }

    private void OnDisable()
    {
        if (AnimationCoroutine != null)
        {
            StopCoroutine(AnimationCoroutine);
            AnimationCoroutine = null;
        }
    }
}