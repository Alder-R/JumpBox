using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Move : MonoBehaviour
{
    private Rigidbody rigid;

    public float jumpPower = 30f;
    public float speed = 5f;
    public int money;
    public Text scoreboard;

    bool isJumping;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        isJumping = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Jump();
        move();
        TurnBrake();
    }
    void move()
    {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            transform.Translate(-speed * Time.deltaTime, 0, speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            transform.Translate(speed * Time.deltaTime, 0, -speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            transform.Translate(speed * Time.deltaTime, 0, speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            transform.Translate(-speed * Time.deltaTime, 0, -speed * Time.deltaTime);
        }
    }
    void Jump()
    {
        if (Input.GetKey(KeyCode.Space) && !isJumping)
        {
            rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            isJumping = true; //�����ϸ� isJumping�� true��
        }
    }
    void TurnBrake()
    {
        if (transform.rotation.x != 0 || transform.rotation.y != 0 || transform.rotation.z != 0)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
    }
    void OnCollisionEnter(Collision playerCol)
    {
        // �ε��� ��ü�� �±װ� "Ground"���
        if (playerCol.gameObject.CompareTag("Ground"))
        {
            isJumping = false; //isJumping�� �ٽ� false��
        }

        if (playerCol.gameObject.CompareTag("RePlacer")) // "RePlacer" �±׿� ������
        {
            transform.position = new Vector3(-100, 3, 0); // ó�� ��ġ��
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("CoinTag"))
        {
            money += 1;
            scoreboard.text = ""+money;
        }
    }

}
