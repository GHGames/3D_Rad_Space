﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _3DRadSpace
{
    public partial class ObjectsList : Form
    {
        public ObjectsList()
        {
            InitializeComponent();
        }

        private void ObjectsList_Load(object sender, EventArgs e)
        {
            pictureBox1.ImageLocation = @"data\resources\OBJ_Camera.png";
            pictureBox2.ImageLocation = @"data\resources\OBJ_Script.png";
            pictureBox3.ImageLocation = @"data\resources\OBJ_Sprite.png";
            pictureBox4.ImageLocation = @"data\resources\OBJ_TextPrint.png";
            pictureBox5.ImageLocation = @"data\resources\OBJ_EOL.png";
            pictureBox6.ImageLocation = @"data\resources\OBJ_EOK.png";
            pictureBox7.ImageLocation = @"data\resources\OBJ_Timer.png";
            pictureBox8.ImageLocation = @"data\resources\OBJ_Skinmesh.png";
            pictureBox9.ImageLocation = @"data\resources\OBJ_SkyColor.png";
            pictureBox10.ImageLocation = @"data\resources\OBJ_SkyBox.png";
            pictureBox11.ImageLocation = @"data\resources\OBJ_Sound.png";
            pictureBox12.ImageLocation = @"data\resources\OBJ_ExitFade.png";
            pictureBox13.ImageLocation = @"data\resources\OBJ_RigidBody.png";
            pictureBox14.ImageLocation = @"data\resources\OBJ_Force.png";
            pictureBox15.ImageLocation = @"data\resources\OBJ_GForce.png";
            pictureBox16.ImageLocation = @"data\resources\OBJ_GameSettings.png";
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Camera camera = new Camera();
            camera.ShowDialog();
            Close();
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            Skycolor skycolor = new Skycolor();
            skycolor.ShowDialog();
            Close();
        }
    }
}
