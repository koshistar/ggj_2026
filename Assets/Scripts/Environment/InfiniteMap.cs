using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteMap : MonoBehaviour
{
    [SerializeField] private Transform player;

    [SerializeField] private float width = 19.2f * 16 / 9;
    [SerializeField] private float height = 10.8f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.x - transform.position.x > width * 0.5f)
        {
            transform.position += Vector3.right * width;
        }
        else if (player.transform.position.x - transform.position.x < -width * 0.5f)
        {
            transform.position += Vector3.left * width;
        }

        if (player.transform.position.y - transform.position.y > height * 0.5f)
        {
            transform.position += Vector3.up * height;
        }
        else if (player.transform.position.y - transform.position.y < -height * 0.5f)
        {
            transform.position += Vector3.down * height;
        }
    }
}
