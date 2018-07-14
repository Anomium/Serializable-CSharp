using System;
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
                carroco.Create(new Carro(txt_Marca.Text, txt_Color.Text, Convert.ToDouble(txt_Precio.Text)));
                listar(tbl_Tabla, carroco.ReadAll());
            }
            if (btn_Guardar.Text == "Guardar Cambios")
            {
                carroco.Update((int)Index, new Carro(txt_Marca.Text, txt_Color.Text, Convert.ToDouble(txt_Precio.Text)));
                listar(tbl_Tabla, carroco.ReadAll());
                btn_Guardar.Text = "Guardar";
            }
        }

        public void listar(DataGridView tabla, List<string[]> Lista)
        {
            if (tabla.RowCount != 0)
            {
                tabla.Rows.Clear();
            }
            
            for (int i = 0; i < carroco.Carros.Count; i++)
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
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CenterToScreen();
        }

        private void btn_Modificar_Click(object sender, EventArgs e)
        {

            if (MessageConfirm("Desea Modificar"))
            {
                btn_Guardar.Text = "Guardar Cambios";
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

        }
    }
}
