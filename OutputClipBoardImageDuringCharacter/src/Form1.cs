﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing.Imaging;

namespace OutputClipBoardImageDuringCharacter
{
    public partial class Form1 : Form
    {
        public string pythonInterpreterPath = @"C:\Users\Yuki\AppData\Local\Programs\Python\Python37\python.exe";
        public string pythonScriptPath = @"G:\マイドライブ\ドライブ\IT系\プログラミング\開発\ToolApplications\OutputClipBoardImageDuringCharacter\app\main.py";
        public string saveImageName = @"G:\マイドライブ\ドライブ\IT系\プログラミング\開発\ToolApplications\OutputClipBoardImageDuringCharacter\oclpbdidgchtr.png";
        public Form1()
        {
            InitializeComponent();
        }
        private void SetTextBox(string str)
        {
            this.TextBox.Text = str;
        }
        /// <summary>
        /// スクリーンショットの画像から文字列を読み込む
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        private string GetImageDuringCharacter(List<string> args = null)
        {
            try
            {
                // pythonのプログラムを呼び出す。
                var pythonProcess = new PythonProcess(pythonInterpreterPath, pythonScriptPath);
                pythonProcess.StartProcess(args);
                // 戻り値を返す。
                return pythonProcess.GetStandardOutput();
            }
            catch
            {
                throw;
            }
        }
        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                var clipboardImage = new ClipBoardImage((Bitmap)Clipboard.GetImage());
                clipboardImage.Save(saveImageName);
                var arg = new List<string> { saveImageName };
                // スクリーンショットの画像から文字列を読み込む
                var imageString = GetImageDuringCharacter(arg);
                // 読み込んだ文字列をテキストボックスに出力する。
                SetTextBox(imageString);
                // 読み込んだ文字列をクリップボードに設定する。
                Clipboard.SetText(imageString);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
