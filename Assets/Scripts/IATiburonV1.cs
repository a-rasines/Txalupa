using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IATiburonV1 : MonoBehaviour
{
    [SerializeField] GameObject tF;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        tF.transform.Rotate(0, 15 * Time.deltaTime, 0);
    }
}
