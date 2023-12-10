using UnityEngine;

public class Trigger : MonoBehaviour
{
    private bool canGo = true;

    public bool checkPos() { return canGo; }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.isTrigger)
        {
            if (other.gameObject.GetComponent<Prep>())
                canGo = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (!other.isTrigger)
        {
            if (other.gameObject.GetComponent<Prep>())
                canGo = true;
        }
    }
}
