using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Practica1_2S2018_201212527
{
    public partial class Form1 : Form
    {

        List<string> ListaTokens;//lista token
        List<string> Tipo;
        List<int> ListaFila;
        List<int> ListaColumna;
        List<string> ListaError;
        List<int> ListaFilaError;
        List<int> ListaColumnaError;
        int numeroNodo = 0;


        ArrayList ListaPestaña = new ArrayList();
        string direccionescritorio = @"C:\Users\HECTOR\Desktop";
        int ContarPestaña = 0;
        int columna = 0;
        int fila = 1;
        string Palabra = "";

        public Form1()
        {
            InitializeComponent();
            ListaTokens = new List<string>();
            Tipo = new List<string>();
            ListaFila = new List<int>();
            ListaColumna = new List<int>();
            ListaError = new List<string>();
            ListaFilaError = new List<int>();
            ListaColumnaError = new List<int>();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        //Metodo para guardar
        private void guardarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RichTextBox temp = new RichTextBox();
            temp = (RichTextBox)tabControl1.SelectedTab.Controls[0];
            System.IO.StreamWriter file = new System.IO.StreamWriter("C:\\Users\\luiis\\Desktop\\" + "Organigrama " + ContarPestaña + ".ogm");
            file.WriteLine(temp.Text);
            MessageBox.Show("Organigrama guardado en el escritorio", "Guardar");
            file.Close();
        }


        //Metodo para crear Txt
        StreamWriter file;
        public void CrearTxt()
        {
            //RichTextBox temp = new RichTextBox();
            //temp = (RichTextBox)tabControl1.SelectedTab.Controls[0];
            file = new System.IO.StreamWriter("C:\\Users\\HECTOR\\Desktop\\" + "Organigrama " + ".txt");
            //file.WriteLine(temp.Text);
            MessageBox.Show("Archivo guardado en el escritorio", "Guardar");
            //file.Close();

        }
        //Metodo para Crear Pestañas
        public void CrearPestaña()
        {
            TabPage NuevaPestaña = new TabPage("Organigrama " + ContarPestaña);
            RichTextBox texto = new RichTextBox();
            texto.SetBounds(0, 0, 445, 364);
            ListaPestaña.Add(NuevaPestaña);
            tabControl1.TabPages.Add(NuevaPestaña);
            ContarPestaña++;
            tabControl1.SelectedTab = NuevaPestaña;
            NuevaPestaña.Controls.Add(texto);
        }
        //Metodo para Crear los HTML
        public void ArchivosSalida()
        {

            try
            {

                StreamWriter limpiar = new StreamWriter(direccionescritorio + @"\Lista de Tokens.html");
                limpiar.Write("");
                //MessageBox.Show("se limpio el archivo");
                limpiar.Close();
                StreamWriter escribir = File.AppendText(direccionescritorio + @"\Lista de Tokens.html");
                escribir.WriteLine("<html>\n<head>\n<title>Lista tokens</title>\n</head>\n<body>\n<table border=\"2px\">\n<tr>\n<td><strong>No.</strong></td>\n<td><strong>Lexema</strong></td>\n<td><strong>Tipo</strong></td>\n<td><strong>Fila</strong></td>\n<td><strong>Columna</strong></td>\n</tr>\n");
                for (int i = 0; i < ListaTokens.Count; i++)
                {

                    escribir.WriteLine("<tr>\n<td>" + i + "</td>\n<td>" + ListaTokens[i] + "</td>\n<td>" + Tipo[i] + "</td>\n<td>" + ListaFila[i] + "</td>\n<td>" + ListaColumna[i] + "</td></tr>");

                }
                escribir.WriteLine("</table></body>\n</html>");
                escribir.Close();





                StreamWriter error = new StreamWriter(direccionescritorio + @"\Lista de Errores.html");
                error.Write("");
                error.Close();
                StreamWriter escribirerror = File.AppendText(direccionescritorio + @"\Lista de Errores.html");
                escribirerror.WriteLine("<html><head><title>Errores lexicos</title></head><body><table border=\"2px\"><tr><td>NO</td><td>Error</td><td>Descripcion</td><td>Fila</td><td>Columna</td></tr>");
                for (int i = 0; i < ListaError.Count; i++)
                {
                    escribirerror.WriteLine("<tr><td>" + i + "</td><td>" + ListaError[i] + "</td><td>Caracter Desconocido</td><td>" + ListaFilaError[i] + "</td><td>" + ListaColumnaError[i] + "</td></tr>");
                }
                escribirerror.WriteLine("</table></body>\n</html>");
                escribirerror.Close();


                MessageBox.Show("Reportes generados correctamente.", "Archivos de Salida", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }

            catch
            {
                MessageBox.Show("Error al crear archivo de tokens", "Archivos de Salida", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }

        }

        //Metodo que analiza el archivo de entrada
        public void Analizador(string cadena)
        {
            CrearTxt();//archivo texto 
            file.WriteLine("digraph grafica{");
            file.WriteLine("rankdir=TB;");
            file.WriteLine("node [shape = record, style=filled, fillcolor=seashell2];");
            //cadena = cadena + "#";
            char[] analizador = cadena.ToCharArray();
            int estado = 0;
            for (int i = 0; i < analizador.Length; i++)
            {
                Console.WriteLine("Estado " + estado + "numero " + i + " caracter " + analizador[i]);
                switch (estado)
                {
                    //Estado No.0
                    case 0:

                        if (char.IsLetter(analizador[i]))
                        {
                            Palabra += analizador[i].ToString();
                            columna++;
                            estado = 1;
                            continue;
                        }

                        else if (char.IsNumber(analizador[i]))
                        {
                            Palabra += analizador[i].ToString();
                            columna++;
                            estado = 2;
                            continue;
                        }

                        else if (analizador[i] == ':')
                        {

                            Palabra += analizador[i].ToString();

                            ListaTokens.Add(Palabra);
                            Tipo.Add("Dos Puntos");
                            ListaFila.Add(fila);
                            ListaColumna.Add(columna);
                            Palabra = "";

                            columna++;
                            estado = 0;
                            continue;
                        }

                        else if (analizador[i] == ';')
                        {
                            Palabra += analizador[i].ToString();

                            ListaTokens.Add(Palabra);
                            Tipo.Add("Punto y Coma");
                            ListaFila.Add(fila);
                            ListaColumna.Add(columna);
                            Palabra = "";

                            columna++;
                            estado = 0;
                            continue;
                        }

                        else if (analizador[i] == '{')
                        {
                            Palabra += analizador[i].ToString();

                            ListaTokens.Add(Palabra);
                            Tipo.Add("Llave Abre");
                            ListaFila.Add(fila);
                            ListaColumna.Add(columna);
                            Palabra = "";

                            columna++;
                            estado = 0;
                            continue;
                        }

                        else if (analizador[i] == '}')
                        {
                            Palabra += analizador[i].ToString();

                            ListaTokens.Add(Palabra);
                            Tipo.Add("Llave Cierra");
                            ListaFila.Add(fila);
                            ListaColumna.Add(columna);
                            Palabra = "";

                            columna++;
                            estado = 0;
                            continue;
                        }

                        else if (analizador[i] == ',')
                        {
                            Palabra += analizador[i].ToString();

                            ListaTokens.Add(Palabra);
                            Tipo.Add("Coma");
                            ListaFila.Add(fila);
                            ListaColumna.Add(columna);
                            Palabra = "";

                            columna++;
                            estado = 0;
                            continue;
                        }

                        else if (analizador[i] == '"')
                        {
                            Palabra += analizador[i].ToString();

                            ListaTokens.Add(Palabra);
                            Tipo.Add("Comillas Dobles");
                            ListaFila.Add(fila);
                            ListaColumna.Add(columna);
                            Palabra = "";

                            columna++;
                            estado = 0;
                            continue;
                        }


                        else if (analizador[i].Equals((char)10))//salto de liena
                        {
                            columna = 0;
                            fila++;
                            estado = 0;
                            continue;
                        }
                        else if (analizador[i].Equals((char)32))//espacio en blanco
                        {
                            columna++;
                            estado = 0;
                            continue;
                        }
                        else if (analizador[i].Equals((char)09))//TAB
                        {
                            columna = columna + 8;
                            estado = 0;
                            continue;
                        }


                        else
                        {

                            ListaError.Add(analizador[i].ToString());
                            estado = 0;
                            columna++;
                            ListaColumnaError.Add(columna);
                            ListaFilaError.Add(fila);

                        }

                        break;

                    //Estado No.1
                    case 1:

                        if (char.IsLetter(analizador[i]))
                        {
                            Palabra += analizador[i].ToString();
                            columna++;
                            estado = 1;
                            continue;
                        }
                        else if (analizador[i] == ':') //parala reservada
                        {

                            estado = 0;
                            Verificar(Palabra);
                            i--;
                            Palabra = "";

                        }
                        else if (analizador[i].Equals((char)10))//salto de liena
                        {
                            columna = 0;
                            fila++;
                            estado = 1;
                            continue;
                        }
                        else if (analizador[i].Equals((char)32))//espacio en blanco
                        {
                            Palabra += analizador[i].ToString();
                            columna++;
                            estado = 1;
                            continue;
                        }
                        else if (analizador[i].Equals((char)09))//TAB
                        {
                            columna = columna + 8;
                            estado = 1;
                            continue;
                        }
                        else if (analizador[i] == '"') // para saber si una cadena 
                        {

                            file.WriteLine("nodo"+numeroNodo + "[ label = \"Cod |" +Palabra+ "\"]");//escritura archivo
                            numeroNodo++;
                            ListaTokens.Add(Palabra);
                            Tipo.Add("Cadena");
                            ListaFila.Add(fila);
                            ListaColumna.Add(columna);
                            Palabra = "";
                            estado = 0;
                            i--;
                        }
                        else if (analizador[i] == ';' || analizador[i] == ',' ||  analizador[i] == '{' ||  analizador[i] == '}')
                        {
                            estado = 0;
                            i--;
                        }
                        else
                        {
                            ListaError.Add(analizador[i].ToString());
                            estado = 0;
                            columna++;
                            ListaColumnaError.Add(columna);
                            ListaFilaError.Add(fila);
                        }


                        break;




                    case 2:

                        if (char.IsNumber(analizador[i]))
                        {
                            Palabra += analizador[i].ToString();
                            columna++;
                            estado = 2;
                            continue;
                        }



                        else 
                        {


                            estado = 0;
                            ListaTokens.Add(Palabra);
                            Tipo.Add("Numero");
                            ListaFila.Add(fila);
                            ListaColumna.Add(columna);
                            Palabra = "";

                        }


                        break;



                }//fin del swi
            }//fin del for
            file.WriteLine("}");
            file.Close();//cierre del archivo 
        }
        //Metodo para encontrar las palabras claves en el archivo de entrada    
        public void Verificar(String palabraReservada)
        {
            switch (palabraReservada)
            {
                case "organigrama":


                    ListaTokens.Add(Palabra);
                    Tipo.Add("Palabra Reservada");
                    ListaFila.Add(fila);
                    ListaColumna.Add(columna);
                    columna = 11;


                    break;

                case "nombre":

                    ListaTokens.Add(Palabra);
                    Tipo.Add("Palabra Reservada");
                    ListaFila.Add(fila);
                    ListaColumna.Add(columna);
                    Palabra = "";
                    columna = 6;

                    break;

                case "trabajador":

                    ListaTokens.Add(Palabra);
                    Tipo.Add("Palabra Reservada");
                    ListaFila.Add(fila);
                    ListaColumna.Add(columna);
                    Palabra = "";
                    columna = 10;

                    break;

                case "codigo":

                    ListaTokens.Add(Palabra);
                    Tipo.Add("Palabra Reservada");
                    ListaFila.Add(fila);
                    ListaColumna.Add(columna);
                    Palabra = "";
                    columna = 6;

                    break;

                case "superiores":

                    ListaTokens.Add(Palabra);
                    Tipo.Add("Palabra Reservada");
                    ListaFila.Add(fila);
                    ListaColumna.Add(columna);
                    Palabra = "";
                    columna = 10;

                    break;

                default:

                    ListaTokens.Add(Palabra);
                    Tipo.Add("Error lexico ");
                    ListaFila.Add(fila);
                    ListaColumna.Add(columna);
                    Palabra = "";

                    break;
            }
        }
        //Metodo para pintar las palabras
        public void Pintar(String palabraPintada)
        {

        }
        //Metodo acerca de
        private void acercaDeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Luis ALfredo Vejo Mendoza - 201212527 - LFyP A-", "Acerca de...");
        }

        private void crearPestañaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CrearPestaña();
        }
        //Metodo para abrir archivo
        private void abrirArchivoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog abrir = new OpenFileDialog();
            abrir.Filter = "ogm (*.ogm)|*.ogm";
            if (abrir.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                System.IO.StreamReader sr = new
                System.IO.StreamReader(abrir.FileName);
                RichTextBox temp = new RichTextBox();
                temp = (RichTextBox)tabControl1.SelectedTab.Controls[0];
                temp.Text = sr.ReadToEnd();
                tabControl1.SelectedTab.Text = abrir.SafeFileName;
                sr.Close();


            }
        }

        private void analizarToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void analisisLéxicoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Analizador(tabControl1.SelectedTab.Controls[0].Text);
            MessageBox.Show("Archivo analizado correctamente", "Analizador Léxico");
            //Verificar();
        }

        private void archivosDeSalidaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ArchivosSalida();
        }
        //Metodo para guardar como...
        private void guardarComoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Desea guardar el organigrama?", "Guardar Como...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                SaveFileDialog Guardar = new SaveFileDialog();
                Guardar.Filter = "Organigrama|*.ogm";
                Guardar.Title = "Guardar Como...";
                Guardar.FileName = "Nombre";

                var varGuardar = Guardar.ShowDialog();
                if (varGuardar == DialogResult.OK)
                {
                    RichTextBox temp = new RichTextBox();
                    temp = (RichTextBox)tabControl1.SelectedTab.Controls[0];
                    System.IO.StreamWriter file = new System.IO.StreamWriter("C:\\Users\\luiis\\Desktop\\" + "Organigrama " + ContarPestaña + ".ogm");
                    file.WriteLine(temp.Text);
                    MessageBox.Show("Organigrama guardado en el escritorio", "Guardar");
                    file.Close();
                }


            }

        }
    }
}
