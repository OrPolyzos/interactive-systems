using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitSpawner : MonoBehaviour
{
    public Transform prefabEvilRabbitLeft;
    public Transform prefabEvilRabbitRight;
    public Transform prefabGoodRabbitLeft;
    public Transform prefabGoodRabbitRight;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        InvokeRepeating("InstantiateEvilRabbit", 0, 5);
        InvokeRepeating("InstantiateGoodRabbit", 0, 3);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void InstantiateEvilRabbit()
    {
        Instantiate(prefabEvilRabbitLeft, new Vector3(prefabEvilRabbitLeft.transform.position.x, prefabEvilRabbitLeft.transform.position.y, Random.Range(-120, -70)), prefabEvilRabbitLeft.transform.rotation);
        Instantiate(prefabEvilRabbitRight, new Vector3(prefabEvilRabbitRight.transform.position.x, prefabEvilRabbitRight.transform.position.y, Random.Range(-120, -70)), prefabEvilRabbitRight.transform.rotation);
    }

    void InstantiateGoodRabbit()
    {
        Instantiate(prefabGoodRabbitLeft, new Vector3(prefabGoodRabbitLeft.transform.position.x, prefabGoodRabbitLeft.transform.position.y, Random.Range(-120, -70)), prefabGoodRabbitLeft.transform.rotation);
        Instantiate(prefabGoodRabbitRight, new Vector3(prefabGoodRabbitRight.transform.position.x, prefabGoodRabbitRight.transform.position.y, Random.Range(-120, -70)), prefabGoodRabbitRight.transform.rotation);
    }
}
