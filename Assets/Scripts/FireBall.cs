using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    //消灭时间
    public float killTime = 2f;
    public float Speed = 3;
    public int Damage = 10;
    float startTime = 0;
    Vector3 startTransformPosition;
    // Start is called before the first frame update
    void Start()
    {
        startTransformPosition = transform.forward;

    }

    // Update is called once per frame
    void Update()
    {
        startTime += Time.deltaTime;
        if (startTime >= killTime)
        {
            Destroy(gameObject);
        }
        transform.Translate(new Vector3(0,0,Time.deltaTime * Speed));
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.transform.GetComponent<CharacterStats>().TakeDamage(Damage);
        }
    }
}
