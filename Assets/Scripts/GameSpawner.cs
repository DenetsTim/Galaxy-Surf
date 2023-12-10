using UnityEngine;
using System.Collections.Generic;

public class GameSpawner : MonoBehaviour
{
    [SerializeField] private GameObject asteroid_go;
    [SerializeField] private GameObject oblomok_go;
    [SerializeField] private GameObject path_go;

    private float timer;
    [SerializeField] private GameObject asteroid_decor;
    [SerializeField] private GameObject oblomok_decor;

    private void Start()
    {
        Spawn(10);
    }

    public void Spawn(int z)
    {
        Instantiate<GameObject>(path_go, new Vector3(0, 0.3f, z), Quaternion.identity, transform.parent);

        int max_z = z + 100;
        while(z < max_z)
        {
            Spawner(z);
            z += 10;
        }
    }

    private void Spawner(int z)
    {
        List<float> pos_variations = new List<float>{ -1.75f, 0, 1.75f };
        List<GameObject> go_variations = new List<GameObject>{ asteroid_go, asteroid_go, asteroid_go, oblomok_go, oblomok_go };

        int col = Random.Range(1, 4);

        for (int i = 0; i < col; i++)
        {
            int var_num = Random.Range(0, go_variations.Count);
            GameObject go = go_variations[var_num];
            go_variations.Remove(go);

            int pos_num = Random.Range(0, pos_variations.Count);
            float pos = pos_variations[pos_num];
            pos_variations.Remove(pos);

            Instantiate<GameObject>(go, new Vector3(pos, 0.5f, z), Quaternion.identity, transform.parent);
        }
    }

    private void SpawnDecor()
    {
        GameObject[] GOs = { asteroid_decor, oblomok_decor };
        int[] random_pos = { -1, 1 };

        Instantiate<GameObject>(GOs[Random.Range(0, 2)], new Vector3(Random.Range(3.85f, 5f) * random_pos[Random.Range(0, 2)], Random.Range(-2f, 4f), Player.player.transform.position.z + 20f + Random.Range(20f, 50f)), new Quaternion(Random.Range(0, 360f), Random.Range(0, 360f), Random.Range(0, 360f), 0), transform.parent);
    }

    private void Update()
    {
        if(timer <= 0 && Player.player.IsGame)
        {
            timer = Random.Range(0, 3f);
            SpawnDecor();
        }

        timer -= Time.deltaTime;
    }
}
