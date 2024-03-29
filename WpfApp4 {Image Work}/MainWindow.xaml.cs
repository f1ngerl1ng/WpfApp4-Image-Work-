﻿using Microsoft.Win32;
using ImageWorks.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing;

namespace WpfApp4__Image_Work_
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private System.Drawing.Image image;

        private void BtnSelectImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) " +
                "| *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
            if (dlg.ShowDialog() == true)
            {
                try
                {
                    var imageFolderSave = "uploads";
                    var filePath = dlg.FileName;

                    CreateViewImage(filePath);

                    image = System.Drawing.Image.FromFile(dlg.FileName);
                    if (!Directory.Exists(imageFolderSave))
                    {
                        Directory.CreateDirectory(imageFolderSave);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Щось пішло не так {ex.Message}");
                }

            }
        }
        private void CreateViewImage(string filePath)
        {
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(filePath);
            bitmap.EndInit();

            ImageView.Source = bitmap;
            lblSize.Visibility = Visibility.Visible;
            grbConvert.Visibility = Visibility.Visible;
            lblSize.Content = "Исходный размер: " + bitmap.PixelWidth.ToString() + " x " + bitmap.PixelHeight.ToString();
        }
       
        private void ConvertImage( int size)
        {
            var imageFolderSave = "uploads";
            Bitmap bmpOrigin = new Bitmap(image);
            var imageName = Guid.NewGuid().ToString() + ".jpg";
            var imageSave = ImageWorker.CreateImage(bmpOrigin, size, size);

            if (imageSave == null)
                throw new Exception("Проблема обробки фото");
            else
                MessageBox.Show(imageSave.Height.ToString() + " x " + imageSave.Width.ToString(), "Размер изображения");

            var imageSaveEnd = System.IO.Path.Combine(imageFolderSave, imageName);
            imageSave.Save(imageSaveEnd, System.Drawing.Imaging.ImageFormat.Jpeg);
        }

        private void Btn_Convert_Click(object sender, RoutedEventArgs e)
        {
            int size = 0;
            if (SizeImg.IsChecked == true)
            { size = Convert.ToInt32(SizeImg.Content.ToString()); }
            else if (SizeImg1.IsChecked == true)
            { size = Convert.ToInt32(SizeImg1.Content.ToString()); }
            else if (SizeImg2.IsChecked == true)
            { size = Convert.ToInt32(SizeImg2.Content.ToString()); }
            else if (SizeImg3.IsChecked == true)
            { size = Convert.ToInt32(SizeImg3.Content.ToString()); }
            else if (SizeImg4.IsChecked == true)
            { size = Convert.ToInt32(SizeImg4.Content.ToString()); }

            ConvertImage(size);

            lblSize.Visibility = Visibility.Hidden;
            grbConvert.Visibility = Visibility.Hidden;
        }
    }
}
