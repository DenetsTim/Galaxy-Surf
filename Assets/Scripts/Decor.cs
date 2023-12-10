using UnityEngine;

public class Decor : MonoBehaviour
{
    void Update()
    {
        transform.position += new Vector3(0, 0, -0.3f * Time.deltaTime);
    }
}
