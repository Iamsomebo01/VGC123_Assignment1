using UnityEngine;
using UnityEngine.UIElements;

public class cameraFollow : MonoBehaviour
{
    public float minXPos;
    public float maxXPos;
    public Transform target;
    public float cameraSpeed = 5;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (target == null)
        {
            GameObject Player = GameObject.FindGameObjectWithTag("Player");
            if (Player != null)
            {
                Debug.Log("No Target Found assigned as player. Please include valid reference in the camera follow script.");
                return;
            }
            target = Player.transform;
        }


    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (target == null) // If no target is assigned, exit the function
        {
            return;
        }
        Vector3 currentPos = transform.position;
        currentPos.x = Mathf.Clamp(target.position.x, minXPos, maxXPos);
        transform.position = Vector3.MoveTowards(transform.position, currentPos, cameraSpeed* Time.deltaTime);

    }
}
