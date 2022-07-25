using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalValue : MonoBehaviour
{
    public static List<Elements> allElements = new List<Elements>();
    public static Cell startCell, finishCell;
    public static bool gameStart = false;
    public static float lvl_bar_value = 20, start_time_bar = 100 ;
}
