using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Drawing;
using System.Windows.Controls;
using System.Drawing.Imaging;

namespace DICOM_Viewer_v2
{   

    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    
    public partial class MainWindow : Window
    {
        List<string> orderedListOfFiles = new List<string>();
        List<int> arrayOrder = new List<int>();
        List<int> arrayImage = new List<int>();
        List<byte> showing = new List<byte>();

        int listIndex = 0;
        int pictureIndex = 0;

        byte[] viewDicom1;
        byte[] viewDicom2;
        byte[] viewDicom3;
        byte[,,] database;

        BitmapSource bitmapSource;
        BitmapSource bitmapRegionGrowing;
        private Bitmap OriginalImage;
        private bool IsSelecting = false;
        private double X0, Y0, X1, Y1;




        public MainWindow()
        {
            InitializeComponent();
        }


        // metoda zwracająca array ze wszystkimi wolumenami
        private void Make3Darray()
        {
            byte[] fileBytes;
            byte[] dicomData;
            
            int pixelTag = 0;
            string file_directory;
            List<string> arrayBytes = new List<string>();

            for (int i = 0; i < orderedListOfFiles.Count; i++)
            {
                int count = 0;
                arrayBytes.Clear();
                arrayImage.Clear();
                file_directory = orderedListOfFiles[listIndex];
                fileBytes = File.ReadAllBytes(file_directory);
                

                foreach (byte b in fileBytes)
                {
                    // dodawanie do listy bit po bicie i konwersja na hex
                    arrayBytes.Add(b.ToString("X"));
                    // szukanie tagu
                    if (arrayBytes[count] == "7F")
                    {
                        if (arrayBytes[count - 1] == "E0")
                        {
                            pixelTag = count - 1;
                        }
                    }
                    count++;
                }

                int min = -32000;
                int max = 32000;

                for (int x = pixelTag + 12; x < arrayBytes.Count; x += 2)
                {
                    // min max, czy to short czy unsigned
                    string pixel = arrayBytes[x].Length > 1 ?
                        arrayBytes[x + 1] + arrayBytes[x] :
                        arrayBytes[x + 1] + '0' + arrayBytes[x];   // parujemy bajty np. 7F+E0         

                    short pixelValue = Convert.ToInt16(pixel, 16);

                    if (pixelValue == -2000)
                    {
                        pixelValue = 0;
                    }

                    if (x == pixelTag + 12)
                    {
                        min = pixelValue;
                        max = pixelValue;
                    }

                    if (min >= pixelValue)
                    {
                        min = pixelValue;
                    }

                    if (max <= pixelValue)
                    {
                        max = pixelValue;
                    }

                    arrayOrder.Add(pixelValue);
                    arrayImage.Add(pixelValue);
                }

                int z = 0;
                //int min = -2000; // arrayOrder.Min() - na sztywno
                //int max = 7751; // arrayOrder.Max()
                int range = max + min;
                double sf = 255.0 / range;

                dicomData = new byte[arrayImage.Count];
                for (int y = 0; y < arrayImage.Count; y++)
                {
                    //lista wartosci od 0-255
                    dicomData[y] = (byte)(arrayImage[y] * sf);
                }

                for (int j = 0; j <= 511; j++)
                {
                    for (int k = 0; k <= 511; k++)
                    {
                        database[i, j, k] = dicomData[z++];
                    }
                }
                listIndex++;

            }
            ViewAxialImage(database);
            Histogram();
            ViewSagittalImage(database);
            ViewCoronalImage(database);
        }


        // widok Axial
        private void ViewAxialImage(byte[,,] axial3DArray)
        {
            showing.Clear();
            viewDicom1 = new byte[arrayOrder.Count];

            for (int c = 0; c < 512; c++)
            {
                for (int d = 0; d < 512; d++)
                {
                    showing.Add(axial3DArray[pictureIndex, c, d]);
                }
            }
            viewDicom1 = showing.ToArray();
            bitmapSource = BitmapSource.Create(512, 512, 300, 300, PixelFormats.Indexed8, BitmapPalettes.Gray256, viewDicom1, 512);
            image1.Source = bitmapSource;
        }


