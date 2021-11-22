using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;


public class Logger
{


    public Boolean LogError(string Exception_String)
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine(DateTime.Now.ToString("dd-MMM-yyyy hh:mm tt"));
        sb.AppendLine(Exception_String);
        sb.AppendLine(Environment.NewLine);
        sb.AppendLine(Environment.NewLine);

        File.AppendAllText(ConfigurationManager.AppSettings["errors"].ToString() + "ws_" + DateTime.Now.ToString("dd-MMM-yyyy") + ".txt", sb.ToString());
        return true;


    }


    public Boolean LogToken(string token, string mode, string path)
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine(DateTime.Now.ToString("dd-MMM-yyyy hh:mm tt"));
        sb.AppendLine(token + " - " + mode);
        sb.AppendLine(Environment.NewLine);
        sb.AppendLine(Environment.NewLine);

        File.AppendAllText(path, sb.ToString());
        return true;


    }

    public string ShowSuccess(string Message)
    {
        return "<div class='alert alert-success alert-dismissable'> <button type='button' class='close' data-dismiss='alert' aria-hidden='true'>×</button>" +
                                                 Message + " </div>";

    }


    public string ShowError(string Message)
    {

        return "<div class='alert alert-danger alert-dismissable'> <button type='button' class='close' data-dismiss='alert' aria-hidden='true'>×</button>" +
                                                 Message + " </div>";

    }


    public string ShowErrorWithoutClose(string Message)
    {

        return "<div class='alert alert-danger alert-dismissable'> <button type='button' class='close' data-dismiss='alert' aria-hidden='true'>×</button>" +
                                                "<h4><i class='fa fa-times-circle'></i> Response: </h4><strong>" + Message + " </strong></div>";

    }

    public string ShowSuccessWithoutClose(string Message)
    {
        return "<div class='alert alert-success alert-dismissable'> <button type='button' class='close' data-dismiss='alert' aria-hidden='true'>×</button>" +
                                                "<h4><i class='fa fa-check-circle'></i> Response: </h4><strong>" + Message + "</strong> </div>";

    }



    public string ShowInfo(string Message)
    {

        return "<div class='alert alert-info alert-dismissable'> <button type='button' class='close' data-dismiss='alert' aria-hidden='true'>×</button>" +
                                                "<h4><i class='fa fa-info-circle'></i> Information</h4>" + Message + " </div>";

    }


}
