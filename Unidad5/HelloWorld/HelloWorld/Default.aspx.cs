using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HelloWorld
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Label1.Text = "Hola ";
            if (TextBox1.Text != "")
                Label1.Text += TextBox1.Text;
            else
                Label2.Text = "Por favor, ingrese su nombre.";
            TextBox1.Text = "";
        }
    }
}