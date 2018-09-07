using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStone : MonoBehaviour {
    public GameControllor gameControllor;

    public int rowIndex = 0;
    public int columIndex = 0;

    public float x_offset = 0f;
    public float y_offset = 0f;
    public float z_offset = 0f;

    public GameObject[] stonelist;
    public static GameObject Test_01;
    public int stonetype;

  

    public SpriteRenderer spriteRenderer;
    public bool IsSelect
    {
        set
        {
            if (value)
            {
                spriteRenderer.color = Color.red;
            }
            else
            {
                spriteRenderer.color = Color.white;
            }
        }
    }

    public GameObject stonegb;
    // Use this for initialization

    private void Awake()
    {
        gameControllor = GameObject.Find("GameControllor").GetComponent<GameControllor>();
    }

    void Start()
    {
       
        //stonegb =Instantiate(stonelist[0])as GameObject;
       
       
        
    }  
    // Update is called once per frame
    void Update () {
       
    }

    public void UpdatePosition(int temp_row,int temp_colum)
    {
        rowIndex = temp_row;
        columIndex = temp_colum;
        this.transform.position = new Vector3((columIndex+x_offset)*1.5f, (rowIndex+y_offset)*1.4f, 0+z_offset);
    }

    public void RandomCreateStone()
    {
        if (stonegb != null)
        {
            
        }
        stonetype = Random.Range(0, stonelist.Length);
        stonegb = Instantiate(stonelist[stonetype]) as GameObject;
        stonegb.transform.parent = this.transform;

        spriteRenderer = stonegb.GetComponent<SpriteRenderer>();
    }

    public void OnMouseDown()
    {
       gameControllor.SelectStone(this);
        Debug.Log("row:" + this.rowIndex + "colum:" + this.columIndex);
        //print(this.rowIndex);
        //print(this.columIndex);       //测试代码
    }
    public void Dispose()
    {
        Destroy(this.gameObject);
        Destroy(stonegb.gameObject);
        gameControllor = null;
    }
}
