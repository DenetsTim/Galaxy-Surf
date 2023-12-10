using UnityEngine;

public class MoveToPlayer : MonoBehaviour
{
    [SerializeField] private Vector3 offset = new Vector3(0, 0, 0);
    private void FixedUpdate()
    {
        transform.position = Player.player.transform.position + offset;
    }
}