        // widok Sagittal
        private void ViewSagittalImage(byte[,,] sagittal3DArray)
        {
            showing.Clear();
            viewDicom2 = new byte[arrayOrder.Count];

            if (BoxPixelAverage.IsChecked == false && BoxFirstHit.IsChecked == false && BoxMax.IsChecked == false)
            {
                for (int c = 0; c < 112; c++)
                {
                    for (int d = 0; d < 512; d++)
                    {
                        showing.Add(sagittal3DArray[c, pictureIndex + 150, d]);
                    }
                }
            }
            else if (BoxPixelAverage.IsChecked == true || BoxFirstHit.IsChecked == true || BoxMax.IsChecked == true)
            {
                for (int c = 0; c < 112; c++)
                {
                    for (int d = 0; d < 512; d++)
                    {
                        showing.Add(sagittal3DArray[0, c, d]);
                    }
                }
            }
            
            viewDicom2 = showing.ToArray();
            BitmapSource bitmapSource = BitmapSource.Create(512, 112, 300, 300, PixelFormats.Indexed8, BitmapPalettes.Gray256, viewDicom2, 512);
            image2.Source = bitmapSource;
        }


        // widok Coronal
        private void ViewCoronalImage(byte[,,] coronal3DArray)
        {
            showing.Clear();
            viewDicom3 = new byte[arrayOrder.Count];

            if (BoxPixelAverage.IsChecked == false && BoxFirstHit.IsChecked == false && BoxMax.IsChecked == false)
            {
                for (int c = 0; c < 112; c++)
                {
                    for (int d = 0; d < 512; d++)
                    {
                        showing.Add(coronal3DArray[c, d, pictureIndex + 150]);
                    }
                }
            }
            else if (BoxPixelAverage.IsChecked == true || BoxFirstHit.IsChecked == true || BoxMax.IsChecked == true)
            {
                for (int c = 0; c < 112; c++)
                {
                    for (int d = 0; d < 512; d++)
                    {
                        showing.Add(coronal3DArray[0, d, c]);
                    }
                }
            }

            viewDicom3 = showing.ToArray();
            BitmapSource bitmapSource = BitmapSource.Create(512, orderedListOfFiles.Count, 300, 300, PixelFormats.Indexed8, BitmapPalettes.Gray256, viewDicom3, 512);
            image3.Source = bitmapSource;
        }


        // metoda obsługuje przycisk otwarcia folderu z wolumenami DICOM
        private void OpenImage(object sender, RoutedEventArgs e)
        {
            orderedListOfFiles = null;

            var openFolder = new FolderBrowserDialog();
            DialogResult openDialog = openFolder.ShowDialog();
            string[] filesCollection = Directory.GetFiles(openFolder.SelectedPath);

            var orderedFilesCollection = filesCollection
            .OrderBy(x => new string(x.Where(char.IsLetter).ToArray()))
            .ThenBy(x =>
            {
                int number;
                if (int.TryParse(new string(x.Where(char.IsDigit).ToArray()), out number))
                    return number;
                return -1;
            }).ToList();

            orderedListOfFiles = orderedFilesCollection;
            database = new byte[orderedListOfFiles.Count, 512, 512];
            Make3Darray();
        }


        // metoda obsługująca przycisk poprzedniego zdjęcia
        private void PrevImage(object sender, RoutedEventArgs e)
        {
            pictureIndex -= 1;
            if (pictureIndex == -1)
            {
                pictureIndex = 111;
            }
            ViewAxialImage(database);
            Histogram();
            ViewSagittalImage(database);
            ViewCoronalImage(database);
        }


        // metoda obsługująca przycisk następnego zdjęcia
        private void NextImage(object sender, RoutedEventArgs e)
        {
            pictureIndex += 1;
            if (pictureIndex == 112)
            {
                pictureIndex = 0;
            }
            ViewAxialImage(database);
            Histogram();
            ViewSagittalImage(database);
            ViewCoronalImage(database);
        }


        // metoda obsługująca slider zdjęć
        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            pictureIndex = (int)Slider.Value;
            ViewAxialImage(database);
            Histogram();
            ViewSagittalImage(database);
            ViewCoronalImage(database);
        }


