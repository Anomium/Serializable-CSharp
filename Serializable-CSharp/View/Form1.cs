using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Serializable_CSharp.Controller;
using Serializable_CSharp.Model;

namespace Serializable_CSharp
{
    public partial class Form1 : Form
    {

        private CarroController carroco = new CarroController();
        private Object Index = null;
        public Form1()
        {
            InitializeComponent();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btn_Guardar_Click(object sender, EventArgs e)
        {

            if (btn_Guardar.Text == "Guardar")
            {
                carroco.Create(new Carro(txt_Marca.Text.ToUpper(), txt_Color.Text.ToUpper(), Convert.ToDouble(txt_Precio.Text)));
                listar(tbl_Tabla, carroco.ReadAll());
                btn_Buscar.Enabled = true;
            }
            if (btn_Guardar.Text == "Guardar Cambios")
            {
                carroco.Update((int)Index, new Carro(txt_Marca.Text.ToUpper(), txt_Color.Text.ToUpper(), Convert.ToDouble(txt_Precio.Text)));
                listar(tbl_Tabla, carroco.ReadAll());
                btn_Guardar.Text = "Guardar";
                btn_Cancelar.Enabled = false;
                btn_Modificar.Enabled = true;
            }
        }

        public void listar(DataGridView tabla, List<string[]> Lista)
        {
            if (tabla.RowCount != 0)
            {
                tabla.Rows.Clear();
            }
            
            for (int i = 0; i < Lista.Count; i++)
            {
                tabla.Rows.Insert(i, Lista[i]);
            }
        }

        private void btn_Eliminar_Click(object sender, EventArgs e)
        {
            carroco.Delete((int) Index);
            listar(tbl_Tabla, carroco.ReadAll());
        }

        private void tbl_Tabla_MouseClick(object sender, MouseEventArgs e)
        {
            Index = (int) tbl_Tabla.CurrentRow.Index;
            btn_Eliminar.Enabled = true;
            btn_Cancelar.Enabled = true;
            btn_Modificar.Enabled = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CenterToScreen();
            if (carroco.Carros.Count == 0)
            {
                btn_Eliminar.Enabled = false;
                btn_Cancelar.Enabled = false;
                btn_Modificar.Enabled = false;
                btn_CancelarBusqueda.Enabled = false;
                btn_Buscar.Enabled = false;
            }
            
        }

        private void btn_Modificar_Click(object sender, EventArgs e)
        {

            if (MessageConfirm("Desea Modificar"))
            {
                btn_Guardar.Text = "Guardar Cambios";
                btn_Modificar.Enabled = false;
                btn_Cancelar.Enabled = true;
            }
            else
            {
                Index = null;
            }
            
        }

        public Boolean MessageConfirm(string Texto)
        {
            DialogResult result = MessageBox.Show(Texto, "Salir", MessageBoxButtons.YesNoCancel);
            if (result == DialogResult.Yes)
            {
                return true;
            }
            else if (result == DialogResult.No)
            {
                return false;
            }
            else if (result == DialogResult.Cancel)
            {
                return false;
            }
            else
            {
                return false;
            }
        }

        private void btn_Cancelar_Click(object sender, EventArgs e)
        {
            btn_Guardar.Text = "Guardar";
            btn_Modificar.Enabled = true;
            btn_Eliminar.Enabled = true;
            btn_Cancelar.Enabled = false;
        }

        private void btn_Buscar_Click(object sender, EventArgs e)
        {
            BuscarTodo(txt_Buscar.Text, tbl_Tabla, carroco.Read(txt_Buscar.Text.ToUpper()), btn_CancelarBusqueda);
        }

        public void BuscarTodo(String Filter, DataGridView tabla, List<string[]> List, Button boton)
        {
            if (Filter == null || Filter == String.Empty)
            {
                MessageBox.Show("Los datos ingresados deben ser validos");
            }
            else if (List.Count <= 0 || List == null)
            {
                MessageBox.Show("No se ha encontrado coincidencias");
            }
            else
            {
                listar(tabla, List);
                boton.Enabled = true;
            }
        }

        private void btn_CancelarBusqueda_Click(object sender, EventArgs e)
        {
            listar(tbl_Tabla, carroco.ReadAll());
            btn_CancelarBusqueda.Enabled = false;
        }
    }
}
