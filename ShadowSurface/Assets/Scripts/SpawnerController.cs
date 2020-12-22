using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{

    private bool[] can_spawn;
    private Vector3[] spawn_dir;
    public GameObject enemy;
    private System.Random rnd = new System.Random();
    public float spawn_time;

    bool can_spawn_dir(int idx)
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, spawn_dir[idx], 1f);
        Debug.DrawRay(transform.position, spawn_dir[idx], Color.green);
        for (int i = 0; i < hits.Length; ++i)
        {
            if (hits[i].collider.gameObject == gameObject) continue;
            if (hits[i].collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                return false;
            }
        }
        return true;
    }

    void Spawn()
    {
        GameObject new_en = Instantiate(enemy);
        int rnd_dir = rnd.Next(0, can_spawn.Length);
        while(!can_spawn[rnd_dir])
        {
            rnd_dir = rnd.Next(0, can_spawn.Length);
        }
        new_en.transform.position = transform.position + spawn_dir[rnd_dir] * 1f;
    }

    // Start is called before the first frame update
    void Start()
    {
        spawn_dir = new Vector3[] {transform.up, transform.right, -transform.up, -transform.right};
        can_spawn = new bool[spawn_dir.Length];
        for (int i = 0; i < spawn_dir.Length; ++i)
        {
            can_spawn[i] = can_spawn_dir(i);
            Debug.Log(i.ToString() + can_spawn[i]);
        }
        InvokeRepeating("Spawn", spawn_time, spawn_time);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