        // metoda średnich
        private void PixelAverage(object sender, RoutedEventArgs e)
        {
            // axial average
            int[,,] averageDataAxial_Int = new int[1, 512, 512];
            byte[,,] averageDataAxial_Byte = new byte[1, 512, 512];
            for (int i = 0; i < 112; i++)
            {
                for (int j = 0; j < 512; j++)
                {
                    for (int k = 0; k < 512; k++)
                    {
                        averageDataAxial_Int[0, j, k] += database[i, j, k];
                    }
                }
            }

            for (int m = 0; m < 512; m++)
            {
                for (int n = 0; n < 512; n++)
                {
                    averageDataAxial_Int[0, m, n] /= 37; //powinno być 112, ale wychodzi ciemny obrazek więc 112*1/3
                    averageDataAxial_Byte[0, m, n] = (byte)averageDataAxial_Int[0, m, n];
                }
            }


            // sagittal average
            int[,,] averageDataSagittal_Int = new int[1, 512, 512];
            byte[,,] averageDataSagittal_Byte = new byte[1, 512, 512];
            for (int i = 0; i < 512; i++)
            {
                for (int j = 0; j < 112; j++)
                {
                    for (int k = 0; k < 512; k++)
                    {
                        averageDataSagittal_Int[0, j, k] += database[j, i, k];
                    }
                }
            }

            for (int m = 0; m < 112; m++)
            {
                for (int n = 0; n < 512; n++)
                {
                    averageDataSagittal_Int[0, m, n] /= 112;
                    averageDataSagittal_Byte[0, m, n] = (byte)averageDataSagittal_Int[0, m, n];
                }
            }


            // coronal average
            int[,,] averageDataCoronal_Int = new int[1, 512, 512];
            byte[,,] averageDataCoronal_Byte = new byte[1, 512, 512];
            for (int i = 0; i < 512; i++)
            {
                for (int j = 0; j < 512; j++)
                {
                    for (int k = 0; k < 112; k++)
                    {
                        averageDataCoronal_Int[0, j, k] += database[k, j, i];
                    }
                }
            }

            for (int m = 0; m < 512; m++)
            {
                for (int n = 0; n < 112; n++)
                {
                    averageDataCoronal_Int[0, m, n] /= 112;
                    averageDataCoronal_Byte[0, m, n] = (byte)averageDataCoronal_Int[0, m, n];
                }
            }

            pictureIndex = 0;
            ViewAxialImage(averageDataAxial_Byte);
            ViewSagittalImage(averageDataSagittal_Byte);
            ViewCoronalImage(averageDataCoronal_Byte);
        }


        // metoda firsthit
        private void FirstHit(object sender, RoutedEventArgs e)
        {
            int LT = 20; //threshold
            int PT = 150;
            double rescale_m = (255-0)/(PT-LT);
            double rescale_c = LT * rescale_m;

            BoxFirstHit.IsChecked = true;
            // axial firsthit
            byte[,,] firsthitAxial = new byte[1, 512, 512];
            for (int i = 0; i < 112; i++)
            {
                for (int j = 0; j < 512; j++)
                {
                    for (int k = 0; k < 512; k++)
                    {
                        if (database[i, j, k] >= LT && database[i, j, k] <= PT && firsthitAxial[0, j, k] == 0)
                        {
                            firsthitAxial[0, j, k] = database[i, j, k];
                            firsthitAxial[0, j, k] = (byte)((firsthitAxial[0, j, k] * rescale_m) + rescale_c);
                        }
                    }
                }
            }

            // sagitall firsthit
            byte[,,] firsthitSagittal = new byte[1, 512, 512];
            for (int i = 0; i < 512; i++)
            {
                for (int j = 0; j < 112; j++)
                {
                    for (int k = 0; k < 512; k++)
                    {
                        if (database[j, i, k] >= LT && database[j, i, k] <= PT && firsthitSagittal[0, j, k] == 0)
                        {
                            firsthitSagittal[0, j, k] = database[j, i, k];
                            firsthitSagittal[0, j, k] = (byte)((firsthitSagittal[0, j, k] * rescale_m) + rescale_c);
                        }
                    }
                }
            }

            // coronal firsthit
            byte[,,] firsthitCoronal = new byte[1, 512, 512];
            for (int i = 0; i < 512; i++)
            {
                for (int j = 0; j < 512; j++)
                {
                    for (int k = 0; k < 112; k++)
                    {
                        if (database[k, j, i] >= LT && database[k, j, i] <= PT && firsthitCoronal[0, j, k] == 0)
                        {
                            firsthitCoronal[0, j, k] = database[k, j, i];
                            firsthitCoronal[0, j, k] = (byte)((firsthitCoronal[0, j, k] * rescale_m) + rescale_c);
                        }
                    }
                }
            }

            pictureIndex = 0;
            ViewAxialImage(firsthitAxial);
            ViewSagittalImage(firsthitSagittal);
            ViewCoronalImage(firsthitCoronal);
        }


