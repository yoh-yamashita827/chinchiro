using UnityEngine;
using System.Collections;

public class DiceController : MonoBehaviour
{
    bool isRotating = false; 
    Vector3 initialPosition; // 初期位置を保存する変数
    Quaternion initialRotation; // 初期回転を保存する変数
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.useGravity = false; // 起動時には重力を無効にして浮かせる
            rb.drag = 0; // 空気抵抗を設定（落下速度調整に利用）
        }

        // 初期位置と回転を保存
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    void Update()
    {
        // スペースキーが押されたら回転、または落下
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isRotating)
            {
                float randomRotation = Random.Range(1f, 7f) * 360f;
                Vector3 randomAxis = Random.onUnitSphere;

                StartCoroutine(RotateDice(randomRotation, randomAxis));
            }
            else
            {
                // StopAllCoroutines();
                if (rb != null)
                {
                    rb.useGravity = true;
                    rb.drag = 0.1f;

                    float extraForceMultiplier = Random.value;
                    rb.AddForce(new Vector3(Random.Range(-5f, 5f), Random.Range(10f, 15f) * extraForceMultiplier, Random.Range(-5f, 5f)), ForceMode.Impulse);
                    rb.AddTorque(new Vector3(Random.Range(-50f, 50f), Random.Range(-50f, 50f), Random.Range(-50f, 50f)) * extraForceMultiplier, ForceMode.Impulse);

                    isRotating = false;

                }
            }

            isRotating = !isRotating; 
        }

        // Rキーが押されたら位置と回転をリセット
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetDice();
        }
    }

    IEnumerator RotateDice(float degrees, Vector3 axis)
    {
        while (true)
        {
            transform.Rotate(axis, degrees * Time.deltaTime, Space.World);
            yield return null;
        }
    }

    void ResetDice()
    {
        StopAllCoroutines(); // 回転を止める
        if (rb != null)
        {
            rb.useGravity = false; // 重力を無効にして浮かせる
            rb.velocity = Vector3.zero; // 速度をリセット
            rb.angularVelocity = Vector3.zero; // 回転速度をリセット
        }

        // 初期位置と回転に戻す
        transform.position = initialPosition;
        transform.rotation = initialRotation;
        isRotating = false; // 回転状態をリセット
    }
}
