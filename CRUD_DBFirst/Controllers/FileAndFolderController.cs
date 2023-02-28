using CRUD_DBFirst.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace CRUD_DBFirst.Controllers
{
    public class FileAndFolderController : Controller
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter sda;
        DataTable dt;
        string connectionString = ConnectionString.Constr;
        IWebHostEnvironment _webHostEnvironment;
        public FileAndFolderController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(tblFileAndFolder fileAndFolder)
        {
            using (con = new SqlConnection(connectionString))
            {
                string img = SaveImage(fileAndFolder);
                if (img != "")
                {
                    fileAndFolder.PhotoPath = img;
                }
                cmd = new SqlCommand("Insert into tblFileAndFolder(Name,Modified,ModifiedBy,[File],IsActive) " +
                    "values('" + fileAndFolder.PhotoPath + "','" + DateTime.Now + "',N'" + fileAndFolder.ModifiedBy + "','" + fileAndFolder.PhotoPath + "',1)", con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                ViewBag.msg = "Success";
            }
            return RedirectToAction("AllFileAndFolder");
        }
        public IActionResult AllFileAndFolder(int? id)
        {
            using (con = new SqlConnection(connectionString))
            {
                List<tblFileAndFolder> fileAndFolder;
                tblFileAndFolder emp;
                if (id != null)
                {
                    //cmd = new SqlCommand("Update tblFileAndFolder set IsActive=0 where Id=" + id + "", con); to update the IsActive field 0
                    cmd = new SqlCommand("Delete from tblFileAndFolder where Id=" + id + "", con);
                    cmd.CommandType = CommandType.Text;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    ViewBag.msg = "Removed";
                }

                cmd = new SqlCommand("select * from tblFileAndFolder where IsActive=1", con); //get all data where IsActive is 1
                cmd.CommandType = CommandType.Text;
                con.Open();
                fileAndFolder = new List<tblFileAndFolder>();
                using (sda = new SqlDataAdapter(cmd))
                {
                    using (dt = new DataTable())
                    {
                        sda.Fill(dt);
                        foreach (DataRow dr in dt.Rows)
                        {
                            emp = new tblFileAndFolder();
                            emp.Id = Convert.ToInt32(dr[0].ToString());
                            emp.Name = dr[1].ToString();
                            emp.Modified = dr[2].ToString();
                            emp.ModifiedBy = dr[3].ToString();
                            fileAndFolder.Add(emp);
                        }
                    }
                }
                con.Close();
                //ViewBag.fileAndFolder = dt;
                return View(fileAndFolder);
            }

        }
        public IActionResult Details(int id)
        {
            tblFileAndFolder emp = GetbyId(id);
            return View(emp);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            tblFileAndFolder emp = GetbyId(id);
            return View(emp);
        }
        [HttpPost]
        public IActionResult Edit(tblFileAndFolder fileAndFolder)
        {
            if (fileAndFolder.File != null)
            {
                string img = SaveImage(fileAndFolder);
                fileAndFolder.PhotoPath = img;
            }

            using (con = new SqlConnection(connectionString))
            {
                cmd = new SqlCommand("Update tblFileAndFolder set Name='" + fileAndFolder.PhotoPath + "',Modified='" + DateTime.Now
                    + "',ModifiedBy=N'" + fileAndFolder.ModifiedBy + "',[File]='" + fileAndFolder.PhotoPath +
                    "',IsActive=1 where Id=" + fileAndFolder.Id + "", con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                ViewBag.msg = "Success";
            }
            tblFileAndFolder emp = GetbyId(fileAndFolder.Id);
            return View(emp);

        }
        public tblFileAndFolder GetbyId(int id)
        {
            tblFileAndFolder emp;
            using (con = new SqlConnection(connectionString))
            {
                cmd = new SqlCommand("Select * from tblFileAndFolder where Id=" + id + "", con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                cmd.ExecuteReader();
                con.Close();
                using (sda = new SqlDataAdapter(cmd))
                {
                    using (dt = new DataTable())
                    {
                        sda.Fill(dt);
                        emp = new tblFileAndFolder();
                        emp.Id = Convert.ToInt32(dt.Rows[0][0].ToString());
                        emp.Name = dt.Rows[0][1].ToString();
                        emp.Modified = dt.Rows[0][2].ToString();
                        emp.ModifiedBy = dt.Rows[0][3].ToString();
                        emp.PhotoPath = dt.Rows[0][5].ToString();
                    }
                }
            }
            return emp;
        }
        public string SaveImage(tblFileAndFolder fileAndFolder)
        {
            return fileAndFolder.File.FileName.ToString();
        }
    }
}
