using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Tesseract.OCR.AppEntry.UI;
//using tesseract;
using ImageViewerOCR;
using OCR.TesseractWrapper;
using System.IO;


namespace WindowsFormsApplication1
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        String rec = "";
        private void Form2_Load(object sender, EventArgs e)
        {
            Image mg = Image.FromFile(Application.StartupPath + "\\1.jpg");
            ////Image mg = Image.FromFile("e:\\test24.tif");
            templateViewer1.Initialize(new OCRRenderingData(), new OCRAnalysisRender(templateViewer1));
            templateViewer1.Image = mg;
                
        }

        private void button1_Click(object sender, EventArgs e)
        {

            Image mg = Image.FromFile(Application.StartupPath + "\\1.jpg");
            ////Image mg = Image.FromFile("e:\\test24.tif");
            templateViewer1.Initialize(new OCRRenderingData(), new OCRAnalysisRender(templateViewer1));
            templateViewer1.Image = mg;

            TesseractProcessor _ocrProcessor = null;

            _ocrProcessor = new TesseractProcessor();

            string _tessData = Application.StartupPath + "\\tessdata\\";
            string _lang = "eng";
            int _ocrEngineMode = 3;

            bool status = _ocrProcessor.Init(_tessData, _lang, _ocrEngineMode);

            _ocrProcessor.ROI = templateViewer1.RectA;
            rec += templateViewer1.RectA.X + ";" + templateViewer1.RectA.Y + ";" + templateViewer1.RectA.Width + ";" + templateViewer1.RectA.Height + "|"; 
            _ocrProcessor.UseROI = true;

            string text = _ocrProcessor.Recognize(mg);
           
            _ocrProcessor.ROI = templateViewer1.RectB;
            rec += templateViewer1.RectB.X + ";" + templateViewer1.RectB.Y + ";" + templateViewer1.RectB.Width + ";" + templateViewer1.RectB.Height + "|"; 
            _ocrProcessor.UseROI = true;

            text += _ocrProcessor.Recognize(mg);
           
            _ocrProcessor.ROI = templateViewer1.RectC;
            rec += templateViewer1.RectC.X + ";" + templateViewer1.RectC.Y + ";" + templateViewer1.RectC.Width + ";" + templateViewer1.RectC.Height + "|"; 
            _ocrProcessor.UseROI = true;

            text += _ocrProcessor.Recognize(mg);

            _ocrProcessor.ROI = templateViewer1.RectD;
            rec += templateViewer1.RectD.X + ";" + templateViewer1.RectD.Y + ";" + templateViewer1.RectD.Width + ";" + templateViewer1.RectD.Height + "|";
            _ocrProcessor.UseROI = true;

            text += _ocrProcessor.Recognize(mg);

            _ocrProcessor.ROI = templateViewer1.RectE;
            rec += templateViewer1.RectE.X + ";" + templateViewer1.RectE.Y + ";" + templateViewer1.RectE.Width + ";" + templateViewer1.RectE.Height;
            _ocrProcessor.UseROI = true;

            text += _ocrProcessor.Recognize(mg);

            MessageBox.Show(text);
            MessageBox.Show(rec);

        }

        private void button2_Click(object sender, EventArgs e)
        {

            SaveFileDialog fldg = new SaveFileDialog();
            fldg.Filter = "|*.otl";
            fldg.ShowDialog();

            // Compose a string that consists of three lines.
            string lines = rec;

            // Write the string to a file.
            System.IO.StreamWriter file = new System.IO.StreamWriter(fldg.FileName );
            file.WriteLine(lines);

            file.Close();

 
            

        }

        private void button3_Click(object sender, EventArgs e)
        {

            OpenFileDialog opn = new OpenFileDialog();
            opn.ShowDialog();
            
           StreamReader sr = new StreamReader(opn.FileName);
           String line = sr.ReadToEnd();


           char[] delimiterChars = { '|' };
           char[] delimiterChars1 = { ';' };

           string[] str = line.Split(delimiterChars);

           Image mg = Image.FromFile(Application.StartupPath + "\\1.jpg");
           
           TesseractProcessor _ocrProcessor = null;
           _ocrProcessor = new TesseractProcessor();

           string _tessData = Application.StartupPath + "\\tessdata\\";
           string _lang = "eng";
           int _ocrEngineMode = 3;

           bool status = _ocrProcessor.Init(_tessData, _lang, _ocrEngineMode);


           string[] str1 = str[0].Split(delimiterChars1); 
           Rectangle recA = new Rectangle(Convert.ToInt32(str1[0]), Convert.ToInt32(str1[1]),Convert.ToInt32(str1[2]),Convert.ToInt32(str1[3]));

           _ocrProcessor.ROI = recA;
           _ocrProcessor.UseROI = true;
           templateViewer1.DrawRect(recA);
           templateViewer1.RectA = recA;

           string text = _ocrProcessor.Recognize(mg);

           string[] str2 = str[1].Split(delimiterChars1);
           Rectangle recB = new Rectangle(Convert.ToInt32(str2[0]), Convert.ToInt32(str2[1]), Convert.ToInt32(str2[2]), Convert.ToInt32(str2[3]));

           _ocrProcessor.ROI = recB;
           _ocrProcessor.UseROI = true;
           templateViewer1.DrawRect(recB);
           templateViewer1.RectB = recB;

            text += _ocrProcessor.Recognize(mg);

           string[] str3 = str[2].Split(delimiterChars1);
           Rectangle recC = new Rectangle(Convert.ToInt32(str3[0]), Convert.ToInt32(str3[1]), Convert.ToInt32(str3[2]), Convert.ToInt32(str3[3]));

           _ocrProcessor.ROI = recC;
           _ocrProcessor.UseROI = true;
           templateViewer1.DrawRect(recC);
           templateViewer1.RectC = recC;

           text += _ocrProcessor.Recognize(mg);

           string[] str4 = str[3].Split(delimiterChars1);
           Rectangle recD = new Rectangle(Convert.ToInt32(str4[0]), Convert.ToInt32(str4[1]), Convert.ToInt32(str4[2]), Convert.ToInt32(str4[3]));

           _ocrProcessor.ROI = recD;
           _ocrProcessor.UseROI = true;
           templateViewer1.DrawRect(recD);
           templateViewer1.RectD = recD;

           text += _ocrProcessor.Recognize(mg);

           string[] str5 = str[4].Split(delimiterChars1);
           Rectangle recE = new Rectangle(Convert.ToInt32(str5[0]), Convert.ToInt32(str5[0]), Convert.ToInt32(str5[0]), Convert.ToInt32(str5[0]));

           _ocrProcessor.ROI = recE;
           _ocrProcessor.UseROI = true;
           templateViewer1.DrawRect(recE);
           templateViewer1.RectE = recE;

           text += _ocrProcessor.Recognize(mg);

           MessageBox.Show(text);
          
        }
               
    }
}
