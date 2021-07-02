using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BepInEx;
using BepInEx.Logging;
using BepInEx.Configuration;
using UnityEngine;
using UnityEngine.UI;

namespace TestMod
{
    [BepInPlugin("UK.SPDRN", "Ultrakill Speedrunning tools", "1.0.0")]
    public class Plugin : BaseUnityPlugin
    {
        private ConfigEntry<Color> Col;
        private ConfigEntry<float> XSpeed;
        private ConfigEntry<float> YSpeed;
        private ConfigEntry<float> XAngle;
        private ConfigEntry<float> YAngle;
        private ConfigEntry<float> XPos;
        private ConfigEntry<float> YPos;
        private ConfigEntry<int> Font;
        private ConfigEntry<KeyCode> Toggle;
        GameObject Player;
        bool Display = false;

        public void Start()
        {
            Col = Config.Bind("General", "Color",Color.white,"Color of UI");
            Font = Config.Bind("General", "Font size",30,"Font Size");
            XSpeed = Config.Bind("Speedometer","X speed pos",0f,"X pos of Speedometer");
            YSpeed = Config.Bind("Speedometer","Y  speed pos",0f,"Y pos of Speedometer");
            XAngle = Config.Bind("Angle","X angle pos",0f,"X pos of Player's Angle");
            YAngle = Config.Bind("Angle","Y angle pos",20f,"Y pos of Player's Angle");
            XPos = Config.Bind("Pos","X position pos",0f,"X pos of Player's position");
            YPos = Config.Bind("Pos","Y position pos",40f,"Y pos of Player's position");
            Toggle = Config.Bind("Binds", "Toggle", KeyCode.CapsLock, "Key to toggle stats");
        }
        public void Update()
        {
            if (Player != null)
            {
                if (Input.GetKeyDown(Toggle.Value))
                {
                    Display = !Display;
                }
            }
            ObjCheck();
        }
        public void OnGUI()
        {
            if(Player != null)
            {
                if (Display)
                {
                    var PLA = Player.GetComponent<Rigidbody>();
                    var DSGN = new GUIStyle();
                    DSGN.fontSize = Font.Value;
                    DSGN.normal.textColor = Col.Value;
                    GUI.Label(new Rect(XSpeed.Value, YSpeed.Value, 300, 20), "Velocity(x,y,z): " + PLA.velocity, DSGN);
                    GUI.Label(new Rect(XAngle.Value, YAngle.Value, 300, 20), "Angle: " + Player.transform.eulerAngles.y, DSGN);
                    GUI.Label(new Rect(XPos.Value, YPos.Value, 300, 20), "Position(x,y,z): " + Player.transform.position, DSGN);
                }
            }
        }
        private void ObjCheck()
        {
            if(Player == null)
            {
                Player = GameObject.Find("Player");
            }
        }
    }
}
