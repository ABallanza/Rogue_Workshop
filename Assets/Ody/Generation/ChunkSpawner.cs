using System.Collections;
using UnityEngine;

public class ChunkSpawner : MonoBehaviour
{

    public enum ChunkType { ground, air, goUp }
    public ChunkType chunkType;

    public int index;

    private void Start()
    {
        StartCoroutine("LoadChunks");
    }


    IEnumerator LoadChunks()
    {

        yield return new WaitForSeconds(2f);

        RaycastHit hit;

        if(Physics.Raycast(new Vector3(transform.position.x, transform.position.y - 3, transform.position.z), Vector3.right, out hit, 8f))
        {
            if(hit.transform != null)
            {
                chunkType = ChunkType.goUp;
            }
        }

        SpawnChunks();
    }

    void SpawnChunks()
    {
        if(chunkType == ChunkType.ground)
        {
            GameObject c = Instantiate(Generator.Instance.normalChunks[Random.Range(0, Generator.Instance.normalChunks.Length)], transform.position, Quaternion.identity);
            c.transform.SetParent(Generator.Instance.transform);
        }
        else
        {
            GameObject c = Instantiate(Generator.Instance.goUpChunks[Random.Range(0, Generator.Instance.goUpChunks.Length)], transform.position, Quaternion.identity);
            c.transform.SetParent(Generator.Instance.transform);
        }
    }


}
