using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AxWMPLib;
using System.IO;

namespace videoPic
{
    public partial class Form1 : Form
    {
        private AxWindowsMediaPlayer axWindowsMediaPlayer1;

        public Form1()
        {
            InitializeComponent();
            InitVedio();  
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitVedioUrl();
            InitEvent(); 
        }

        private void InitVedio()
        {
            this.axWindowsMediaPlayer1 = new AxWMPLib.AxWindowsMediaPlayer();
            this.axWindowsMediaPlayer1.Enabled = true;
            this.axWindowsMediaPlayer1.Location = new System.Drawing.Point(10, 10);
            this.axWindowsMediaPlayer1.Name = "axWindowsMediaPlayer1";
            this.axWindowsMediaPlayer1.Size = new System.Drawing.Size(960, 500);
            this.axWindowsMediaPlayer1.TabIndex = 2;

            this.axWindowsMediaPlayer1.Anchor = AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Left;
            ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer1)).BeginInit();  
            this.Controls.Add(this.axWindowsMediaPlayer1);
            ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer1)).EndInit();  
        }
        //初始化播放控件的视频文件地址  
        protected void InitVedioUrl()
        {
            try
            {
                this.axWindowsMediaPlayer1.currentPlaylist.clear();
                DirectoryInfo file = new DirectoryInfo(@"source");//遍历指定文件夹下的文件
                foreach (FileInfo newFile in file.GetFiles())
                {
                    this.axWindowsMediaPlayer1.currentPlaylist.appendItem(this.axWindowsMediaPlayer1.newMedia(newFile.FullName));//把文件夹的视频和图片文件添加进来
                }
                axWindowsMediaPlayer1.settings.setMode("loop", true);   //循环播放
                axWindowsMediaPlayer1.Ctlcontrols.play();               //开始播放
                System.Media.SystemSounds.Beep.Play();                  //声音播放

            }
            catch (Exception ex)
            {

            }
        }


        protected void InitEvent()
        {
            axWindowsMediaPlayer1.StatusChange += new EventHandler(axWindowsMediaPlayer1_StatusChange);
        }

        //通过控件的状态改变，来实现视频循环播放  
        protected void axWindowsMediaPlayer1_StatusChange(object sender, EventArgs e)
        {
            //判断视频是否已停止播放  
            if ((int)axWindowsMediaPlayer1.playState == 1)
            {
                //停顿2秒钟再重新播放  
                System.Threading.Thread.Sleep(1000);
                //重新播放  
                axWindowsMediaPlayer1.Ctlcontrols.play();
            }
            else if ((int)axWindowsMediaPlayer1.playState == 3)
            {
                //这里我就不隐藏了，为了看效果
                //this.axWindowsMediaPlayer1.uiMode = "None";
                this.axWindowsMediaPlayer1.fullScreen = true;
            }
        
        }  


    }
}
