using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowBall : MonoBehaviour
{
    // https://www.youtube.com/watch?v=BwPT7huwjn4
    // https://learn.unity.com/project/roll-a-ball

    public GameObject target;
    public float xOffset;
    public float yOffset;
    public float zOffset;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = target.transform.position + new Vector3(xOffset, yOffset, zOffset);
    }

    // Update is called once per frame
    void Update()
    {
        /*transform.position = target.transform.position + new Vector3(xOffset, yOffset, zOffset);*/
        transform.LookAt(target.transform.position);

        /*transform.Translate(Vector3.right * Time.deltaTime);*/

        float distance = Vector3.Distance(this.transform.position, target.transform.position);

        if (distance > 7 )
        {
            /*Vector3 dir = (this.transform.position - target.transform.position).normalized;
            Debug.Log(dir);
            Vector3 desiredPosition = dir * 5 + target.transform.position;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, 0.5f);
            transform.position = smoothedPosition;*/
        }
    }

    private void FixedUpdate()
    {
        /*Vector3 dir = (this.transform.position - target.transform.position).normalized;
        Vector3 desiredPosition = dir * 5 + target.transform.position;
        Debug.Log(desiredPosition);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, 0.25f);
        transform.position = smoothedPosition;*/

        /*this.transform.position = Vector3.MoveTowards(this.transform.position, target.transform.position, -(7f - Vector3.Distance(this.transform.position, target.transform.position)));*/
    }
}