        // metoda max
        private void MaxPixel(object sender, RoutedEventArgs e)
        {
            BoxMax.IsChecked = true;

            // axial max
            byte[,,] maxAxial = new byte[1, 512, 512];
            for (int i = 0; i < 112; i++)
            {
                for (int j = 0; j < 512; j++)
                {
                    for (int k = 0; k < 512; k++)
                    {
                        if (maxAxial[0, j, k] == 0 || maxAxial[0, j, k] <= database[i, j, k])
                        {
                            maxAxial[0, j, k] = database[i, j, k];
                        }
                    }
                }
            }

            // sagitall firsthit
            byte[,,] maxSagittal = new byte[1, 512, 512];
            for (int i = 0; i < 512; i++)
            {
                for (int j = 0; j < 112; j++)
                {
                    for (int k = 0; k < 512; k++)
                    {
                        if (maxSagittal[0, j, k] == 0 || maxSagittal[0, j, k] <= database[j, i, k])
                        {
                            maxSagittal[0, j, k] = database[j, i, k];
                        }
                    }
                }
            }

            // coronal firsthit
            byte[,,] maxCoronal = new byte[1, 512, 512];
            for (int i = 0; i < 512; i++)
            {
                for (int j = 0; j < 512; j++)
                {
                    for (int k = 0; k < 112; k++)
                    {
                        if (maxCoronal[0, j, k] == 0 || maxCoronal[0, j, k] <= database[k, j, i])
                        {
                            maxCoronal[0, j, k] = database[k, j, i];
                        }
                    }
                }
            }

            pictureIndex = 0;
            ViewAxialImage(maxAxial);
            ViewSagittalImage(maxSagittal);
            ViewCoronalImage(maxCoronal);
        }


        // histogram Axial
        private void Histogram()
        {
            //
            // axial
            //
            float[] frequency = new float[256];

            for (int m = 0; m < 512; m++)
            {
                for (int n = 0; n < 512; n++)
                {
                    if (database[pictureIndex, m, n] > 1)          // 0 i 1 występują bardzo często, bez nich histogram wygląda lepiej
                        frequency[database[pictureIndex, m, n]] += 1;
                }
            }
            float max = frequency.Max();

            for (int o = 0; o < 256; o++)
            {
                frequency[o] *= (float)255.0 / max;
            }

            DicomChart dc = new DicomChart();
            foreach (float item in frequency)
                dc.ChartItems.Add(new DicomChartItem(item));

            ItemsControlName.DataContext = dc;
        }


        // zoom
        private void Zoom(object sender, RoutedEventArgs e)
        {
            BoxZoom.IsChecked = true;
        }


