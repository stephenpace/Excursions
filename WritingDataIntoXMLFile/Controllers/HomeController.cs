using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Web.Mvc;
using System.Xml;
using System.Data;
using WritingDataIntoXMLFile.Models;

namespace WritingDataIntoXMLFile.Controllers
{
    public class HomeController : Controller
    {
        protected int changer;
        
        //Renders Index View
        public ActionResult Index()
        {
            List<CustomerModel> customerList = new List<CustomerModel>();
            string xmlFilePath = @"XMLfile\Customerdata.xml";
            customerList = GetCustomerdata();
            CreateXmlFile(xmlFilePath,customerList);
            ViewBag.Msg = "Customer XML Datafile created successfully";
            return View();
        }
        //Creates XML file if does not exists...
        //and writes data into XML file using XmlWriter
        public void CreateXmlFile(string filepath, List<CustomerModel> xmldata)
        {
            try
            {
                using (XmlWriter xmlwriter = XmlWriter.Create(Server.MapPath(filepath)))
                {
                    xmlwriter.WriteStartDocument();
                    xmlwriter.WriteStartElement("Customer");
                    foreach (CustomerModel i in xmldata)
                    {
                        xmlwriter.WriteStartElement("Excursion");
                        xmlwriter.WriteElementString("Title", i.Title);
                        xmlwriter.WriteElementString("Area", i.Area.ToString());
                        xmlwriter.WriteElementString("Student Cost", i.StudentCost.ToString());
                        xmlwriter.WriteElementString("Transport", i.Transport);
                        xmlwriter.WriteElementString("TransportCost16", i.Transport16.ToString());
                        xmlwriter.WriteElementString("TransportCost30", i.Transport30.ToString());
                        xmlwriter.WriteElementString("TransportCost53", i.Transport53.ToString());
                        xmlwriter.WriteElementString("Website", i.Website.ToString());
                        xmlwriter.WriteElementString("Information", i.Information.ToString());
                        xmlwriter.WriteEndElement();
                    }
                    xmlwriter.WriteEndElement();
                    xmlwriter.WriteEndDocument();
                }
            }
            catch(Exception ex)
            {
                ViewBag.Msg = "Exception of type: " + ex + " occured please try again";
            }
        }
        //Customer Data source
        public List<CustomerModel> GetCustomerdata()
        {
            List<CustomerModel> customerdata = new List<CustomerModel>()
            {
                //new CustomerModel {ExcursionID=1,Title="Glendalough",Area="Wicklow",StudentCost="4",Transport="Yes",Transport16="0",Transport30="0",Transport53="0",LeaderFree="Yes",LeaderCost="0" },
            };
            return customerdata;
        }
        //renders displayXML view page
        public ActionResult DisplayXMl()
        {
            var data = new List<CustomerModel>();
            //get data from source
            data = ReturnData();
            //retrn data to view using model directive
            return View(data);
        }
        public List<CustomerModel> ReturnData()
        {
            //get file path from server
            string xmldata = Server.MapPath("~/XMLfile/Customerdata.xml");
            DataSet ds = new DataSet();
            //read the xml data from file using dataset
            ds.ReadXml(xmldata);
            //get data from dataset,convert and store it in list. 
            var customerlist = new List<CustomerModel>();
            if (changer == 0)
            {
                customerlist = (from rows in ds.Tables[0].AsEnumerable()
                                select new CustomerModel
                                {
                                    Title = rows[0].ToString(),
                                    Area = rows[1].ToString(),
                                    StudentCost = rows[2].ToString(),
                                    Transport = rows[3].ToString(),
                                    Transport16 = rows[4].ToString(),
                                    Transport30 = rows[5].ToString(),
                                    Transport53 = rows[6].ToString(),
                                    LeaderFree = rows[7].ToString(),
                                    LeaderCost = rows[8].ToString(),
                                    Website = rows[9].ToString(),
                                    Information = rows[10].ToString()
                                }).OrderBy(x => x.Title).ToList();
                return customerlist;
            }
            else if (changer == 1)
            {
                customerlist = (from rows in ds.Tables[0].AsEnumerable()
                                select new CustomerModel
                                {
                                    Title = rows[0].ToString(),
                                    Area = rows[1].ToString(),
                                    StudentCost = rows[2].ToString(),
                                    Transport = rows[3].ToString(),
                                    Transport16 = rows[4].ToString(),
                                    Transport30 = rows[5].ToString(),
                                    Transport53 = rows[6].ToString(),
                                    LeaderFree = rows[7].ToString(),
                                    LeaderCost = rows[8].ToString(),
                                    Website = rows[9].ToString(),
                                    Information = rows[10].ToString()
                                }).Where (x => x.Area == "Bray").OrderBy(x => x.Title).ToList();
                return customerlist;
            }
            else
            {
                customerlist = (from rows in ds.Tables[0].AsEnumerable()
                                select new CustomerModel
                                {
                                    Title = rows[0].ToString(),
                                    Area = rows[1].ToString(),
                                    StudentCost = rows[2].ToString(),
                                    Transport = rows[3].ToString(),
                                    Transport16 = rows[4].ToString(),
                                    Transport30 = rows[5].ToString(),
                                    Transport53 = rows[6].ToString(),
                                    LeaderFree = rows[7].ToString(),
                                    LeaderCost = rows[8].ToString(),
                                    Website = rows[9].ToString(),
                                    Information = rows[10].ToString()
                                }).Where(x => x.Area == "Dublin").OrderBy(x => x.Title).ToList();
                return customerlist;
            }
        }

        private void Click_ShowBray()
        {
            changer = 1;
            DisplayXMl();
        }
    }
}