
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using System.IO;
using System.Net;
using System.Reflection;
using System.Windows.Forms;
using System.Text;


namespace Firebase
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            
            while (true)
            {
                DateTime date = DateTime.Now;
                string Date = "PleaseWork";
                string Time = "Workdammit";

                var json = Newtonsoft.Json.JsonConvert.SerializeObject(new
                {
                    id = Time,
                    password = Date,

                });

                var request = WebRequest.CreateHttp("https://tuition-grant.firebaseio.com/");
                request.Method = "POST";
                request.ContentType = "application/json";
                var buffer = Encoding.UTF8.GetBytes(json);
                request.ContentLength = buffer.Length;
                request.GetRequestStream().Write(buffer, 0, buffer.Length);
                var response = request.GetResponse();
                json = (new StreamReader(response.GetResponseStream())).ReadToEnd();


            }

        }
    }
}
