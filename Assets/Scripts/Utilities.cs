using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public static class Utilities
{

    public static List<int> GetRandomIntList(int start, int end)
    {
        List<int> orig_list = new List<int>();
        List<int> random_list = new List<int>();

        for (int i = start;i< end;i++)
        {
            orig_list.Add(i);

        }

        for (int i = start; i < end; i++)
        {
            int value = orig_list[Random.Range(0, orig_list.Count)];
            random_list.Add(value);
            orig_list.Remove(value);
        }


        return random_list;
    }



}


