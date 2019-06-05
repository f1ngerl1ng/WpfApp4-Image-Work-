using Microsoft.Win32;
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

                    var image = System.Drawing.Image.FromFile(dlg.FileName);
                    if (!Directory.Exists(imageFolderSave))
                    {
                        Directory.CreateDirectory(imageFolderSave);
                    }
                    //вхідне фото для обробки
                    var bmpOrigin = new System.Drawing.Bitmap(image);
                    
                    var imageName = Guid.NewGuid().ToString() + ".jpg";
                    var imageSave = ImageWorker.CreateImage(bmpOrigin, 200, 200);
                    
                    if (imageSave == null)
                        throw new Exception("Проблема обробки фото");

                    var imageSaveEnd = System.IO.Path.Combine(imageFolderSave, imageName);
                    imageSave.Save(imageSaveEnd, System.Drawing.Imaging.ImageFormat.Jpeg);
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
            lblSize.Content += bitmap.PixelWidth.ToString() + " x " + bitmap.PixelHeight.ToString();
        }
    }
}
