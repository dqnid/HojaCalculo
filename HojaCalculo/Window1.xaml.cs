using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using System.Collections;
using System.Collections.ObjectModel;

namespace HojaCalculo
{
    public partial class Window1 : Window
    {
        public List<NumericTextBox> x = new List<NumericTextBox>();
        public List<NumericTextBox> y = new List<NumericTextBox>();
        public List<NumericTextBox> z = new List<NumericTextBox>();
        public List<Button> botones = new List<Button>();
        public Color color_linea;

        public event EventHandler evento;
        public event EventHandler eventoColor;
        public event EventHandler eventoCierre;

        public Window1()
        {
            InitializeComponent();

            grid_puntos_init();
            estilo_inicial();
            color_linea = Color.FromArgb(255, Convert.ToByte(rojo_slider.Value), Convert.ToByte(verde_slider.Value), Convert.ToByte(azul_slider.Value));
        }

        private void estilo_inicial()
        {
            LinearGradientBrush gradienteRojo = new LinearGradientBrush();
            LinearGradientBrush gradienteVerde = new LinearGradientBrush();
            LinearGradientBrush gradienteAzul = new LinearGradientBrush();

            gradienteRojo.GradientStops.Add(new GradientStop(Colors.Black, 0.0));
            gradienteRojo.GradientStops.Add(new GradientStop(Colors.Red, 1.0));
            rojo_slider.Background = gradienteRojo;

            gradienteVerde.GradientStops.Add(new GradientStop(Colors.Black, 0.0));
            gradienteVerde.GradientStops.Add(new GradientStop(Colors.Green, 1.0));
            verde_slider.Background = gradienteVerde;

            gradienteAzul.GradientStops.Add(new GradientStop(Colors.Black, 0.0));
            gradienteAzul.GradientStops.Add(new GradientStop(Colors.Blue, 1.0));
            azul_slider.Background = gradienteAzul;
        }

        private void anadir_par_Click(object sender, RoutedEventArgs e)
        {
            NumericTextBox xN = new NumericTextBox();
            xN.Text = "0";
            NumericTextBox yN = new NumericTextBox();
            yN.Text = "0";
            Button bN = new Button();
            bN.Content = "Del";
            bN.Click += eliminar_par_Click;

            x.Add(xN);
            y.Add(yN);
            botones.Add(bN);

            refrescarGrid_puntos();
        }

        public void eliminar_par_Click(object sender, RoutedEventArgs e)
        {
            int i = 0;
            foreach (Button li in botones)
            {
                if (li.Equals(sender))
                {
                    break;
                }
                i++;
            }
            x.RemoveAt(i);
            y.RemoveAt(i);
            botones.RemoveAt(i);
            
            refrescarGrid_puntos();
        }

        private void aplicar_puntos_Click(object sender, RoutedEventArgs e)
        {
            if (evento != null)
                evento(this, new EventArgs());
        }

        private void ac_puntos_Click(object sender, RoutedEventArgs e)
        {
            grid_puntos_init();
        }

        private void grid_puntos_init()
        {
            x.Clear();
            y.Clear();
            botones.Clear();

            NumericTextBox xN = new NumericTextBox();
            xN.Text = "0";
            NumericTextBox yN = new NumericTextBox();
            yN.Text = "0";

            Button bN = new Button();
            bN.Content = "Del";
            bN.Click += eliminar_par_Click;

            x.Add(xN);
            y.Add(yN);
            botones.Add(bN);

            refrescarGrid_puntos();
        }

        private void refrescarGrid_puntos()
        {
            int i = 0;
            grid_puntos.Children.Clear();
            grid_puntos.ColumnDefinitions.Clear();
            grid_puntos.RowDefinitions.Clear();

            grid_puntos.ColumnDefinitions.Add(new ColumnDefinition());
            grid_puntos.ColumnDefinitions.Add(new ColumnDefinition());
            grid_puntos.ColumnDefinitions.Add(new ColumnDefinition());

            i = 0;
            foreach (NumericTextBox xi in x)
            {
                grid_puntos.RowDefinitions.Add(new RowDefinition());
                grid_puntos.Children.Add(xi);
                Grid.SetRow(xi, i);
                Grid.SetColumn(xi, 0);
                i++;
            }

            i = 0;
            foreach (NumericTextBox yi in y)
            {
                grid_puntos.Children.Add(yi);
                Grid.SetRow(yi, i);
                Grid.SetColumn(yi, 1);
                i++;
            }

            i = 0;
            foreach (Button bi in botones)
            {
                grid_puntos.Children.Add(bi);
                Grid.SetRow(bi, i);
                Grid.SetColumn(bi, 2);
                i++;
            }

            if (evento != null)
                evento(this, new EventArgs());
        }

        private void rojo_slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            color_linea = Color.FromArgb(255, Convert.ToByte(rojo_slider.Value), Convert.ToByte(verde_slider.Value), Convert.ToByte(azul_slider.Value));
            if (eventoColor != null)
                eventoColor(this, new EventArgs());
        }

        private void verde_slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            color_linea = Color.FromArgb(255, Convert.ToByte(rojo_slider.Value), Convert.ToByte(verde_slider.Value), Convert.ToByte(azul_slider.Value));
            if (eventoColor != null)
                eventoColor(this, new EventArgs());
        }

        private void azul_slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            color_linea = Color.FromArgb(255, Convert.ToByte(rojo_slider.Value), Convert.ToByte(verde_slider.Value), Convert.ToByte(azul_slider.Value));
            if (eventoColor != null)
                eventoColor(this, new EventArgs());
        }

        private void GridGlobal_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (GridGlobal.ActualWidth < 150)
            {
                panel_tipo_linea.Orientation = Orientation.Vertical;
                panel_tipo_linea.HorizontalAlignment = HorizontalAlignment.Left;
            }else
            {
                panel_tipo_linea.Orientation = Orientation.Horizontal;
                panel_tipo_linea.HorizontalAlignment = HorizontalAlignment.Center;
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            if (eventoCierre != null)
                eventoCierre(this, new EventArgs());
        }

        private void anadir_elemento_Click(object sender, RoutedEventArgs e)
        {
            NumericTextBox nuevo = new NumericTextBox();
            TextBlock nuevo_tb = new TextBlock();
            TextBlock nuevo_x = new TextBlock();
            nuevo_x.Text = "x";
            int exponente = z.Count();
            string exponente_s = exponente.ToString();

            nuevo_tb.Text = exponente_s;
            nuevo_tb.FontSize = 10;
            nuevo_tb.LayoutTransform.Value.Scale(0.75, 0.75);
            nuevo_tb.LayoutTransform.Value.Translate(0,-5);

            nuevo.Text = "0";

            z.Add(nuevo);

            panel_expresion.Children.Add(nuevo);
            panel_expresion.Children.Add(nuevo_x);
            panel_expresion.Children.Add(nuevo_tb);
        }

        private void aplicar_expresion_Click(object sender, RoutedEventArgs e)
        {
            if (evento != null)
                evento(this, new EventArgs());
        }
    }
}