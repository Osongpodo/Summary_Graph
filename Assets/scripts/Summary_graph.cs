using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summary_graph : MonoBehaviour
{
    public GameObject graph;
    public GameObject instance;
    public GameObject obj;

    private bool isActive = false;

    public float value;
    public float minValue;
    public float maxValue;
    private float total;
    private float per;
    private float rate;
    private float spec;
    private float y_plus;

    // X = +x, x = -x, Z = +z, z = -z
    public enum DIR { X, x, Z, z };

    void Start()
    {

    }

    void Update()
    {
        SetGraphEnable(true);
    }

    public void SetMinMax(float minValue, float maxValue)
    {

        this.minValue = minValue;
        this.maxValue = maxValue;
        value = Mathf.Clamp(value, minValue, maxValue);
        //value 값을 반환 (min보다 작으면 min반환, max보다 크면 max반환)

    }

    public Vector2 GetMinMax()
    {
        Vector2 g_scale = new Vector2(minValue, maxValue);
        return g_scale;
    }

    public void DrawGraph(float value, float distance, DIR direction)
    {
        this.value = value;

        instance = Instantiate(graph);


        Transform g_transform = instance.GetComponent<Transform>();
        Transform g_child_transform = g_transform.GetChild(0);
        Transform o_transform = obj.GetComponent<Transform>();

        //value
        //최대 최소 범위 내에서 특정값의 범위 구하기
        //총범위 = 최대값 - 최소값
        //특정값 = 범위내 값 - 최소값
        //비율 = 특정값 / 총범위
        total = maxValue - minValue;
        spec = value - minValue;
        rate = spec / total;
        y_plus = rate / 2;



        g_child_transform.localScale = new Vector3(1.001f, rate, 1.001f);

        Vector3 y_pos = g_transform.position;
        g_child_transform.position = new Vector3(y_pos.x, y_pos.y - (g_transform.localScale.y / 2) + y_plus + y_pos.z);
        Vector3 o_position = o_transform.position;
        Vector3 new_position = new Vector3();



        // direction 
        switch (direction)
        {
            case DIR.X: new_position = new Vector3(o_position.x + distance, o_position.y, o_position.z); break;
            case DIR.x: new_position = new Vector3(o_position.x - distance, o_position.y, o_position.z); break;
            case DIR.Z: new_position = new Vector3(o_position.x, o_position.y, o_position.z + distance); break;
            case DIR.z: new_position = new Vector3(o_position.x, o_position.y, o_position.z - distance); break;
        }

        g_transform.position = new_position;


    }

    public void SetGraphEnable(bool isActive)
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

                    DrawGraph(50, 2, DIR.X);
                }
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

    public void SetLabelEnable(bool isActive)
    {
        //per 에 따른 값 ui로 라벨
        //per * 100 하면 퍼센티지로 나옴
    }
}
