using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyMovement : MonoBehaviour
{

    public AIPath aIPath;

    // Update is called once per frame
    void Update()
    {
        Vector3 oldScale = transform.localScale;
        if (aIPath.desiredVelocity.x >= 0.01f)
        {
            if (oldScale.x <= 0)
            {
                transform.localScale = new Vector3(-1 * oldScale.x, oldScale.y, oldScale.z);
            }
            // Flip();
        }
        else if (aIPath.desiredVelocity.x <= -0.01f)
        {
            if (oldScale.x >= 0)
            {
                transform.localScale = new Vector3(-1 * oldScale.x, oldScale.y, oldScale.z);
            }
            // Flip();
        }
    }

    void Flip()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
