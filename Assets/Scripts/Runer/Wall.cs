using System.Collections;
using UnityEngine;

public class Wall : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(DropWall());
        }
    }

    private IEnumerator DropWall()
    {
        Vector3 newPos = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z + 1);
        do
        {
            transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(90, 0, 0), Time.deltaTime * 2f);
            yield return null;
        } while (transform.rotation.eulerAngles.x < 90);
    }
}
