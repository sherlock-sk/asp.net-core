using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using productWithoutEF.Models;
using System;
using System.Data;
using System.Data.SqlClient;


namespace productWithoutEF.Controllers
{
    public class ProductsController : Controller
    {


        string ConnectionString = @"Data Source=SUBODH-PC;Initial Catalog=productdatabase;Integrated Security=True";

         [HttpGet]
        public ActionResult Index()
        {

            DataTable Products = new DataTable();
            using (SqlConnection sqlConn = new SqlConnection(ConnectionString))
            {

                sqlConn.Open();
               
                SqlDataAdapter tbldata = new SqlDataAdapter("SELECT * from Products", sqlConn);
                tbldata.Fill(Products);
            
            
            }
                return View(Products);
        }

        // GET: Products/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Products/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View(new ProductModel());
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductModel productModel)
        {

            using (SqlConnection sqlConn = new SqlConnection(ConnectionString)) { 
                sqlConn.Open();
                string query = "INSERT INTO Products VALUES(@Name,@Category,@Color,@UnitPrice,@AvailableQuantity,@CratedDate)";
                SqlCommand sqlcmd = new SqlCommand(query, sqlConn);

                sqlcmd.Parameters.AddWithValue("@Name", productModel.Name);
                sqlcmd.Parameters.AddWithValue("@Category", productModel.Category);
                sqlcmd.Parameters.AddWithValue("@Color", productModel.Color);
                sqlcmd.Parameters.AddWithValue("@UnitPrice", productModel.UnitPrice);
                sqlcmd.Parameters.AddWithValue("@AvailableQuantity", productModel.AvailableQuantity);
                sqlcmd.Parameters.AddWithValue("@CratedDate", productModel.CratedDate.Date);

                sqlcmd.ExecuteNonQuery();



            }
            return RedirectToAction("Index");



        }

        // GET: Products/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {

            ProductModel productModel = new ProductModel();
            DataTable dtable = new DataTable();
            using (SqlConnection sqlConn = new SqlConnection(ConnectionString)) {


                sqlConn.Open();
                string query = "SELECT * FROM Products where @ProductId=ProductId";
                SqlDataAdapter sda = new SqlDataAdapter(query, sqlConn);
                sda.SelectCommand.Parameters.AddWithValue("@ProductId", id);
                sda.Fill(dtable);
            
            
            
            }
            if (dtable.Rows.Count == 1) {

                productModel.ProductId = Convert.ToInt32(dtable.Rows[0][0].ToString());
                productModel.Name = dtable.Rows[0][1].ToString();
                productModel.Color = dtable.Rows[0][2].ToString();
                productModel.Category = dtable.Rows[0][3].ToString();
                productModel.UnitPrice = Convert.ToDecimal(dtable.Rows[0][4].ToString());
                productModel.AvailableQuantity = Convert.ToInt32(dtable.Rows[0][5].ToString());
                productModel.CratedDate = Convert.ToDateTime(dtable.Rows[0][6].ToString());

                return View(productModel);




            }
            else
                return RedirectToAction ("Index");
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductModel productModel)
        {

            using (SqlConnection sqlConn = new SqlConnection(ConnectionString))
            {
                sqlConn.Open();
                string query = "UPDATE Products SET Name=@Name,Category=@Category,Color=@Color,UnitPrice=@UnitPrice,AvailableQuantity=@AvailableQuantity,CratedDate=@CratedDate where ProductId=@ProductId";
                SqlCommand sqlcmd = new SqlCommand(query, sqlConn);

                sqlcmd.Parameters.AddWithValue("@ProductId", productModel.ProductId);
                sqlcmd.Parameters.AddWithValue("@Name", productModel.Name);
                sqlcmd.Parameters.AddWithValue("@Category", productModel.Category);
                sqlcmd.Parameters.AddWithValue("@Color", productModel.Color);
                sqlcmd.Parameters.AddWithValue("@UnitPrice", productModel.UnitPrice);
                sqlcmd.Parameters.AddWithValue("@AvailableQuantity", productModel.AvailableQuantity);
                sqlcmd.Parameters.AddWithValue("@CratedDate", productModel.CratedDate.Date);

                sqlcmd.ExecuteNonQuery();


            }

                return RedirectToAction("Index");
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int id)
        {


            using (SqlConnection sqlConn = new SqlConnection(ConnectionString))
            {
                sqlConn.Open();
                string query = "DELETE Products  where ProductId=@ProductId";
                SqlCommand sqlcmd = new SqlCommand(query, sqlConn);

                sqlcmd.Parameters.AddWithValue("@ProductId", id);
         

                sqlcmd.ExecuteNonQuery();


            }

            return RedirectToAction("Index");


          
        }

        // POST: Products/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}