        // zadanie zaliczeniowe Region Growing Algorithm
        private void RegionGrowing(object sender, RoutedEventArgs e)
        {
            byte[] databaseRegion;
            byte[,,] databaseRegion2D = new byte[1, 512, 512];
            int averageValue = 0;

            var positiveVisitedPixels = new List<Tuple<int, int>>();
            var toVisitPixels = new List<Tuple<int, int>>();

            // ustawiam punkt początkowy
            int row = (int)Y0, column = (int)X0;
            int posX = row, posY = column;
            toVisitPixels.Add(new Tuple<int, int>(row, column));


            databaseRegion = GetBytesFromBitmapSource(bitmapRegionGrowing); // są jakieś błędy odczytu i część wartości wynosi 255 co zawyża wart. średnią
                                                                            
            for (int i = 0; i < databaseRegion.Count()-1; i++)
            {
                if (databaseRegion[i] == 255)
                    databaseRegion[i] = databaseRegion[i + 1];

            }

            // statystyka obszaru -> ŚREDNIA
            for (int i = 0; i < databaseRegion.Count(); i++)
            {
                averageValue += databaseRegion[i];
            }
            averageValue /= databaseRegion.Count();

            // statystyka obszaru -> PRAWDOPODOBIEŃSTWO WYSTĄPIEŃ INTENSYWNOŚCI
            float[] count = new float[256];
            List<float> allPositive = new List<float>(); 

            for (int c = 0; c < 255; c++)
            {
                for (int d = 0; d < databaseRegion.Length; d++)
                {
                    if (databaseRegion[d] == c)
                        count[c]++;
                }
            }

            for (int f = 0; f < 255; f++)
            {
                count[f] /= databaseRegion.Count();
                if (count[f] > 0)
                {
                    allPositive.Add(f);
                }
            }



            // właściwy algorytm
            while (toVisitPixels.Count > 0)
            {
                double threshold = 0.015;

                // biorę pierwszy element listy
                // i ustawiam pozycję piksela do sprawdzenia
                posX = toVisitPixels[0].Item1;
                posY = toVisitPixels[0].Item2;


                // sprawdzam otoczenie piksela database[pictureIndex, posX, posY] :: kursor ma kształt plusa +
                // lewy piksel
                if (allPositive.Contains(database[pictureIndex, posX - 1, posY]) && database[pictureIndex, posX - 1, posY] > threshold)
                {
                    var index = toVisitPixels.FindIndex(s => s.Item1 == posX - 1 && s.Item2 == posY);
                    var index2 = positiveVisitedPixels.FindIndex(s => s.Item1 == posX - 1 && s.Item2 == posY);
                    if (index > -1 || index2 > -1)
                    {
                        
                    }
                    else
                    {
                        databaseRegion2D[0, posX - 1, posY] = database[pictureIndex, posX - 1, posY];
                        toVisitPixels.Add(new Tuple<int, int>(posX - 1, posY));
                    }
                }

                // prawy piksel
                if (allPositive.Contains(database[pictureIndex, posX + 1, posY]) && database[pictureIndex, posX - 1, posY] > threshold)
                {
                    var index = toVisitPixels.FindIndex(s => s.Item1 == posX + 1 && s.Item2 == posY);
                    var index2 = positiveVisitedPixels.FindIndex(s => s.Item1 == posX + 1 && s.Item2 == posY);
                    if (index > -1 || index2 > -1)
                    {
                        
                    }
                    else
                    {
                        databaseRegion2D[0, posX + 1, posY] = database[pictureIndex, posX + 1, posY];
                        toVisitPixels.Add(new Tuple<int, int>(posX + 1, posY));
                    }
                }

                // górny piksel
                if (allPositive.Contains(database[pictureIndex, posX, posY - 1]) && database[pictureIndex, posX - 1, posY] > threshold)
                {
                    var index = toVisitPixels.FindIndex(s => s.Item1 == posX && s.Item2 == posY - 1);
                    var index2 = positiveVisitedPixels.FindIndex(s => s.Item1 == posX && s.Item2 == posY - 1);
                    if (index > -1 || index2 > -1)
                    {
                        
                    }
                    else
                    {
                        databaseRegion2D[0, posX, posY - 1] = database[pictureIndex, posX, posY - 1];
                        toVisitPixels.Add(new Tuple<int, int>(posX, posY - 1));
                    }
                }

                // dolny piksel
                if (allPositive.Contains(database[pictureIndex, posX, posY + 1]) && database[pictureIndex, posX - 1, posY] > threshold)
                {
                    var index = toVisitPixels.FindIndex(s => s.Item1 == posX && s.Item2 == posY + 1);
                    var index2 = positiveVisitedPixels.FindIndex(s => s.Item1 == posX && s.Item2 == posY + 1);
                    if (index > -1 || index2 > -1)
                    {
                        
                    }
                    else
                    {
                        databaseRegion2D[0, posX, posY + 1] = database[pictureIndex, posX, posY + 1];
                        toVisitPixels.Add(new Tuple<int, int>(posX, posY + 1));
                    }
                }


                Tuple<int, int> t = toVisitPixels.First();
                if (t != null)
                {
                    // usuwam oglądany piksel
                    toVisitPixels.Remove(t);
                }
                positiveVisitedPixels.Add(t);

                
            }  

            pictureIndex = 0;
            ViewAxialImage(databaseRegion2D);
        }






