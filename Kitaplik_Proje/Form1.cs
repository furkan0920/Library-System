using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Data.Common;

namespace Kitaplik_Proje
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        OleDbConnection baglanti = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data
            Source=C:\Users\Furkan Ayakdaş\Desktop\kitaplık.mdb");

        void listele()
        {
            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter("Select * From Kitaplar",baglanti);
            da.Fill(dt);
            dataGridView1.DataSource = dt;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listele();
        }

        private void btnlistele_Click(object sender, EventArgs e)
        {
            listele();
        }
        string durum="";
        private void btnkaydet_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            OleDbCommand komut1 = new OleDbCommand("insert into Kitaplar(KitapAd,Yazar,Tur,Sayfa,Durum) values (@p1,@p2,@p3,@p4,@p5)", baglanti);
            komut1.Parameters.AddWithValue("@p1", Txtad.Text);
            komut1.Parameters.AddWithValue("@p2",Txtyazar.Text);
            komut1.Parameters.AddWithValue("@p3", cmbtur.Text);
            komut1.Parameters.AddWithValue("@p4", Txtsayfa.Text);
            komut1.Parameters.AddWithValue("@p5", durum);
            komut1.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kitap Sisteme Kaydedildi","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);
            listele();
        
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            durum = "0";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            durum="1";
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            Txtid.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            Txtad.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString(); 
            Txtyazar.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            cmbtur.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            Txtsayfa.Text=dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            //if (dataGridView1.Rows[secilen].Cells[5].Value.ToString == "True")
            //{
            //    radioButton2.Checked=true;

            //}
            //else
            //{
            //    radioButton1.Checked = true;
            //}
            


        }

        private void btnsil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            OleDbCommand komut=new OleDbCommand("Delete From Kitaplar Where Kitapid=@p1",baglanti);
            komut.Parameters.AddWithValue("@p1", Txtid.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kitap Lİsteden Silindi","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            listele();
        }

        private void btnguncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            OleDbCommand komut2 = new OleDbCommand("update kitaplar set Kitapad=@p1,Yazar=@p2,Tur=@p3,Sayfa=@p4,Durum=@p5 where Kitapid=@p6", baglanti);

            komut2.Parameters.AddWithValue("@p1", Txtid.Text);
            komut2.Parameters.AddWithValue("@P2", Txtyazar.Text);
            komut2.Parameters.AddWithValue("@p3", cmbtur.Text);
            komut2.Parameters.AddWithValue("@p4", Txtsayfa.Text);
            if (radioButton1.Checked==true)
            {
                komut2.Parameters.AddWithValue("@p5", durum);

            }
            if (radioButton2.Checked==true)
            {
                komut2.Parameters.AddWithValue("@p5", durum);   
            }
            komut2.Parameters.AddWithValue("@p6", Txtad.Text);
            komut2.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kayit Güncellendi", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
        }

        private void Btnbul_Click(object sender, EventArgs e)
        {
            OleDbCommand komut=new OleDbCommand("Select * From Kitaplar Where KitapAd=@p1",baglanti);
            komut.Parameters.AddWithValue("@p1", Txtbul.Text);
            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter(komut);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            
        }

        private void Btnara_Click(object sender, EventArgs e)
        {
            OleDbCommand komut = new OleDbCommand("Select * From Kitaplar Where KitapAd like '%"+Txtbul.Text+"%'", baglanti);           
            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter(komut);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
    }
}
 