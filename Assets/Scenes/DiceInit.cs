using UnityEngine;

public class DiceInitializer : MonoBehaviour
{
    void Start()
    {
        // サイコロの初期面をランダムに設定
        int randomFace = Random.Range(1, 7); // 1から6までのランダムな値
        transform.rotation = Quaternion.Euler(0f, 0f, 90f * randomFace); // サイコロの面に対応する角度を設定
    }
}

