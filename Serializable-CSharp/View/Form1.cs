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
            try
            {
                if (btn_Guardar.Text == "Guardar")
                {
                    if (ValVacio(txt_Marca, txt_Color, txt_Precio))
                    {
                        carroco.Create(new Carro(txt_Marca.Text.ToUpper(), txt_Color.Text.ToUpper(), Convert.ToDouble(txt_Precio.Text)));
                        listar(tbl_Tabla, carroco.ReadAll());
                        btn_Buscar.Enabled = true;
                        BorrarText(txt_Marca, txt_Color, txt_Precio, true);
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Verifique que los campos esten correctos.", "Error");
            }
            try
            {
                if (btn_Guardar.Text == "Guardar Cambios")
                {
                    if (ValVacio(txt_Marca, txt_Color, txt_Precio))
                    {
                        carroco.Update((int)Index, new Carro(txt_Marca.Text.ToUpper(), txt_Color.Text.ToUpper(), Convert.ToDouble(txt_Precio.Text)));
                        listar(tbl_Tabla, carroco.ReadAll());
                        BorrarText(txt_Marca, txt_Color, txt_Precio, true);
                        btn_Guardar.Text = "Guardar";
                        chbx_Seleccionado.Checked = false;
                        btn_Cancelar.Enabled = false;
                        btn_Modificar.Enabled = true;
                        Index = null;
                    }

                }
            }
            catch (Exception)
            {

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
            carroco.Delete((int)Index);
            BorrarText(txt_Marca, txt_Color, txt_Precio, true);
            listar(tbl_Tabla, carroco.ReadAll());
            Index = null;
            chbx_Seleccionado.Checked = false;
        }

        private void tbl_Tabla_MouseClick(object sender, MouseEventArgs e)
        {
            Index = (int)tbl_Tabla.CurrentRow.Index;
            txt_Marca.Text = tbl_Tabla.CurrentRow.Cells[0].Value.ToString();
            txt_Color.Text = tbl_Tabla.CurrentRow.Cells[1].Value.ToString();
            txt_Precio.Text = tbl_Tabla.CurrentRow.Cells[2].Value.ToString();

            txt_Marca.Enabled = false;
            txt_Color.Enabled = false;
            txt_Precio.Enabled = false;

            chbx_Seleccionado.Checked = true;
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

                txt_Marca.Enabled = true;
                txt_Color.Enabled = true;
                txt_Precio.Enabled = true;
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
            BorrarText(txt_Marca, txt_Color, txt_Precio, true);
            chbx_Seleccionado.Checked = false;
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

        public void BorrarText(TextBox marca, TextBox color, TextBox precio, Boolean cond)
        {
            marca.Text = null;
            color.Text = null;
            precio.Text = null;

            marca.Enabled = cond;
            color.Enabled = cond;
            precio.Enabled = cond;
        }

        public Boolean ValVacio(TextBox marca, TextBox color, TextBox precio)
        {
            try
            {
                if (marca.Text == null || marca.Text == "" || marca.Text == String.Empty ||
                color.Text == null || color.Text == "" || color.Text == String.Empty ||
                precio.Text == null || precio.Text == "" || precio.Text == String.Empty)
                {
                    MessageBox.Show("No se aceptan campos vacios","Error");
                    return false;
                }
                else
                {
                    return true;
                }
            } catch (Exception)
            {
                return false;
            }
        }
    }
}
