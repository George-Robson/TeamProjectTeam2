using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockChecker : MonoBehaviour
{
    private GameManager gm;
    public string condition;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("Game Manager").transform.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gm.FindState(condition))
        {
            Destroy(this.gameObject);
        }
    }
}
