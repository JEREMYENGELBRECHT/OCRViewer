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

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
        }

        

        private void Form1_Load(object sender, EventArgs e)
        {

            Image mg = Image.FromFile(Application.StartupPath + "\\1.jpg");
            ////Image mg = Image.FromFile("e:\\test24.tif");
            imageViewer1.Initialize(new OCRRenderingData(), new OCRAnalysisRender(imageViewer1));
            imageViewer1.Image = mg;
                      
        }

        private void UpdateImageViewer(List<Word> detectedWords)
        {
            if (imageViewer1.RenderingData != null && imageViewer1.RenderingData is OCRRenderingData)
            {
                //(imageViewer.RenderingData as OCRRenderingData).ShowDetectedCharacters = false;
                (imageViewer1.RenderingData as OCRRenderingData).WordList = detectedWords;
                imageViewer1.Invalidate(true);
            }
        }

        private void UpdateImageViewer(BlockList blocks)
        {
            if (imageViewer1.RenderingData != null && imageViewer1.RenderingData is OCRRenderingData)
            {
                (imageViewer1.RenderingData as OCRRenderingData).Blocks = blocks;
                imageViewer1.Invalidate(true);
            }
        }

        private void imageViewer1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            Image mg = Image.FromFile(Application.StartupPath + "\\1.jpg");
            ////Image mg = Image.FromFile("e:\\test24.tif");
            imageViewer1.Initialize(new OCRRenderingData(), new OCRAnalysisRender(imageViewer1));
            imageViewer1.Image = mg;
           
            TesseractProcessor _ocrProcessor = null;

            _ocrProcessor = new TesseractProcessor();
            
            //string _tessData = "E:\\tesseract\\tesseract-ocr-dotnet-3.01\\tessdata\\";
            string _tessData = Application.StartupPath + "\\tessdata\\";
            string _lang = "eng";
            int _ocrEngineMode = 3;

            bool status = _ocrProcessor.Init(_tessData, _lang, _ocrEngineMode);

            _ocrProcessor.ROI = imageViewer1.RectA;
            _ocrProcessor.UseROI = true;

            string text = _ocrProcessor.Recognize(mg);
            //string text1 = text.Replace(" ", "");
            MessageBox.Show(text);
        }

        private void button2_Click(object sender, EventArgs e)
        {

            Image mg = Image.FromFile(Application.StartupPath + "\\1.jpg");
            ////Image mg = Image.FromFile("e:\\test24.tif");
            imageViewer1.Initialize(new OCRRenderingData(), new OCRAnalysisRender(imageViewer1));
            imageViewer1.Image = mg;
            
            TesseractProcessor _ocrProcessor = null;
            _ocrProcessor = new TesseractProcessor();
            _ocrProcessor.UseROI = false;
            imageViewer1.RectA = new Rectangle(0, 0, 0, 0);
                       
            //string _tessData = "E:\\tesseract\\tesseract-ocr-dotnet-3.01\\tessdata\\";
            string _tessData = Application.StartupPath + "\\tessdata\\";
            string _lang = "eng";
            int _ocrEngineMode = 3;

            bool status = _ocrProcessor.Init(_tessData, _lang, _ocrEngineMode);
            Console.WriteLine(string.Format("[DEBUG] Init status: {0}", status));

            //string text = _ocrProcessor.Recognize(mg);
            //MessageBox.Show(text);

            ////**************************************************** converts 1bpp to 24bpp
            //Bitmap image = new Bitmap(@"e:\\test11.tif");
            //Bitmap bitmap = new Bitmap(image.Width, image.Height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            //for (int i = 0; i < image.Width; i++)
            //{
            //    for (int j = 0; j < image.Height; j++)
            //    {
            //        Color temp = image.GetPixel(i, j);
            //        bitmap.SetPixel(i, j, temp);
            //    }
            //}
            //bitmap.Save(@"e:\\test24.tif", System.Drawing.Imaging.ImageFormat.Bmp);
            //Image mg1 = Image.FromFile("e:\\test24.tif");
            ////******************************************************************************************************


            string variable = "tessedit_pageseg_mode";
           
            // Fully automatic page segmentation
            int fully_psm_auto = 3;
            _ocrProcessor.SetVariable(variable, fully_psm_auto.ToString());

            ///// DEMO ONLY
            //_ocrProcessor.Clear();
            //_ocrProcessor.ClearAdaptiveClassifier();

            //BlockList blocks = _ocrProcessor.DetectBlocks(mg);
            //this.UpdateImageViewer(blocks);

            _ocrProcessor.Clear();
            _ocrProcessor.ClearAdaptiveClassifier();

            string result = _ocrProcessor.Apply(mg);
            List<Word> detectedWords = _ocrProcessor.RetriveResultDetail();
            this.UpdateImageViewer(detectedWords);
                        
            //MessageBox.Show(result);
            button3.Enabled = true;
            button4.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            (imageViewer1.RenderingData as OCRRenderingData).UpdateFlags(true, false, false);
            imageViewer1.Invalidate(true);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            (imageViewer1.RenderingData as OCRRenderingData).UpdateFlags(false, true, false);
            imageViewer1.Invalidate(true);
        }
    }
}