        ///
        /// OBSŁUGA MYSZKI NA OBRAZIE
        ///

        private void picOriginal_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            IsSelecting = true;
            System.Windows.Point p0 = e.GetPosition((IInputElement)sender);

            // zapisz start point
            X0 = p0.X;
            Y0 = p0.Y;
        }

        private void picOriginal_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            System.Windows.Point p1 = e.GetPosition((IInputElement)sender);

            if (!IsSelecting) return;

            X1 = p1.X;
            Y1 = p1.Y;

            OriginalImage = GetBitmap(bitmapSource);
            Bitmap bm = new Bitmap(OriginalImage);

            using (Graphics gr = Graphics.FromImage(bm))
            {
                gr.DrawRectangle(Pens.Red,
                    (float)Math.Min(X0, X1), (float)Math.Min(Y0, Y1),
                    (float)Math.Abs(X0 - X1), (float)Math.Abs(Y0 - Y1));
            }

            image1.Source = BitmapToImageSource(bm);
        }

        private void picOriginal_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (!IsSelecting) return;
            IsSelecting = false;

            image1.Source = BitmapToImageSource(OriginalImage);

            int wid = (int)Math.Abs(X0 - X1);
            int hgt = (int)Math.Abs(Y0 - Y1);


            if ((wid < 1) || (hgt < 1)) return;

            Bitmap area = new Bitmap(wid, hgt);

            using (Graphics gr = Graphics.FromImage(area))
            {
                Rectangle source_rectangle = new Rectangle((int)Math.Min(X0, X1), (int)Math.Min(Y0, Y1), wid, hgt);
                Rectangle dest_rectangle = new Rectangle(0, 0, wid, hgt);
                gr.DrawImage(OriginalImage, dest_rectangle, source_rectangle, GraphicsUnit.Pixel);
            }

            if (BoxZoom.IsChecked == true)
                image1.Source = BitmapToImageSource(area);
            else 
                bitmapRegionGrowing = BitmapToImageSource(area);


        }






        /// 
        /// KONWERTERY BITMAP
        /// 

        BitmapImage BitmapToImageSource(Bitmap bitmap)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                memory.Position = 0;
                BitmapImage bitmapimage = new BitmapImage();
                bitmapimage.BeginInit();
                bitmapimage.StreamSource = memory;
                bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapimage.EndInit();

                return bitmapimage;
            }
        }
         
        Bitmap GetBitmap(BitmapSource source)
        {

            Bitmap bmp = new Bitmap
            (
              source.PixelWidth,
              source.PixelHeight,
              System.Drawing.Imaging.PixelFormat.Format8bppIndexed
            );

            BitmapData data = bmp.LockBits
            (
                new System.Drawing.Rectangle(System.Drawing.Point.Empty, bmp.Size),
                ImageLockMode.WriteOnly,
                System.Drawing.Imaging.PixelFormat.Format8bppIndexed
            );

            source.CopyPixels
            (
              Int32Rect.Empty,
              data.Scan0,
              data.Height * data.Stride,
              data.Stride
            );

            bmp.UnlockBits(data);

            // przekolorowanie palety Format8bppIndexed na Gray 
            ColorPalette _palette = bmp.Palette;
            System.Drawing.Color[] _entries = _palette.Entries;
            for (int i = 0; i < 256; i++)
            {
                System.Drawing.Color b = new System.Drawing.Color();
                b = System.Drawing.Color.FromArgb((byte)i, (byte)i, (byte)i);
                _entries[i] = b;
            }
            bmp.Palette = _palette;

            return bmp;
        }

        static byte[] GetBytesFromBitmapSource(BitmapSource bmp)
        {
            int width = bmp.PixelWidth;
            int height = bmp.PixelHeight;
            int stride = width * ((bmp.Format.BitsPerPixel + 7) / 8);

            byte[] pixels = new byte[height * stride];

            bmp.CopyPixels(pixels, stride, 0);

            return pixels;
        }

    }
}
