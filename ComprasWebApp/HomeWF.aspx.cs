using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


namespace ComprasWebApp
{
    public partial class HomeWF : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.BindGrid();
            }
            btnGuardar.Click += new EventHandler(this.btnGuardar_Click);
            btnQuitarFiltros.Click += new EventHandler(this.btnQuitarFiltros_Click);
            ddlNumeroCompra.TextChanged += new EventHandler(this.ddlNumeroCompra_OnSelectedIndexChanged);    
        }


        private void BindGrid()
        {
            string constr = ConfigurationManager.ConnectionStrings["conDB"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("sp_Compras_CRUD"))
                {
                    cmd.Parameters.AddWithValue("@Action", "SELECT");
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            gvCompras.DataSource = dt;
                            gvCompras.DataBind();

                            ddlNumeroCompra.DataSource = dt;
                            ddlNumeroCompra.DataTextField = "NumeroCompra";
                            ddlNumeroCompra.DataValueField = "NumeroCompra";
                            ddlNumeroCompra.DataBind();
                            ddlNumeroCompra.SelectedIndex = 0;


                        }
                    }
                }
            }

        }

        protected void ddlNumeroCompra_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            int NumeroCompra = Convert.ToInt32(ddlNumeroCompra.SelectedItem.ToString());
            string constr = ConfigurationManager.ConnectionStrings["conDB"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("sp_Select_Numero_Compra"))
                {
                    cmd.Parameters.AddWithValue("@NumeroCompra", NumeroCompra);
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            gvCompras.DataSource = dt;
                            gvCompras.DataBind();
                        }
                    }
                }
            }
        }


        protected void OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int IDCompras = Convert.ToInt32(gvCompras.DataKeys[e.RowIndex].Values[0]);
            string constr = ConfigurationManager.ConnectionStrings["conDB"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("sp_Compras_CRUD"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Action", "DELETE");
                    cmd.Parameters.AddWithValue("@IDCompras", IDCompras);
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            this.BindGrid();
        }

        protected void OnRowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = gvCompras.Rows[e.RowIndex];
            int IDCompras = Convert.ToInt32(gvCompras.DataKeys[e.RowIndex].Values[0]);
            string NumeroCompra = (row.FindControl("txtNumeroCompra") as TextBox).Text;
            string MontoCompra = (row.FindControl("txtMontoCompra") as TextBox).Text;
            string NombreCliente = (row.FindControl("txtNombreCliente") as TextBox).Text;
            string constr = ConfigurationManager.ConnectionStrings["conDB"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("sp_Compras_CRUD"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Action", "UPDATE");
                    cmd.Parameters.AddWithValue("@IDCompras", IDCompras);
                    cmd.Parameters.AddWithValue("@NumeroCompra", NumeroCompra);
                    cmd.Parameters.AddWithValue("@MontoCompra", MontoCompra);
                    cmd.Parameters.AddWithValue("@NombreCliente", NombreCliente);
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            gvCompras.EditIndex = -1;
            this.BindGrid();
        }


        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowIndex != gvCompras.EditIndex)
            {
                (e.Row.Cells[4].Controls[2] as LinkButton).Attributes["onclick"] = "return confirm('Deseas eliminar el registro seleccionado?');";
            }
        }

        protected void OnRowEditing(object sender, GridViewEditEventArgs e)
        {
            gvCompras.EditIndex = e.NewEditIndex;
            this.BindGrid();
        }

        protected void OnRowCancelingEdit(object sender, EventArgs e)
        {
            gvCompras.EditIndex = -1;
            this.BindGrid();
        }

        //void gvCompras_PageIndexChanging(Object sender, GridViewPageEventArgs e)
        //{
        //    gvCompras.PageIndex = e.NewPageIndex;
        //    this.BindGrid();
        //}



        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            var sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["conDB"].ConnectionString);
            var sqlSP = "sp_Insert_Compra";
            var command = new SqlCommand(sqlSP, sqlConn);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@NumeroCompra", SqlDbType.Int)).Value = txtNumCompra.Text;
            command.Parameters.Add(new SqlParameter("@MontoCompra", SqlDbType.Decimal)).Value = txtMontoCompra.Text;
            command.Parameters.Add(new SqlParameter("@NombreCliente", SqlDbType.VarChar)).Value = txtNombreCliente.Text;


            sqlConn.Open();
            command.ExecuteNonQuery();
            sqlConn.Close();

            this.BindGrid();

        }

        protected void btnQuitarFiltros_Click(object sender, EventArgs e)
        {
            this.BindGrid();
        }
    }
}