using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BepInEx;
using BepInEx.Logging;
using BepInEx.Configuration;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        Rigidbody Player;
        bool Display;

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
        }
        public void Update()
        {
            if(SceneManager.GetActiveScene().name != "Intro")
            {
                if (Player != null)
                {
                    if (Input.GetKeyDown(KeyCode.Tab))
                    {
                        Display = !Display;
                    }
                }
                else
                {
                    ObjCheck();
                }
            }
        }
        public void OnGUI()
        {
            if(Player != null)
            {
                if (Display)
                {
                    var DSGN = new GUIStyle();
                    DSGN.fontSize = Font.Value;
                    DSGN.normal.textColor = Col.Value;

                    var HorSpeed = new Vector3(Player.velocity.x, 0, Player.velocity.z).magnitude;

                    GUI.Label(new Rect(XSpeed.Value, YSpeed.Value, 300, 20), $"Velocity Horizontal: {HorSpeed}", DSGN);
                    GUI.Label(new Rect(XSpeed.Value, YSpeed.Value+20, 300, 20), $"Velocity Vertical: {Player.velocity.y}", DSGN);
                    GUI.Label(new Rect(XAngle.Value, YAngle.Value, 300, 20), "Angle: " + Player.transform.eulerAngles.y, DSGN);
                    GUI.Label(new Rect(XPos.Value, YPos.Value, 300, 20), "Position(x,y,z): " + Player.transform.position, DSGN);
                }
            }
        }
        private void ObjCheck()
        {
            Player = NewMovement.Instance.gameObject.GetComponent<Rigidbody>();
        }
    }
}
