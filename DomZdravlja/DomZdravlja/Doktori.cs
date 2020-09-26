using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace DomZdravlja
{
    public partial class Doktori : Form
    {
       //SqlConnection con= new SqlConnection("server=DESKTOP-2S3AHT9\\SQLEXPRESS;initial catalog=Dom;integrated security = true");
        SqlConnection con = new SqlConnection(@"Server=(localdb)\v11.0;AttachDbFilename=C:\Log\Dom.mdf;Database=Dom;Trusted_Connection=Yes;");
        SqlCommand command;
        int globablId;
    
        

        public Doktori()
        {
            InitializeComponent();
            napuniCombo();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            //Dodavanje doktora
            try
            {
                
                string komanda = "INSERT INTO Dom.dbo.Doktor (ime,prezime) VALUES (@ime,@prezime)";
                command = new SqlCommand(komanda, con);

                command.Parameters.Add("@ime",SqlDbType.NVarChar);
                command.Parameters.Add("@prezime", SqlDbType.NVarChar);

                command.Parameters["@ime"].Value = textBox1.Text;
                command.Parameters["@prezime"].Value = textBox2.Text;


                con.Open();
                command.ExecuteNonQuery();
                MessageBox.Show("Doktor ubacen");
                con.Close();
                napuniCombo();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }

        }

        private void Doktori_Load(object sender, EventArgs e)
        {

        }
        public void napuniCombo()
        {

            try
            {

                Dictionary<int, string> recnik = new Dictionary<int, string>();
                string komanda = "SELECT * FROM Dom.dbo.Doktor";
                command = new SqlCommand(komanda, con);
                con.Open();
                SqlDataReader reader = command.ExecuteReader();


                while (reader.Read())
                {
                    string ime = reader.GetString(0);
                    string prezime = reader.GetString(2);
                    int id = reader.GetInt32(1);
                    //MessageBox.Show(id.ToString());
                    string punoIme = ime + " " + prezime;

                    recnik.Add(id, punoIme);
                    //MessageBox.Show(item.Value.ToString());

                    






                }
                reader.Close();
                comboBox1.DataSource = new BindingSource(recnik, null);
                comboBox1.DisplayMember = "Value";
                comboBox1.ValueMember = "Key";


                command.ExecuteNonQuery();

                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
            }
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            //globablId = ((KeyValuePair<int, string>)comboBox1.SelectedItem).Value;

            try
            {
                string ime, prezime;
                string[] niz = comboBox1.Text.Split(' '); ;
                ime = niz[0];
                prezime = niz[1];
                globablId = Convert.ToInt32(comboBox1.SelectedValue.ToString());
                //MessageBox.Show(comboBox1.SelectedValue.ToString());
                textBox1.Text = ime;
                textBox2.Text = prezime;
            }
            catch (Exception)
            {


            }
            

        }

        private void Button2_Click(object sender, EventArgs e)
        {
            string com = String.Format("Update  Dom.dbo.Doktor set ime='{0}',prezime='{1}' where id='{2}'",textBox1.Text,textBox2.Text,globablId);
            command = new SqlCommand(com, con);
            try
            {
                con.Open();
                command.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Azuriran doktor");
                napuniCombo();
            }
            catch (Exception)
            {

                MessageBox.Show("Doslo je do greske pri Azuriranju");
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            string com = String.Format("DELETE FROM  Dom.dbo.Doktor where id='{0}'", globablId);
            command = new SqlCommand(com, con);
            try
            {
                con.Open();
                command.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Izbrisan doktor");
                napuniCombo();
            }
            catch (Exception)
            {

                MessageBox.Show("Doslo je do greske pri brisanju");
            }
        }
    }
}
