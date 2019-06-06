using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToBigger : MonoBehaviour
{
    public Vector2 toSize;
    public float time;
    public float blankTime;

    void Start()
    {
        this.StartCoroutine(this.bigger());
    }

    private IEnumerator bigger()
    {
        float ctime = 0;
        Vector2 dsize = (this.toSize - (Vector2)this.transform.localScale) / (this.time / 0.1f);

        yield return new WaitForSeconds(this.blankTime);
        while (ctime < this.time)
        {
            this.transform.localScale += (Vector3)dsize;
            yield return new WaitForSeconds(0.1f);

            ctime += 0.1f;
        }
    }
}
