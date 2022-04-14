using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KayitSistemi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data Source=S0AP-SERHAT;Initial Catalog=kayit;Integrated Security=True");
        SqlDataAdapter da;
        SqlCommand cmd;
        DataSet ds;
        SqlDataReader dr;
        void listele_grid()
        {
            da = new SqlDataAdapter("SELECT ogr_no,ad,soyad,tel_no,resim FROM kisiler order by ad", con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds, "kisiler");
            dataGridView1.DataSource = ds.Tables["kisiler"];
            con.Close();
        }
        private void btnAra_Click(object sender, EventArgs e)
        {
            da = new SqlDataAdapter("select ogr_no,ad,soyad,tel_no,resim from kisiler where ad like '" + textBox1.Text + "%'", con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds, "kisiler");
            dataGridView1.DataSource = ds.Tables["kisiler"];
            con.Close();
            clearr();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            cmd = new SqlCommand();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "insert into kisiler (ogr_no,ad,soyad,tel_no,resim) values ('" + txtNo.Text + "','" + txtAd.Text + "','" + txtSoyad.Text + "','" + txtTel.Text + "','"+txtDosya.Text+"')";
            cmd.ExecuteNonQuery();
            MessageBox.Show(txtNo.Text + " no 'lu Kayıt Eklendi.");
            con.Close();
            listele_grid();

            clearr();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            cmd = new SqlCommand();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "update kisiler set ad='" + txtAd.Text + "',soyad='" + txtSoyad.Text + "',ogr_no='" + txtNo.Text + "',tel_no='" + txtTel.Text + "',resim='" + txtDosya.Text + "' where ogr_no='" + txtNo.Text + "'";
            cmd.ExecuteNonQuery();
            MessageBox.Show(txtNo.Text + " no 'lu Kaydi Güncellendi.");
            con.Close();
            listele_grid();

            clearr();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            cmd = new SqlCommand();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "DELETE FROM kisiler WHERE ogr_no=" + txtNo.Text + "";
            cmd.ExecuteNonQuery();
            MessageBox.Show(txtNo.Text + " nolu kayit silindi.");
            con.Close();
            listele_grid();

            clearr();
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
           
            txtNo.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtAd.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtSoyad.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            txtTel.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            txtDosya.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            

        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
            listele_grid();
            clearr();
        }
        void clearr()
        {
            txtAd.Clear();
            txtNo.Clear();
            txtSoyad.Clear();
            txtTel.Clear();
            txtDosya.Clear();
        }

        private void btnGeri_Click(object sender, EventArgs e)
        {
            listele_grid();
            clearr();
        }
     
        private void btnFoto_Click(object sender, EventArgs e)
        {
            OpenFileDialog dosya = new OpenFileDialog();
            dosya.Filter = "Resim Dosyası |*.jpg;*.nef;*.png |  Tüm Dosyalar |*.*";
            dosya.ShowDialog();
            string dosyayolu = dosya.FileName;
            txtDosya.Text = dosyayolu;
            pictureBox1.ImageLocation = dosyayolu;
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            pictureBox1.ImageLocation = txtDosya.Text;
            
            
        }
    }
}
