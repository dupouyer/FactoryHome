using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Globals {
    public static EntityManager entityManager = new EntityManager();
    public static PanelManager panelManager = new PanelManager();
    public static ConfigManager configManager = new ConfigManager();
    public static InputManager input;

    public static int LAYER_ENTITY = 8;
    public static int LAYER_MATERIAL = 9;
    public static int LAYER_HITBOX = 10;
    public static int LAYER_TRANSPORT = 11;

    public static int FLAG_TRANSPORT = -2;
    public static int FLAG_STATIC = -1;
    public static int FLAG_IDLE = 0;
}
