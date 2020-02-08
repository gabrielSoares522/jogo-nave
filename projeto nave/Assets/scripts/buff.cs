using UnityEngine;

public class buff : MonoBehaviour {
    public int codBuff;

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag != "tiro") {
            Destroy(gameObject);
        }
    }
}
