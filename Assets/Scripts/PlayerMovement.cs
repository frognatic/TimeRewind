using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 20f;

    private float x;
    private float z;
    private Vector3 movement;
    
    private void Update()
    {
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");
        movement = new Vector3(x, 0, z);
        
        transform.Translate(movement * speed * Time.deltaTime);
    }
}
