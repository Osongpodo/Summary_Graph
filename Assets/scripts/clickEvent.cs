using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clickEvent : MonoBehaviour
{

    public GameObject graph;
    public GameObject instance;

    static public float graph_dist = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        click();
    }


    public void click()
    {



        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;


        if (Input.GetMouseButtonDown(0))
        {

            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("Left-Clicked");

                if (!instance)
                {
                    instance = Instantiate(graph);
                }

                Vector3 center_p = GetComponent<Transform>().position;
                //Debug.Log(center_p.x);
                
                Vector3 s_half = (GetComponent<Transform>().localScale)/2;
                //Debug.Log(s_half.x);
                
                var dist =(center_p.x + s_half.x + graph_dist);
                //Debug.Log(dist);

                instance.transform.position = new Vector3(center_p.x + dist, center_p.y, center_p.z);
                       
            }
            
        }

        else if (Input.GetMouseButtonDown(1))
        {
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("Right-Clicked");

                if (instance)
                {
                    instance.GetComponent<Renderer>().enabled = false;
                    Destroy(instance);
                }
               
                   

            }

        }


    }
}
