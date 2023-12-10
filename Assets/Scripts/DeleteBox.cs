using UnityEngine;

public class DeleteBox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
    }

    private void Update()
    {
        transform.position = Player.player.transform.position + new Vector3(0, 0, -10);
    }
}
