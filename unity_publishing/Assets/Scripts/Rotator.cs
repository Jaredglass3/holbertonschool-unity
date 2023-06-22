using UnityEngine;

public class Rotator : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(new Vector3(45f, 0f, 0f) * Time.deltaTime);
    }
}
