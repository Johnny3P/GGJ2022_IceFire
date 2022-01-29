using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayerTest : MonoBehaviour
{

    [SerializeField] private GameObject explosionPrefab;
    private Animator animator;

    // Start is called before the first frame update
    void Start() {
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.U)) {
            Instantiate(explosionPrefab, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z - 0.5f), Quaternion.identity);

            animator.SetTrigger("Death");
        }
    }
}
