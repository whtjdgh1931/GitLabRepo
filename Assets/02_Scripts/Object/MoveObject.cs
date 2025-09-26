using DG.Tweening;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    public GameObject[] cars;

    public GameObject destroyEffect;

    public float minSpeed;
    public float maxSpeed;

    public void Awake()
    {
        DOTween.Init();
    }

    public void InitObject()
    {
        int rndCar = Random.Range(0, cars.Length);
        cars[rndCar].gameObject.SetActive(true);

        float speed = Random.Range(minSpeed, maxSpeed);

        transform.DOLocalMove(transform.localPosition - Vector3.back * speed, 3f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Incremental);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerInput>().mapMgr.GameOver();
        }
        else if (collision.collider.CompareTag("MoveObject"))
        {
            GameObject effect = Instantiate(destroyEffect,transform.position, Quaternion.identity);
            Destroy(effect, 1f);
            Destroy(gameObject);
        }
        else if(collision.collider.CompareTag("Spawner"))
            {
            Destroy(gameObject);

        }

    }

    private void OnDestroy()
    {
        transform.DOKill();
    }
}
