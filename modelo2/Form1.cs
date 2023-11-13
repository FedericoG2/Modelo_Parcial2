using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace modelo2
{
    public partial class Form1 : Form
    {
        // Creo la estructura 
        public struct REPARACION
        {
            public string Cliente;
            public string Dispositivo;
            public string Reparacion;
            public int FormaPago;
            public float Importe;
        };
        // Declaro el arreglo de tipo estructura

        public REPARACION[] reparaciones;

        //Declaro la posicion en el arreglo

        public int Posicion;

        //Maxima cantidad de elementos

        const int MAX = 30;
        
        //Variables

        float ImporteTotal = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Se carga el formulario y la etiqueta cliente y total se limpian.
            //El boton pago contado ya aparece seleccionado por default

            txtCliente.Text = "";
            txtTotal.Text = "";
            optContado.Checked = true;

            // Combos

            //Limpio los items , cargo y preselecciono el primero
            
            cmbDispositivo.Items.Clear();
            cmbDispositivo.Items.Add("Smartphone");
            cmbDispositivo.Items.Add("Tablet");
            cmbDispositivo.Items.Add("Consola de Videojuego");
            cmbDispositivo.SelectedIndex = 0;
            //
            cmbReparación.Items.Clear();
            cmbReparación.Items.Add("Pantalla");
            cmbReparación.Items.Add("Teclado");
            cmbReparación.Items.Add("Sistema Operativo");
            cmbReparación.SelectedIndex = 0;

            //Se carga el boton registrar y lo quiero en falso

            btnRegistrar.Enabled = false;

            // Crear el arreglo de 30 elementos
            reparaciones = new REPARACION[MAX];
           
            // Indice en cero para empezar a cargar
            Posicion = 0; 
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            //Limpia el nombre del cliente y el total 

            txtCliente.Text = "";
            txtTotal.Text = "";
            

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            //Cierra el formulario
            Close();
        }

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            //Variable total va a ir cambiando su valor durante el programa
            float total = 0;

            //Variable descuento o recargo

            float dto_rec = 0; 
            
            // Valor total segùn el dispositivo seleccionado por el usuario

            //Puede ser con switch o una sucesion de if sin anidar,sueltos

            switch(cmbDispositivo.SelectedIndex)
            {
                case 0:
                    total = 2500;
                    break;
                case 1:
                    total = 1850;
                    break;
                case 2:
                    total = 3000;
                    break;
            }

            
            // Al valor del dispositivo le sumo el costo de la reparación

            //Un nuevo total

            switch(cmbReparación.SelectedIndex)
            {
                case 0:
                    total = total + 2500;
                    break;
                case 1:
                    total = total + 1500;
                    break;
                case 2:
                    total = total + 1000;
                    break;
            }

            // Calcular el descuento o recargo y aplicarlo en el ultimo valor de total

            if(optContado.Checked)
            {
                // 20% de descuento
                dto_rec = total * 20 / 100;

                // Importe final
                total = total - dto_rec;
            }
            // 5% de recargo

            else
            {
                dto_rec = total * 5 / 100;

                // Importe final
                total = total + dto_rec;
            }

            // Mostrar el resultado en la etiqueta

            txtTotal.Text = total.ToString();

            //Reasigno total que era local a global en la variable ImporteTotal

            ImporteTotal = total;


            // Habilitar el botón Registrar

            btnRegistrar.Enabled = true;
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {

           //Si el nombre del cliente no esta vacio 

            //Guardo en el arreglo
            //Muevo la posicion en el arreglo
            //Pregunto si posicion es igual al maximo o mayor deshabilito el boton calcular 

            if (txtCliente.Text != "")
            {
                reparaciones[Posicion].Cliente = txtCliente.Text;
                reparaciones[Posicion].Dispositivo = cmbDispositivo.Text;
                reparaciones[Posicion].Reparacion = cmbReparación.Text;

                if(optContado.Checked)
                {
                    reparaciones[Posicion].FormaPago = 1; // contado
                }
                else
                {
                    reparaciones[Posicion].FormaPago = 2; // credito
                }
                //Importe total es global

                reparaciones[Posicion].Importe = ImporteTotal;

                //Posicion en el arreglo

                Posicion++;

                if(Posicion >= MAX)
                {
                    //Deshabilito calcular
                    btnCalcular.Enabled = false;
                    MessageBox.Show("Datos completos, no se pueden agregar más reparaciones");
                }
                
                //Deshabilito registrar
                btnRegistrar.Enabled = false;


                // Limpiar el formulario , el nombre cliente y la etiqueta total
                txtCliente.Text = "";
                txtTotal.Text = "";
            }
            else {
                //Aparece cuando se hace click en registrar sin tener el nombre o la condicion que pida
                MessageBox.Show("Complete los datos requeridos");
            }

        }
    }
}
