using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearAttackTrigger : MonoBehaviour
{
    public List<GameObject> CurrentEnemys;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.name.Contains("Enemy"))
            return;
        if (CurrentEnemys.Find(x => x.gameObject == other.gameObject))
            return;
        CurrentEnemys.Add(other.gameObject);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.name.Contains("Enemy"))
            return;
        if (CurrentEnemys.Find(x => x.gameObject == collision.gameObject))
        {
            CurrentEnemys.Remove(collision.gameObject);
        }

    }
}
