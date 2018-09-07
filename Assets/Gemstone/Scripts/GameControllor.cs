using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControllor : MonoBehaviour {

    public GameStone gameStone;

    public int rowIndex=8;
    public int columIndex=8;

    public GameStone CurrentStone;//已经被选着的stone

    public GameStone Matches_temp_stone;
    public ArrayList gamestonlist=new ArrayList();

    private bool Is_Matche=false;

    public ArrayList MatchesGameStone=new ArrayList();
	// Use this for initialization
	void Start () {
       
        for (int temp_row = 0; temp_row < rowIndex; temp_row++)
        {
            ArrayList temp = new ArrayList();
            for (int temp_colum = 0; temp_colum < columIndex; temp_colum++)
            {
                //GameStone c = Instantiate(gameStone) as GameStone;
                //c.transform.parent = this.transform;
                //c.GetComponent<GameStone>().RandomCreateStone();
                //c.UpdatePosition(temp_row, temp_colum);
                
                temp.Add(AddStone(temp_row, temp_colum));
            }
            gamestonlist.Add(temp);

           
        }
    }

    public GameStone  AddStone(int temp_row, int temp_colum)
    {
        GameStone c = Instantiate(gameStone) as GameStone;
        c.transform.parent = this.transform;
        c.GetComponent<GameStone>().RandomCreateStone();
        c.UpdatePosition(temp_row, temp_colum);
        return c;
    }

    public void SelectStone(GameStone c)
        //选中stone 操作
    {
       // Destroy(c.gameObject);        //测试代码
        if (CurrentStone == null)
        {
            CurrentStone = c;
            CurrentStone.IsSelect = true;
        }
        else
        {
            c.IsSelect = true;
            //ExangePosition(CurrentStone, c);
            CurrentStone.IsSelect = false;
            c.IsSelect = false;
            Is_Matche = false;
            ExchangeMatches_Position(CurrentStone, c);           
            CurrentStone = null;
            
        }
    }

    void ExchangeMatches_Position(GameStone c1, GameStone c2)
    {
        ExangePosition(c1, c2);

        //if (Check_Row())
        //{
        //    Debug.Log("Removeing Row .........");
        //    RemoveMatches();
        //    if (Check_Colum())
        //    {
        //        Debug.Log("Removeing Colum ......");
        //        RemoveMatches();
        //    }
        //}
        Check_Colum();
        Check_Row();
        if (Is_Matche)
        {
            RemoveMatches();
        }       
        else
        {
            ExangePosition(c1, c2);
            Debug.Log("ReExange.......!");
        }

    }

    bool Check_Row()
    {
        for(int rownum = 0; rownum < rowIndex; rownum++)
        {
            for(int columnum = 0; columnum < columIndex-2; columnum++)
            {
               
                if (GetStone_List(rownum, columnum).stonetype == GetStone_List(rownum, columnum + 1).stonetype && GetStone_List(rownum, columnum).stonetype == GetStone_List(rownum, columnum + 2).stonetype)
                {
                    AddMatches(GetStone_List(rownum, columnum));
                    AddMatches(GetStone_List(rownum, columnum+1));
                    AddMatches(GetStone_List(rownum, columnum+2));
                    Debug.Log("发现行相同！");
                    Is_Matche = true;
                }
            }
        }
        return Is_Matche;
 
    }

    bool Check_Colum()
    {
        for (int columnum = 0; columnum < columIndex ; columnum++)
        {
            for (int rownum=0;rownum<rowIndex-2;rownum++)
            {
                if (GetStone_List(rownum, columnum).stonetype == GetStone_List(rownum+1, columnum ).stonetype && GetStone_List(rownum, columnum).stonetype == GetStone_List(rownum+2, columnum ).stonetype)
                {
                    AddMatches(GetStone_List(rownum, columnum));
                    AddMatches(GetStone_List(rownum+1, columnum));
                    AddMatches(GetStone_List(rownum+2, columnum));
                    Debug.Log("发现列相同！");
                    Is_Matche = true;
                }
            }
        }
        return Is_Matche;
    }

    void AddMatches(GameStone c)
    {
        if (MatchesGameStone ==null)
        {
            MatchesGameStone = new ArrayList();
        }
        int index = MatchesGameStone.IndexOf(c);
        if (index == -1)
        {
            MatchesGameStone.Add(c);
        }
    }
    void RemoveMatches()
    {
        for (int i = 0; i < MatchesGameStone.Count; i++)
        {
            GameStone c = MatchesGameStone[i] as GameStone;
            RemoveStone(c);
        }
        MatchesGameStone = new ArrayList();
    }
  
    void RemoveStone(GameStone c)
    {
        Debug.Log(c.rowIndex + "行" + c.columIndex+"列 宝石已经删除！");
        c.Dispose();
        for(int i=c.rowIndex; i < rowIndex-1; i++)
        {
            GameStone TempStone = GetStone_List(i + 1,c.columIndex);
            TempStone.rowIndex--;
            SetStone_List(TempStone.rowIndex, TempStone.columIndex, TempStone);
            TempStone.UpdatePosition(TempStone.rowIndex, TempStone.columIndex);
        }

        GameStone NewStone = AddStone(rowIndex, c.columIndex);
        NewStone.rowIndex--;
        SetStone_List(NewStone.rowIndex, NewStone.columIndex, NewStone);
        NewStone.UpdatePosition(NewStone.rowIndex, NewStone.columIndex);

    }
 
    public GameStone GetStone_List(int rowIndex,int columIndex)
    //通过row与colum得到指定位置的stone
    {
        ArrayList templist = gamestonlist[rowIndex] as ArrayList;
        GameStone tempstone = templist[columIndex] as GameStone;
        return tempstone;
    }

    public void SetStone_List(int rowIndex,int columIndex,GameStone c)
     //通过row与colum设置指定位置的stone
    {
        ArrayList templist = gamestonlist[rowIndex] as ArrayList;
        templist[columIndex] = c;
    }
    public void ExangePosition(GameStone currentstone,GameStone newstone)
    {
        
        //在数组改变对应位置的stone
        SetStone_List(currentstone.rowIndex,currentstone.columIndex,newstone );
        SetStone_List(newstone.rowIndex, newstone.columIndex, CurrentStone);

        //改变游戏界面下对应位置的stone
        int cur_row = currentstone.rowIndex;
        int cur_colum = currentstone.columIndex;
        int new_row = newstone.rowIndex;
        int new_colum = newstone.columIndex;
        //更新游戏界界面下对应位置的stone
        if ((Mathf.Abs(cur_row-new_row)+Mathf.Abs(cur_colum-new_colum))==1)
        {
            currentstone.UpdatePosition(new_row, new_colum);
            newstone.UpdatePosition(cur_row, cur_colum);
        }
        else
        {
            return;
        }

    }
	
	// Update is called once per frame
	void Update () {
        Check_Row();
        Check_Colum();
        RemoveMatches();
	}
}
