Summary_Graph
===============
## 1. 작업자
@Osongpodo
## 2. 작업 개요
새만금 Scene에 위치한 Clickable Object 옆에 요약 그래프를 그리는 기능 구현
![165900637-c49c5ece-1db4-4914-bcf2-f0dfa561e6c1](https://user-images.githubusercontent.com/73912947/167556893-e90af123-6444-446d-ba5b-e9db313beefe.png)
## 3. 작업 내용
```c#
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Summary_graph : MonoBehaviour
{
    public GameObject graph;
    public GameObject instance;
    public GameObject obj;

    public float value;
    public float minValue;
    public float maxValue;
    public float distance;
    private float total;
    private float per;
    private float rate;
    private float spec;
    private float y_plus;
    private float target;
    private Coroutine moveRoutine = null;

    public enum DIR { X, x, Z, z };
    // direction : X = +x, x = -x, Z = +z, z = -z

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

        // value 값을 반환 (min보다 작으면 min반환, max보다 크면 max반환)
        value = Mathf.Clamp(value, minValue, maxValue);
    }

    public Vector2 GetMinMax()
    {
        Vector2 g_scale = new Vector2(minValue, maxValue);
        return g_scale;
    }

    public void DrawGraph(float value, float distance, DIR direction)
    {
        this.value = value;
        this.distance = distance;

        if (!instance)
            instance = Instantiate(graph);

        Transform g_transform = instance.GetComponent<Transform>();
        Transform o_transform = obj.GetComponent<Transform>();
        Transform g_child_transform = g_transform.GetChild(0);
        Transform g_text = g_transform.GetChild(1);

        // 최대최소 범위에 따른 그래프의 비율
        total = maxValue - minValue;
        spec = value - minValue;
        rate = spec / total;
        y_plus = rate / 2;
        per = rate * 100;

        // 그래프의 변화하는 값
        g_child_transform.localScale = new Vector3(1.001f, rate, 1.001f);
        Vector3 y_pos = g_transform.position;
        g_child_transform.position = new Vector3(y_pos.x, y_pos.y - (g_transform.localScale.y / 2) + y_plus + y_pos.z);

        Vector3 o_position = o_transform.position;
        Vector3 new_position = new Vector3();

        // direction & distance
        switch (direction)
        {
            case DIR.X: new_position = new Vector3(o_position.x + distance, o_position.y, o_position.z); break;
            case DIR.x: new_position = new Vector3(o_position.x - distance, o_position.y, o_position.z); break;
            case DIR.Z: new_position = new Vector3(o_position.x, o_position.y, o_position.z + distance); break;
            case DIR.z: new_position = new Vector3(o_position.x, o_position.y, o_position.z - distance); break;
        }

        g_transform.position = new_position;

        //text
        g_text.GetChild(0).GetComponent<Text>().text = per + "%";
        g_text.GetChild(1).GetComponent<Text>().text = "최소 : " + minValue + "\n" + "최대 : " + maxValue;

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

                if(moveRoutine != null)
                    StopCoroutine(moveRoutine);

                moveRoutine = StartCoroutine(GraphMove(Random.Range(0, 101), 2, DIR.X));
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
    IEnumerator GraphMove(float target, float distance, DIR direction)
    {
        //증감 확인
        if (target - value > 0)
        {
            for (float i = value; i < target + 1; i++)
            {
                DrawGraph(i, distance, direction);
                yield return 0;
            }
        }
        else if (target - value < 0)
        {
            for (float i = value; i > target + 1; i--)
            {
                DrawGraph(i, distance, direction);
                yield return 0;
            }
        }    
    }
}

```
## 4. 작업 


https://user-images.githubusercontent.com/73912947/167557068-972ba7dd-538b-442d-9f65-bee20e5d827c.mp4

