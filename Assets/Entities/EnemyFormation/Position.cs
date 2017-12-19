using UnityEngine;
using System.Collections;

public class Position : MonoBehaviour {

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 1);
    }
}
