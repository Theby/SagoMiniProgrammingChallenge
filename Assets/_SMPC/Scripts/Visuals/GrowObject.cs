using System;
using UnityEngine;

public class GrowObject : MonoBehaviour
{
    [SerializeField] float startDelay;
    [SerializeField] Vector3 initialScale;
    [SerializeField] Vector3 targetScale;
    [SerializeField] AnimationCurve curve;
    [SerializeField] float speed;

    float _time = 0.0f;
    float _delayTimer = 0.0f;
    void Start()
    {
        transform.localScale = initialScale;
        _delayTimer = Time.time + startDelay;
    }

    void Update()
    {
        if (Time.time < _delayTimer)
            return;

        _time += Time.deltaTime * speed;
        transform.localScale = Vector3.LerpUnclamped(initialScale, targetScale, curve.Evaluate(_time));
    }
}
