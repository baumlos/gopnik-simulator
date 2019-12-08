using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalVariables {
    public static float vodka_level = 500;
    public static float max_vodka_level = 1000;

    public static void addVodka(int amount) {
        vodka_level = Mathf.Min(vodka_level+amount, max_vodka_level);
    }
}
