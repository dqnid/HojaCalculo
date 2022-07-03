using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace HojaCalculo
{    
    struct Linea
    {
        public Polyline linea_asociada;
        public Polyline polinomio_asociado;
        public List<Line> barras_asociadas;
        public Button boton_asociado;
        public Window1 ventana_asociada;
    };

    public partial class MainWindow : Window
    {
        List<Linea> lineas = new List<Linea>();
        Binding grosor_enlace;
        double maxTotalX, minTotalX, maxTotalY, minTotalY;
        double escaladoX, escaladoY, desplazamientoX, desplazamientoY;
        bool mouseDown = false;
        Point mouseDownPos;

        public MainWindow()
        {
            InitializeComponent();
            Anadir_coleccion_Click(this, new RoutedEventArgs());
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            while (lineas.Count > 0)
                lineas[0].ventana_asociada.Close();
        }

        void controlador(Object sender, EventArgs e)
        {
            actualizarMaximos();

            Lienzo.Children.Clear();

            dibujarEjes();

            dibujarExpresiones();

            dibujarLineas();
        }

        void actualizarMaximos()
        {
            int i, j;
            double k;
            double valor_x, valor_y;
            for (j = 0; j < lineas.Count; j++)
            {
                for (i = 0; i < lineas[j].ventana_asociada.x.Count; i++)
                {
                    if (i == 0 && j == 0)
                    {
                        maxTotalX = minTotalX = lineas[j].ventana_asociada.x[i].DoubleValue;
                        maxTotalY = minTotalY = lineas[j].ventana_asociada.y[i].DoubleValue;
                        for (k = lineas[j].ventana_asociada.rango_min.DoubleValue; k < lineas[j].ventana_asociada.rango_max.DoubleValue; k += lineas[j].ventana_asociada.intervalo.DoubleValue)
                        {
                            valor_x = k;
                            valor_y = 0;
                            for (int contador = 0; contador < lineas[j].ventana_asociada.z.Count; contador++)
                            {
                                valor_y += lineas[j].ventana_asociada.z[contador].DoubleValue;
                            }
                            if (valor_x > maxTotalX)
                                maxTotalX = valor_x;
                            else if (valor_x < minTotalX)
                                minTotalX = valor_x;
                            if (valor_y > maxTotalY)
                                maxTotalY = valor_y;
                            else if (valor_y < minTotalY)
                                minTotalY = valor_y;
                            i++;
                        }
                    }
                    else
                    {
                        if (lineas[j].ventana_asociada.x[i].DoubleValue > maxTotalX)
                        {
                            maxTotalX = lineas[j].ventana_asociada.x[i].DoubleValue;
                        }
                        else if (lineas[j].ventana_asociada.x[i].DoubleValue < minTotalX)
                        {
                            minTotalX = lineas[j].ventana_asociada.x[i].DoubleValue;
                        }

                        if (lineas[j].ventana_asociada.y[i].DoubleValue > maxTotalY)
                        {
                            maxTotalY = lineas[j].ventana_asociada.y[i].DoubleValue;
                        }
                        else if (lineas[j].ventana_asociada.y[i].DoubleValue < minTotalY)
                        {
                            minTotalY = lineas[j].ventana_asociada.y[i].DoubleValue;
                        }

                        for (k = lineas[j].ventana_asociada.rango_min.DoubleValue; k < lineas[j].ventana_asociada.rango_max.DoubleValue; k += lineas[j].ventana_asociada.intervalo.DoubleValue)
                        {
                            valor_x = k;
                            valor_y = 0;
                            for (int contador = 0; contador < lineas[j].ventana_asociada.z.Count; contador++)
                            {
                                valor_y += lineas[j].ventana_asociada.z[contador].DoubleValue;
                            }
                            if (valor_x > maxTotalX)
                                maxTotalX = valor_x;
                            else if (valor_x < minTotalX)
                                minTotalX = valor_x;
                            if (valor_y > maxTotalY)
                                maxTotalY = valor_y;
                            else if (valor_y < minTotalY)
                                minTotalY = valor_y;
                            i++;
                        }
                    }
                }
            }

            if ((maxTotalX - minTotalX) == 0)
                escaladoX = Lienzo.ActualWidth;
            else
                escaladoX = Lienzo.ActualWidth / (maxTotalX - minTotalX);
            if ((maxTotalY - minTotalY) == 0)
                escaladoY = Lienzo.ActualHeight;
            else
                escaladoY = Lienzo.ActualHeight / (maxTotalY - minTotalY);

            desplazamientoX = desplazamientoY = 0;
            if (minTotalX < 0)
            {
                desplazamientoX = minTotalX * escaladoX;
            }

            if (minTotalY < 0)
            {
                desplazamientoY = minTotalY * escaladoY;
            }
        }

        void dibujarLineas()
        {
            int numPuntos;
            Point[] puntos;
            Line nueva;

            int i, j;
            for (j = 0; j < lineas.Count; j++)
            {
                if (lineas[j].ventana_asociada.polilinea_seleccion.IsChecked.Value)
                {
                    lineas[j].barras_asociadas.Clear();
                    numPuntos = lineas[j].ventana_asociada.grid_puntos.RowDefinitions.Count;
                    puntos = new Point[numPuntos];

                    for (i = 0; i < lineas[j].ventana_asociada.x.Count; i++)
                    {
                        puntos[i].X = lineas[j].ventana_asociada.x[i].DoubleValue * escaladoX + Math.Abs(desplazamientoX);
                    }

                    for (i = 0; i < lineas[j].ventana_asociada.y.Count; i++)
                    {
                        puntos[i].Y = lineas[j].ventana_asociada.y[i].DoubleValue * escaladoY + Math.Abs(desplazamientoY);
                    }
                    lineas[j].linea_asociada.Points = new PointCollection(puntos);
                    lineas[j].linea_asociada.Stroke = new SolidColorBrush(lineas[j].ventana_asociada.color_linea);
                    Lienzo.Children.Add(lineas[j].linea_asociada);
                }
                else if (lineas[j].ventana_asociada.barras_seleccion.IsChecked.Value)
                {
                    lineas[j].linea_asociada.Points.Clear();
                    lineas[j].barras_asociadas.Clear();

                    for (i = 0; i<lineas[j].ventana_asociada.x.Count; i++)
                    {
                        nueva = new Line();
                        nueva.Stroke = new SolidColorBrush(lineas[j].ventana_asociada.color_linea);
                        nueva.X1 = lineas[j].ventana_asociada.x[i].DoubleValue * escaladoX+ Math.Abs(desplazamientoX);
                        nueva.Y1 = 0;
                        nueva.X2 = lineas[j].ventana_asociada.x[i].DoubleValue * escaladoX + Math.Abs(desplazamientoX);
                        nueva.Y2 = lineas[j].ventana_asociada.y[i].DoubleValue * escaladoY + Math.Abs(desplazamientoY);

                        grosor_enlace = new Binding();
                        grosor_enlace.Source = lineas[j].ventana_asociada.grosor_slider;
                        grosor_enlace.Path = new PropertyPath("Value");
                        nueva.SetBinding(Line.StrokeThicknessProperty, grosor_enlace);

                        lineas[j].barras_asociadas.Add(nueva);
                    }

                    for (i = 0; i< lineas[j].barras_asociadas.Count; i++)
                        Lienzo.Children.Add(lineas[j].barras_asociadas[i]);
                }
            }
        }

        void controladorColor(Object sender, EventArgs e)
        {
            int j = 0;
            foreach (Linea li in lineas)
            {
                if (li.ventana_asociada.Equals(sender))
                    break;
                j++;
            }
            lineas[j].linea_asociada.Stroke = new SolidColorBrush(lineas[j].ventana_asociada.color_linea);
            lineas[j].polinomio_asociado.Stroke = new SolidColorBrush(lineas[j].ventana_asociada.color_linea);
            for (int i = 0; i < lineas[j].barras_asociadas.Count; i++)
            {
                lineas[j].barras_asociadas[i].Stroke = new SolidColorBrush(lineas[j].ventana_asociada.color_linea);
            }
        }

        private void theGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            mouseDown = true;
            mouseDownPos = e.GetPosition(theGrid);
            theGrid.CaptureMouse();
         
            Canvas.SetLeft(selectionBox, mouseDownPos.X);
            Canvas.SetTop(selectionBox, mouseDownPos.Y);
            selectionBox.Width = 0;
            selectionBox.Height = 0;

            selectionBox.Visibility = Visibility.Visible;
        }

        private void theGrid_MouseUp(object sender, MouseButtonEventArgs e)
        {
            mouseDown = false;
            theGrid.ReleaseMouseCapture();

            selectionBox.Visibility = Visibility.Collapsed;

            Point mouseUpPos = e.GetPosition(theGrid);

            int i, j;

            for (i=0; i<lineas.Count; i++)
            {
                for (j=0; j<lineas[i].ventana_asociada.x.Count; j++)
                {
                    if ((lineas[i].ventana_asociada.x[j].DoubleValue * escaladoX + Math.Abs(desplazamientoX)) > mouseUpPos.X || 
                        (lineas[i].ventana_asociada.x[j].DoubleValue * escaladoX + Math.Abs(desplazamientoX)) < mouseDownPos.X || 
                        (lineas[i].ventana_asociada.y[j].DoubleValue * escaladoY + Math.Abs(desplazamientoY)) > mouseUpPos.Y || 
                        (lineas[i].ventana_asociada.y[j].DoubleValue * escaladoY + Math.Abs(desplazamientoY)) < mouseDownPos.Y) /*if*/
                        lineas[i].ventana_asociada.eliminar_par_Click(lineas[i].ventana_asociada.botones[j], new RoutedEventArgs());
                }
            }

        }

        private void theGrid_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                Point mousePos = e.GetPosition(theGrid);

                if (mouseDownPos.X < mousePos.X)
                {
                    Canvas.SetLeft(selectionBox, mouseDownPos.X);
                    selectionBox.Width = mousePos.X - mouseDownPos.X;
                }
                else
                {
                    Canvas.SetLeft(selectionBox, mousePos.X);
                    selectionBox.Width = mouseDownPos.X - mousePos.X;
                }

                if (mouseDownPos.Y < mousePos.Y)
                {
                    Canvas.SetTop(selectionBox, mouseDownPos.Y);
                    selectionBox.Height = mousePos.Y - mouseDownPos.Y;
                }
                else
                {
                    Canvas.SetTop(selectionBox, mousePos.Y);
                    selectionBox.Height = mouseDownPos.Y - mousePos.Y;
                }
            }
        }

        private void Lienzo_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            controlador(this, new EventArgs());
        }

        private void Anadir_coleccion_Click(object sender, RoutedEventArgs e)
        {
            int n;
            Linea nuevaLinea = new Linea();

            try
            {
                n = lineas.Count();
            }
            catch (InvalidCastException error)
            {
                n = 0;
            }

            if (n != 0)
                n = (Int32.Parse(lineas[n - 1].boton_asociado.Content.ToString())) + 1;


            nuevaLinea.linea_asociada = new Polyline();

            nuevaLinea.barras_asociadas = new List<Line>();

            nuevaLinea.polinomio_asociado = new Polyline();

            nuevaLinea.boton_asociado = new Button();
            nuevaLinea.boton_asociado.MinWidth = 20;
            nuevaLinea.boton_asociado.Content = n.ToString();
            nuevaLinea.boton_asociado.Click += reLanzarVentana;
            Panel_Inferior.Children.Add(nuevaLinea.boton_asociado);

            nuevaLinea.ventana_asociada = new Window1();
            nuevaLinea.ventana_asociada.evento += controlador;
            nuevaLinea.ventana_asociada.eventoColor += controladorColor;
            nuevaLinea.ventana_asociada.eventoCierre += controladorCierre;
            nuevaLinea.ventana_asociada.Show();

            grosor_enlace = new Binding();
            grosor_enlace.Source = nuevaLinea.ventana_asociada.grosor_slider;
            grosor_enlace.Path = new PropertyPath("Value");
            nuevaLinea.linea_asociada.SetBinding(Polyline.StrokeThicknessProperty,grosor_enlace);

            grosor_enlace = new Binding();
            grosor_enlace.Source = nuevaLinea.ventana_asociada.grosor_slider;
            grosor_enlace.Path = new PropertyPath("Value");
            nuevaLinea.polinomio_asociado.SetBinding(Polyline.StrokeThicknessProperty, grosor_enlace);

            lineas.Add(nuevaLinea);
        }

        private void reLanzarVentana(object sender, RoutedEventArgs e)
        {
            foreach (Linea li in lineas)
            {
                if (li.boton_asociado.Equals(sender))
                {
                    li.ventana_asociada.Focus();
                    break;
                }
            }
        }

        private void dibujarEjes()
        {
            Line abscisas = new Line();
            Line ordenadas = new Line();
            Label cero = new Label();
            Color negro = Color.FromArgb(255, 0, 0, 0);
            int ajusteX, ajusteY;

            ordenadas.X1 = Math.Abs(desplazamientoX);
            ordenadas.Y1 = 0;
            ordenadas.X2 = Math.Abs(desplazamientoX);
            ordenadas.Y2 = Lienzo.ActualHeight;

            ordenadas.Stroke = new SolidColorBrush(negro);

            abscisas.X1 = 0;
            abscisas.Y1 = Math.Abs(desplazamientoY);
            abscisas.X2 = Lienzo.ActualWidth;
            abscisas.Y2 = Math.Abs(desplazamientoY);

            abscisas.Stroke = new SolidColorBrush(negro);

            if (minTotalX < 0)
                ajusteX = 35;
            else
                ajusteX = 0;

            if (minTotalY < 0)
                ajusteY = 35;
            else
                ajusteY = 0;
            cero.Content = "(0'0)";
            cero.Foreground = new SolidColorBrush(negro);
            cero.RenderTransform = new TranslateTransform(Math.Abs(desplazamientoX) - ajusteX, Math.Abs(desplazamientoY) - ajusteY);

            Lienzo.Children.Add(abscisas);
            Lienzo.Children.Add(ordenadas);
            Lienzo.Children.Add(cero);
        }

        void controladorCierre(Object sender, EventArgs e)
        {
            int i = 0;
            foreach (Linea li in lineas)
            {
                if (li.ventana_asociada.Equals(sender))
                {
                    break;
                }
                i++;
            }
            Panel_Inferior.Children.Remove(lineas[i].boton_asociado);
            lineas.RemoveAt(i);
            controlador(this, new EventArgs());
        }

        void dibujarExpresiones()
        {
            Point[] puntos;
            int numPuntos;
            int intervalo;
            double total_y;
            Line nueva;
            int i, j, k, contador;

            for (j = 0; j < lineas.Count; j++)
            {
                if (lineas[j].ventana_asociada.polilinea_seleccion.IsChecked.Value)
                {
                    lineas[j].barras_asociadas.Clear();
                    lineas[j].linea_asociada.Points.Clear();
                    intervalo = lineas[j].ventana_asociada.intervalo.IntValue;
                    if (lineas[j].ventana_asociada.intervalo.IntValue != 0)
                        numPuntos = (lineas[j].ventana_asociada.rango_max.IntValue - lineas[j].ventana_asociada.rango_min.IntValue) / lineas[j].ventana_asociada.intervalo.IntValue;
                    else
                        numPuntos = 0;
                    puntos = new Point[numPuntos];

                    i = 0;
                    for (k=lineas[j].ventana_asociada.rango_min.IntValue; k<lineas[j].ventana_asociada.rango_max.IntValue && i<numPuntos; k+=intervalo)
                    {
                        puntos[i].X = k * escaladoX + Math.Abs(desplazamientoX);
                        puntos[i].Y = 0;
                        for (contador=0; contador<lineas[j].ventana_asociada.z.Count; contador++)
                        {
                            puntos[i].Y += lineas[j].ventana_asociada.z[contador].IntValue * (Math.Pow(k, contador));
                        }
                        puntos[i].Y = puntos[i].Y * escaladoY + Math.Abs(desplazamientoY);
                        i++;
                    }
                    lineas[j].polinomio_asociado.Points = new PointCollection(puntos);
                    lineas[j].polinomio_asociado.Stroke = new SolidColorBrush(lineas[j].ventana_asociada.color_linea);
                    Lienzo.Children.Add(lineas[j].polinomio_asociado);
                }
                else if (lineas[j].ventana_asociada.barras_seleccion.IsChecked.Value)
                {
                    lineas[j].linea_asociada.Points.Clear();
                    lineas[j].barras_asociadas.Clear();

                    for (i = lineas[j].ventana_asociada.rango_min.IntValue; i < lineas[j].ventana_asociada.rango_max.IntValue; i++)
                    {
                        nueva = new Line();
                        nueva.Stroke = new SolidColorBrush(lineas[j].ventana_asociada.color_linea);
                        nueva.X1 = i * escaladoX + Math.Abs(desplazamientoX);
                        nueva.Y1 = 0;
                        nueva.X2 = i * escaladoX + Math.Abs(desplazamientoX);
                        total_y = 0;
                        for (contador=0; contador<lineas[j].ventana_asociada.z.Count; contador++)
                            total_y = lineas[j].ventana_asociada.z[contador].IntValue * (Math.Pow(i, contador));
                        nueva.Y2 = total_y * escaladoX + Math.Abs(desplazamientoY);

                        grosor_enlace = new Binding();
                        grosor_enlace.Source = lineas[j].ventana_asociada.grosor_slider;
                        grosor_enlace.Path = new PropertyPath("Value");
                        nueva.SetBinding(Line.StrokeThicknessProperty, grosor_enlace);

                        lineas[j].barras_asociadas.Add(nueva);
                    }

                    for (i = 0; i < lineas[j].barras_asociadas.Count; i++)
                        Lienzo.Children.Add(lineas[j].barras_asociadas[i]);
                }
            }
        }
    }
}