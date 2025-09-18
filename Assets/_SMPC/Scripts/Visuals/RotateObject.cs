using UnityEngine;

public class RotateObject : MonoBehaviour
{
    [SerializeField] Vector3 angleSpeed;

    void Update()
    {
        transform.Rotate(Time.deltaTime * angleSpeed);
    }
}
