using UnityEngine;

public class BubbleGun : MonoBehaviour
{
    public Transform parent; 
    public GameObject prefab;
    public GameObject self; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(UnityEngine.Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) {
            Instantiate(prefab, parent);
            Destroy(self);
        }
    }
}
