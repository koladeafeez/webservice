using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common;
using System.Data;
using System.Data.Common;
using System.Web;
using System.Xml;
using System.Security.Cryptography;
using System.IO;
using System.Net.Mail;
using System.Net;
using System.Configuration;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Web.Services;


/// <summary>
/// Summary description for WebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.None)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class DataService : System.Web.Services.WebService
{

    //Initialize DB
   
  //Database  db = DatabaseFactory.CreateDatabase("DBConnect");
  // Database db = new Microsoft.Practices.EnterpriseLibrary.Data.Oracle.OracleDatabase(ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString);
    Database db = new Microsoft.Practices.EnterpriseLibrary.Data.Oracle.OracleDatabase("Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=10.0.0.52)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=orcl)));User Id=CONFIRMME;Password=Chams123#");

    //Initialize classes
    Logger logger = new Logger();

    [WebMethod]
    public Boolean CreateEmployee(Int32 OrganizationId, String Surname, String Firstname, String Middlename, String EmployeeNo, DateTime DateOfEmployment, DateTime EndDate,
        String Reason, String Department, String Jobrole, Int32 Activefg)
    {

        try
        {
            Boolean output = true;

            DbCommand cmd = db.GetStoredProcCommand("ADD_EMPLOYEE");
            db.AddInParameter(cmd, "V_ORGANIZATIONID", DbType.Int32, OrganizationId);
            db.AddInParameter(cmd, "V_SURNAME", DbType.String, Surname);
            db.AddInParameter(cmd, "V_FIRSTNAME", DbType.String, Firstname);
            db.AddInParameter(cmd, "V_MIDDLENAME", DbType.String, Middlename);
            db.AddInParameter(cmd, "V_EMPLOYEENO", DbType.String, EmployeeNo);
            db.AddInParameter(cmd, "V_STARTDATE", DbType.Date, DateOfEmployment);
            db.AddInParameter(cmd, "V_ENDDATE", DbType.Date, EndDate);
            db.AddInParameter(cmd, "V_REASON", DbType.String, Reason);
            db.AddInParameter(cmd, "V_DEPARTMENT", DbType.String, Department);
            db.AddInParameter(cmd, "V_JOBROLE", DbType.String, Jobrole);
            db.AddInParameter(cmd, "V_ACTIVEFG", DbType.Int32, Activefg);
            db.ExecuteNonQuery(cmd);


            return output;
        }
        catch (Exception ex)
        {

            logger.LogError(ex.ToString());
            return false;
        }
    }

    [WebMethod]
    public Boolean UpdateEmployeeByNo(Int32 OrganizationId, String Surname, String Firstname, String Middlename, String EmployeeNo, DateTime DateOfEmployment, DateTime EndDate,
        String Reason, String Department, String Jobrole, Int32 Activefg)
    {

        try
        {
            Boolean output = true;

            DbCommand cmd = db.GetStoredProcCommand("UPDATE_EMPLOYEE_BY_NO");
            db.AddInParameter(cmd, "V_ORGANIZATIONID", DbType.Int32, OrganizationId);
            db.AddInParameter(cmd, "V_SURNAME", DbType.String, Surname);
            db.AddInParameter(cmd, "V_FIRSTNAME", DbType.String, Firstname);
            db.AddInParameter(cmd, "V_MIDDLENAME", DbType.String, Middlename);
            db.AddInParameter(cmd, "V_EMPLOYEENO", DbType.String, EmployeeNo);
            db.AddInParameter(cmd, "V_STARTDATE", DbType.Date, DateOfEmployment);
            db.AddInParameter(cmd, "V_ENDDATE", DbType.Date, EndDate);
            db.AddInParameter(cmd, "V_REASON", DbType.String, Reason);
            db.AddInParameter(cmd, "V_DEPARTMENT", DbType.String, Department);
            db.AddInParameter(cmd, "V_JOBROLE", DbType.String, Jobrole);
            db.AddInParameter(cmd, "V_ACTIVEFG", DbType.Int32, Activefg);
            db.ExecuteNonQuery(cmd);


            return output;
        }
        catch (Exception ex)
        {

            logger.LogError(ex.ToString());
            return false;
        }
    }




    [WebMethod]
    public DataSet GetCreditRegistryId(Int32 OrgId)
    {
        try
        {
            DataSet ds = db.ExecuteDataSet("GET_CREDITREGISTRY_BY_ORGID", OrgId, new Object() { });
            return ds;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.ToString());
            return null;


        }
    }

    [WebMethod]
    public DataSet GetWalletBalanceHistory(Int32 OrgId, string StartDate, string EndDate)
    {
        try
        {
            DataSet ds = db.ExecuteDataSet("GET_WALLET_BALANCE_HISTORY",StartDate,EndDate, OrgId, new Object() { });
            return ds;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.ToString());
            return null;


        }
    }


    [WebMethod]
    public Boolean CreateCreditRegistryId(Int32 OrgId, String RegistryId)
    {

        try
        {
            Boolean output = true;

            DbCommand cmd = db.GetStoredProcCommand("ADD_CREDITREGISTRY_ID");
            db.AddInParameter(cmd, "V_ORGID", DbType.Int32, OrgId);
            db.AddInParameter(cmd, "V_REGISTRYID", DbType.String, RegistryId);
            db.ExecuteNonQuery(cmd);

            return output;
        }
        catch (Exception ex)
        {

            logger.LogError(ex.ToString());
            return false;
        }
    }



    [WebMethod]
    public Boolean UpdateCardByNo(String CardNumber, Int32 Units, Int32 ActiveFg)
    {

        try
        {
            Boolean output = true;

            DbCommand cmd = db.GetStoredProcCommand("UPDATE_CARD_BY_NO");
            db.AddInParameter(cmd, "V_CARD_NUMBER", DbType.String, CardNumber);
            db.AddInParameter(cmd, "V_UNITS", DbType.String, Units);
            db.AddInParameter(cmd, "V_ACTIVEFG", DbType.Int32, ActiveFg);
            db.ExecuteNonQuery(cmd);


            return output;
        }
        catch (Exception ex)
        {

            logger.LogError(ex.ToString());
            return false;
        }
    }



    [WebMethod]
    public Boolean CreateActivityLog(String Activity, String IP, Int32 UserId)
    {

        try
        {
            Boolean output = true;

            DbCommand cmd = db.GetStoredProcCommand("ADD_ACTIVITY_LOG");
            db.AddInParameter(cmd, "V_ACTIVITY", DbType.String, Activity);
            db.AddInParameter(cmd, "V_IP", DbType.String, IP);
            db.AddInParameter(cmd, "V_USERID", DbType.Int32, UserId);
            db.ExecuteNonQuery(cmd);


            return output;
        }
        catch (Exception ex)
        {

            logger.LogError(ex.ToString());
            return false;
        }
    }

    [WebMethod]
    public Boolean CreateNQRReport(String Institution, String Parameter, String ReferenceNo, String Response, Int32 OrganizationId)
    {

        try
        {
            Boolean output = true;

            DbCommand cmd = db.GetStoredProcCommand("ADD_NQR_REPORT");
            db.AddInParameter(cmd, "V_INSTITUTION", DbType.String, Institution);
            db.AddInParameter(cmd, "V_PARAMETER", DbType.String, Parameter);
            db.AddInParameter(cmd, "V_REFERENCENO", DbType.String, ReferenceNo);
            db.AddInParameter(cmd, "V_RESPONSE", DbType.String, Response);
            db.AddInParameter(cmd, "V_ORGANIZATIONID", DbType.Int32, OrganizationId);
            db.ExecuteNonQuery(cmd);


            return output;
        }
        catch (Exception ex)
        {

            logger.LogError(ex.ToString());
            return false;
        }
    }


    [WebMethod]
    public Boolean CreateHistory(Int32 OrganizationId, Int32 SourceId, String ReferenceNo, String Response)
    {

        try
        {
            Boolean output = true;

            DbCommand cmd = db.GetStoredProcCommand("ADD_HISTORY");
            db.AddInParameter(cmd, "V_ORGANIZATIONID", DbType.Int32, OrganizationId);
            db.AddInParameter(cmd, "V_SOURCEID", DbType.Int32, SourceId);
            db.AddInParameter(cmd, "V_REFERENCENO", DbType.String, ReferenceNo);
            db.AddInParameter(cmd, "V_RESPONSE", DbType.String, Response);
            db.ExecuteNonQuery(cmd);


            return output;
        }
        catch (Exception ex)
        {

            logger.LogError(ex.ToString());
            return false;
        }
    }

    [WebMethod]
    public Boolean CreateTranscriptRequest(Int32 UserId, String MatricNo, String Institution, String RecipientName, String RecipientAddress,
        String RecipientEmail, String RecipientPhone, Int32 Status)
    {

        try
        {
            Boolean output = true;

            DbCommand cmd = db.GetStoredProcCommand("ADD_TRANSCRIPT");
            db.AddInParameter(cmd, "V_ORGANIZATIONID", DbType.Int32, UserId);
            db.AddInParameter(cmd, "V_MATRICNO", DbType.String, MatricNo);
            db.AddInParameter(cmd, "V_INSTITUTION", DbType.String, Institution);
            db.AddInParameter(cmd, "V_RECIPIENT_NAME", DbType.String, RecipientName);
            db.AddInParameter(cmd, "V_RECIPIENT_ADDRESS", DbType.String, RecipientAddress);
            db.AddInParameter(cmd, "V_RECIPIENT_EMAIL", DbType.String, RecipientEmail);
            db.AddInParameter(cmd, "V_RECIPIENT_PHONE", DbType.String, RecipientPhone);
            db.AddInParameter(cmd, "V_STATUS", DbType.Int32, Status);
            db.ExecuteNonQuery(cmd);


            return output;
        }
        catch (Exception ex)
        {

            logger.LogError(ex.ToString());
            return false;
        }
    }


    [WebMethod]
    public Boolean UpdateTranscriptRequest(Int32 RequestId, String MatricNo, String Institution, String RecipientName, String RecipientAddress,
        String RecipientEmail, String RecipientPhone, Int32 Status)
    {

        try
        {
            Boolean output = true;

            DbCommand cmd = db.GetStoredProcCommand("UPDATE_TRANSCRIPT");
            db.AddInParameter(cmd, "V_REQUESTID", DbType.Int32, RequestId);
            db.AddInParameter(cmd, "V_MATRICNO", DbType.String, MatricNo);
            db.AddInParameter(cmd, "V_INSTITUTION", DbType.String, Institution);
            db.AddInParameter(cmd, "V_RECIPIENT_NAME", DbType.String, RecipientName);
            db.AddInParameter(cmd, "V_RECIPIENT_ADDRESS", DbType.String, RecipientAddress);
            db.AddInParameter(cmd, "V_RECIPIENT_EMAIL", DbType.String, RecipientEmail);
            db.AddInParameter(cmd, "V_RECIPIENT_PHONE", DbType.String, RecipientPhone);
            db.AddInParameter(cmd, "V_STATUS", DbType.Int32, Status);
            db.ExecuteNonQuery(cmd);


            return output;
        }
        catch (Exception ex)
        {

            logger.LogError(ex.ToString());
            return false;
        }
    }

    [WebMethod]
    public Boolean UpdateTranscriptStatus(Int32 RequestId, Int32 Status)
    {

        try
        {
            Boolean output = true;

            DbCommand cmd = db.GetStoredProcCommand("UPDATE_TRANSCRIPT_STATUS");
            db.AddInParameter(cmd, "V_REQUESTID", DbType.Int32, RequestId);
            db.AddInParameter(cmd, "V_STATUS", DbType.Int32, Status);
            db.ExecuteNonQuery(cmd);


            return output;
        }
        catch (Exception ex)
        {

            logger.LogError(ex.ToString());
            return false;
        }
    }


    [WebMethod]
    public Boolean CreateCard(String Card_Number, Int32 Units, Int32 Created_By, Int32 ActiveFg)
    {

        try
        {
            Boolean output = true;

            DbCommand cmd = db.GetStoredProcCommand("ADD_CARD");
            db.AddInParameter(cmd, "V_CARDNUMBER", DbType.String, Card_Number);
            db.AddInParameter(cmd, "V_UNITS", DbType.Int32, Units);
            db.AddInParameter(cmd, "V_CREATEDBY", DbType.Int32, Created_By);
            db.AddInParameter(cmd, "V_ACTIVEFG", DbType.Int32, ActiveFg);
            db.ExecuteNonQuery(cmd);


            return output;
        }
        catch (Exception ex)
        {

            logger.LogError(ex.ToString());
            return false;
        }
    }


    [WebMethod]
    public Boolean CreateSource(String SourceName, Decimal Amount)
    {

        try
        {
            Boolean output = true;

            DbCommand cmd = db.GetStoredProcCommand("ADD_SOURCE");
            db.AddInParameter(cmd, "V_SOURCE_NAME", DbType.String, SourceName);
            db.AddInParameter(cmd, "V_AMOUNT", DbType.Decimal, Amount);
            db.ExecuteNonQuery(cmd);


            return output;
        }
        catch (Exception ex)
        {

            logger.LogError(ex.ToString());
            return false;
        }
    }



    [WebMethod]
    public Boolean CreatePayment(Int32 OrganizationId, Int32 Units, String TransactionRef, Double Amount, Int32 Status, String Description)
    {

        try
        {
            Boolean output = false;
            DbCommand cmd = db.GetStoredProcCommand("ADD_PAYMENT");
            db.AddInParameter(cmd, "V_ORGANIZATIONID", DbType.Int32, OrganizationId);
            db.AddInParameter(cmd, "V_UNITS", DbType.Int32, Units);
            db.AddInParameter(cmd, "V_TRANSACTIONREF", DbType.String, TransactionRef);
            db.AddInParameter(cmd, "V_AMOUNT", DbType.Double, Amount);
            db.AddInParameter(cmd, "V_STATUS", DbType.Int32, Status);
            db.AddInParameter(cmd, "V_DESCRIPTION", DbType.String, Description);

            db.ExecuteNonQuery(cmd);


            return true;
        }
        catch (Exception ex)
        {

            logger.LogError(ex.ToString());
            return false;
        }
    }


    [WebMethod]
    public Boolean CreateAPIProfile(Int32 ClientId, String ClientKey)
    {

        try
        {
            Boolean output = false;
            DbCommand cmd = db.GetStoredProcCommand("ADD_API_PROFILE");
            db.AddInParameter(cmd, "V_CLIENTID", DbType.Int32, ClientId);
            db.AddInParameter(cmd, "V_CLIENTKEY", DbType.String, ClientKey);

            db.ExecuteNonQuery(cmd);


            return true;
        }
        catch (Exception ex)
        {

            logger.LogError(ex.ToString());
            return false;
        }
    }

    [WebMethod]
    public Boolean UpdateAPIProfile(Int32 ClientId, String ClientKey, Int32 ActiveFg)
    {

        try
        {
            Boolean output = false;
            DbCommand cmd = db.GetStoredProcCommand("UPDATE_API_PROFILE");
            db.AddInParameter(cmd, "V_CLIENTID", DbType.Int32, ClientId);
            db.AddInParameter(cmd, "V_CLIENTKEY", DbType.String, ClientKey);
            db.AddInParameter(cmd, "V_ACTIVEFG", DbType.Int32, ActiveFg);

            db.ExecuteNonQuery(cmd);


            return true;
        }
        catch (Exception ex)
        {

            logger.LogError(ex.ToString());
            return false;
        }
    }

    [WebMethod]
    public Boolean DeleteAPIProfile(Int32 ClientId)
    {

        try
        {
            Boolean output = false;
            DbCommand cmd = db.GetStoredProcCommand("DELETE_API_PROFILE");
            db.AddInParameter(cmd, "V_CLIENTID", DbType.Int32, ClientId);

            db.ExecuteNonQuery(cmd);


            return true;
        }
        catch (Exception ex)
        {

            logger.LogError(ex.ToString());
            return false;
        }
    }

    [WebMethod]
    public DataSet GetAPIProfiles()
    {
        try
        {
            DataSet ds = db.ExecuteDataSet("GET_API_PROFILES", new Object() { });
            return ds;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.ToString());
            return null;


        }
    }

    [WebMethod]
    public DataSet GetAPIProfile(Int32 ClientId)

    {
        try
        {
            DataSet ds = db.ExecuteDataSet("GET_API_PROFILE", ClientId, new Object() { });
            return ds;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.ToString());
            return null;


        }
    }

    [WebMethod]
    public DataSet GetAPIResponseCodes()
    {
        try
        {
            DataSet ds = db.ExecuteDataSet("GET_API_RESPONSE_CODES", new Object() { });
            return ds;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.ToString());
            return null;


        }
    }


    [WebMethod]
    public string hashValue(string ConcatenatedInputString)
    {

        return BitConverter.ToString(new SHA256CryptoServiceProvider().ComputeHash(Encoding.Default.GetBytes(ConcatenatedInputString))).Replace("-", string.Empty).ToLower();
    }

    [WebMethod]
    public Boolean UpdateUnits(Int32 OrganizationId, Int32 Units)
    {

        try
        {
            Boolean output = true;

            DbCommand cmd = db.GetStoredProcCommand("UPDATE_CREDIT");
            db.AddInParameter(cmd, "V_ORGANIZATIONID", DbType.Int32, OrganizationId);
            db.AddInParameter(cmd, "V_UNITS", DbType.Int32, Units);

            db.ExecuteNonQuery(cmd);


            return output;
        }
        catch (Exception ex)
        {

            logger.LogError(ex.ToString());
            return false;
        }
    }

    [WebMethod]
    public Boolean UpdatePayment(String TxnRef, Int32 Status, String Description)
    {

        try
        {
            Boolean output = true;

            DbCommand cmd = db.GetStoredProcCommand("UPDATE_PAYMENT");
            db.AddInParameter(cmd, "V_TXNREF", DbType.String, TxnRef);
            db.AddInParameter(cmd, "V_STATUS", DbType.Int32, Status);
            db.AddInParameter(cmd, "V_DESCRIPTION", DbType.String, Description);

            db.ExecuteNonQuery(cmd);


            return output;
        }
        catch (Exception ex)
        {

            logger.LogError(ex.ToString());
            return false;
        }
    }


    [WebMethod]
    public Boolean DebitUnits(Int32 OrganizationId, Int32 Units)
    {

        try
        {
            Boolean output = true;

            DbCommand cmd = db.GetStoredProcCommand("DEBIT_CREDIT");
            db.AddInParameter(cmd, "V_ORGANIZATIONID", DbType.Int32, OrganizationId);
            db.AddInParameter(cmd, "V_UNIT", DbType.Int32, Units);

            db.ExecuteNonQuery(cmd);


            return output;
        }
        catch (Exception ex)
        {

            logger.LogError(ex.ToString());
            return false;
        }
    }


    [WebMethod]
    public Boolean BlockUser(Int32 UserId)
    {

        try
        {
            Boolean output = true;

            DbCommand cmd = db.GetStoredProcCommand("BLOCK_USER");
            db.AddInParameter(cmd, "V_USERID", DbType.Int32, UserId);

            db.ExecuteNonQuery(cmd);


            return output;
        }
        catch (Exception ex)
        {

            logger.LogError(ex.ToString());
            return false;
        }
    }

    [WebMethod]
    public Boolean BlockOrganization(Int32 OrganizationId)
    {

        try
        {
            Boolean output = true;

            DbCommand cmd = db.GetStoredProcCommand("BLOCK_ORGANIZATION");
            db.AddInParameter(cmd, "V_ORGANIZATIONID", DbType.Int32, OrganizationId);

            db.ExecuteNonQuery(cmd);


            return output;
        }
        catch (Exception ex)
        {

            logger.LogError(ex.ToString());
            return false;
        }
    }

    [WebMethod]
    public DataSet GetTranscriptStatus()
    {
        try
        {
            DataSet ds = db.ExecuteDataSet("GET_TRANSCRIPT_STATUS", new Object() { });
            return ds;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.ToString());
            return null;


        }
    }

    [WebMethod]
    public DataSet GetUserTypes()
    {
        try
        {
            DataSet ds = db.ExecuteDataSet("GET_USERTYPES", new Object() { });
            return ds;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.ToString());
            return null;


        }
    }



    [WebMethod]
    public Boolean UpdateSource(Int32 SourceId, String SourceName, Decimal Amount, Int32 ActiveFg)
    {

        try
        {
            Boolean output = true;

            DbCommand cmd = db.GetStoredProcCommand("UPDATE_SOURCE");
            db.AddInParameter(cmd, "V_SOURCEID", DbType.Int32, SourceId);
            db.AddInParameter(cmd, "V_SOURCE_NAME", DbType.String, SourceName);
            db.AddInParameter(cmd, "V_AMOUNT", DbType.Decimal, Amount);
            db.AddInParameter(cmd, "V_ACTIVEFG", DbType.Int32, ActiveFg);

            db.ExecuteNonQuery(cmd);


            return output;
        }
        catch (Exception ex)
        {

            logger.LogError(ex.ToString());
            return false;
        }
    }


    [WebMethod]
    public Boolean UpdateConvenience(Int32 ConvenienceId, Decimal Amount)
    {

        try
        {
            Boolean output = true;

            DbCommand cmd = db.GetStoredProcCommand("UPDATE_CONVENIENCE");
            db.AddInParameter(cmd, "V_CONVENIENCEID", DbType.Int32, ConvenienceId);
            db.AddInParameter(cmd, "V_AMOUNT", DbType.Decimal, Amount);

            db.ExecuteNonQuery(cmd);


            return output;
        }
        catch (Exception ex)
        {

            logger.LogError(ex.ToString());
            return false;
        }
    }



    [WebMethod]
    public Boolean UpdateCardAsUsed(String CardNo)
    {

        try
        {
            Boolean output = true;

            DbCommand cmd = db.GetStoredProcCommand("UPDATE_CARD_AS_USED");
            db.AddInParameter(cmd, "V_CARD_NUMBER", DbType.String, CardNo);

            db.ExecuteNonQuery(cmd);


            return output;
        }
        catch (Exception ex)
        {

            logger.LogError(ex.ToString());
            return false;
        }
    }



    [WebMethod]
    public Boolean CreateUnits(Int32 OrganizationId, Int32 Units)
    {

        try
        {
            Boolean output = true;

            DbCommand cmd = db.GetStoredProcCommand("ADD_CREDIT");
            db.AddInParameter(cmd, "V_ORGANIZATIONID", DbType.Int32, OrganizationId);
            db.AddInParameter(cmd, "V_UNITS", DbType.Int32, Units);

            db.ExecuteNonQuery(cmd);


            return output;
        }
        catch (Exception ex)
        {

            logger.LogError(ex.ToString());
            return false;
        }
    }


    [WebMethod]
    public Boolean UpdateUserByEmail(Int32 OrganizationId, String Name, String Email, String NewEmail, String Phone, Int32 Activefg)
    {

        try
        {
            Boolean output = true;

            DbCommand cmd = db.GetStoredProcCommand("UPDATE_USER_BY_EMAIL");
            db.AddInParameter(cmd, "V_ORGANIZATIONID", DbType.Int32, OrganizationId);
            db.AddInParameter(cmd, "V_NAME", DbType.String, Name);
            db.AddInParameter(cmd, "V_EMAIL", DbType.String, Email);
            db.AddInParameter(cmd, "V_EMAIL2", DbType.String, NewEmail);
            db.AddInParameter(cmd, "V_PHONE", DbType.String, Phone);
            db.AddInParameter(cmd, "V_ACTIVEFG", DbType.Int32, Activefg);

            db.ExecuteNonQuery(cmd);


            return output;
        }
        catch (Exception ex)
        {

            logger.LogError(ex.ToString());
            return false;
        }
    }


    [WebMethod]
    public Boolean UpdateAdminByEmail(String Name, String Email, String NewEmail, String Phone, Int32 Activefg, Int32 RoleId)
    {

        try
        {
            Boolean output = true;

            DbCommand cmd = db.GetStoredProcCommand("UPDATE_ADMIN_BY_EMAIL");
            db.AddInParameter(cmd, "V_NAME", DbType.String, Name);
            db.AddInParameter(cmd, "V_EMAIL", DbType.String, Email);
            db.AddInParameter(cmd, "V_EMAIL2", DbType.String, NewEmail);
            db.AddInParameter(cmd, "V_PHONE", DbType.String, Phone);
            db.AddInParameter(cmd, "V_ACTIVEFG", DbType.Int32, Activefg);
            db.AddInParameter(cmd, "V_ROLE", DbType.Int32, RoleId);

            db.ExecuteNonQuery(cmd);


            return output;
        }
        catch (Exception ex)
        {

            logger.LogError(ex.ToString());
            return false;
        }
    }


    [WebMethod]
    public Boolean ChangePassword(Int32 UserId, String NewPassword)
    {

        try
        {
            Boolean output = true;

            DbCommand cmd = db.GetStoredProcCommand("CHANGE_USERPASSWORD");
            db.AddInParameter(cmd, "V_USERID", DbType.Int32, UserId);
            db.AddInParameter(cmd, "V_PASSWORD", DbType.String, NewPassword);

            db.ExecuteNonQuery(cmd);


            return output;
        }
        catch (Exception ex)
        {

            logger.LogError(ex.ToString());
            return false;
        }
    }

    [WebMethod]
    public Boolean ChangeAdminPassword(Int32 UserId, String NewPassword)
    {

        try
        {
            Boolean output = true;

            DbCommand cmd = db.GetStoredProcCommand("CHANGE_ADMINPASSWORD");
            db.AddInParameter(cmd, "V_ADMINID", DbType.Int32, UserId);
            db.AddInParameter(cmd, "V_PASSWORD", DbType.String, NewPassword);

            db.ExecuteNonQuery(cmd);


            return output;
        }
        catch (Exception ex)
        {

            logger.LogError(ex.ToString());
            return false;
        }
    }



    [WebMethod]
    public Boolean UpdateUser(Int32 UserId, Int32 OrganizationId, String Name, String Email, String Phone, Int32 UserTypeId, Int32 Activefg, String DOB, String Gender, String BVN, String Address)
    {

        try
        {
            Boolean output = true;

            DbCommand cmd = db.GetStoredProcCommand("UPDATE_USER");
            db.AddInParameter(cmd, "V_USERID", DbType.Int32, UserId);
            db.AddInParameter(cmd, "V_ORGANIZATIONID", DbType.Int32, OrganizationId);
            db.AddInParameter(cmd, "V_NAME", DbType.String, Name);
            db.AddInParameter(cmd, "V_EMAIL", DbType.String, Email);
            db.AddInParameter(cmd, "V_PHONE", DbType.String, Phone);
            db.AddInParameter(cmd, "V_USERTYPEID", DbType.Int32, UserTypeId);
            db.AddInParameter(cmd, "V_ACTIVEFG", DbType.Int32, Activefg);
            db.AddInParameter(cmd, "V_DOB", DbType.String, DOB);
            db.AddInParameter(cmd, "V_GENDER", DbType.String, Gender);
            db.AddInParameter(cmd, "V_BVN", DbType.String, BVN);
            db.AddInParameter(cmd, "V_ADDRESS", DbType.String, Address);
            db.ExecuteNonQuery(cmd);


            return output;
        }
        catch (Exception ex)
        {

            logger.LogError(ex.ToString());
            return false;
        }
    }




    [WebMethod]
    public Boolean ResetPassword(String Email, String Password)
    {

        try
        {
            Boolean output = true;

            DbCommand cmd = db.GetStoredProcCommand("RESET_PASSWORD");
            db.AddInParameter(cmd, "V_EMAIL", DbType.String, Email);
            db.AddInParameter(cmd, "V_PASSWORD", DbType.String, Password);
            db.ExecuteNonQuery(cmd);


            return output;
        }
        catch (Exception ex)
        {

            logger.LogError(ex.ToString());
            return false;
        }
    }


    [WebMethod]
    public Boolean ActivateUser(Int32 OrganizationId)
    {

        try
        {
            Boolean output = true;

            DbCommand cmd = db.GetStoredProcCommand("ACTIVATE_USER");
            db.AddInParameter(cmd, "V_ORGANIZATIONID", DbType.Int32, OrganizationId);
            db.ExecuteNonQuery(cmd);


            return output;
        }
        catch (Exception ex)
        {

            logger.LogError(ex.ToString());
            return false;
        }
    }


    [WebMethod]
    public Boolean UpdateOrganization(Int32 OrganizationId, String Organization, String Address, String Website, String Email, String RCno, Int32 Activefg, String ActivatedBy, String Phone, String DOB)
    {

        try
        {
            Boolean output = true;

            DbCommand cmd = db.GetStoredProcCommand("UPDATE_ORGANIZATION");
            db.AddInParameter(cmd, "V_ORGANIZATIONID", DbType.Int32, OrganizationId);
            db.AddInParameter(cmd, "V_ORGANIZATION", DbType.String, Organization);
            db.AddInParameter(cmd, "V_ADDRESS", DbType.String, Address);
            db.AddInParameter(cmd, "V_WEBSITE", DbType.String, Website);
            db.AddInParameter(cmd, "V_EMAIL", DbType.String, Email);
            db.AddInParameter(cmd, "V_RCNO", DbType.String, RCno);
            db.AddInParameter(cmd, "V_ACTIVEFG", DbType.Int32, Activefg);
            db.AddInParameter(cmd, "V_ACTIVATEDBY", DbType.String, ActivatedBy);
            db.AddInParameter(cmd, "V_PHONE", DbType.String, Phone);
            db.AddInParameter(cmd, "V_DOB", DbType.String, DOB);

            db.ExecuteNonQuery(cmd);


            return output;
        }
        catch (Exception ex)
        {

            logger.LogError(ex.ToString());
            return false;
        }
    }



    [WebMethod]
    public Int32
        CreateOrganization(String Organization, String Address, String Website, String Email, String RCNo, Int32 ActiveFg, String Phone, String DOB)
    {

        try
        {
            Int32 output = 0;

            DbCommand cmd = db.GetStoredProcCommand("ADD_ORGANIZATION");
            db.AddInParameter(cmd, "V_ORGANIZATION", DbType.String, Organization);
            db.AddInParameter(cmd, "V_ADDRESS", DbType.String, Address);
            db.AddInParameter(cmd, "V_WEBSITE", DbType.String, Website);
            db.AddInParameter(cmd, "V_EMAIL", DbType.String, Email);
            db.AddInParameter(cmd, "V_RCNUMBER", DbType.String, RCNo);
            db.AddInParameter(cmd, "V_ACTIVEFG", DbType.Int32, ActiveFg);
            db.AddInParameter(cmd, "V_PHONE", DbType.String, Phone);
            db.AddInParameter(cmd, "V_DOB", DbType.String, DOB);
            db.AddOutParameter(cmd, "RETVAL", DbType.Int32, 0);

            db.ExecuteNonQuery(cmd);
            output = int.Parse(db.GetParameterValue(cmd, "RETVAL").ToString());

            return output;
        }
        catch (Exception ex)
        {

            logger.LogError(ex.ToString());
            return 0;
        }
    }


    [WebMethod]
    public Boolean CreateUser(Int32 OrganizationId, String Name, String Email, String Phone, String Password, Int32 ActiveFg, Int32 UserTypeId, String DOB, String Gender, String BVN, String Address)
    {

        try
        {
            Boolean output = true;

            DbCommand cmd = db.GetStoredProcCommand("ADD_USER");
            db.AddInParameter(cmd, "V_ORGANIZATIONID", DbType.Int32, OrganizationId);
            db.AddInParameter(cmd, "V_NAME", DbType.String, Name);
            db.AddInParameter(cmd, "V_EMAIL", DbType.String, Email);
            db.AddInParameter(cmd, "V_PHONE", DbType.String, Phone);
            db.AddInParameter(cmd, "V_PASSWORD", DbType.String, Password);
            db.AddInParameter(cmd, "V_ACTIVEFG", DbType.Int32, ActiveFg);
            db.AddInParameter(cmd, "V_USERTYPEID", DbType.Int32, UserTypeId);
            db.AddInParameter(cmd, "V_DOB", DbType.String, DOB);
            db.AddInParameter(cmd, "V_GENDER", DbType.String, Gender);
            db.AddInParameter(cmd, "V_BVN", DbType.String, BVN);
            db.AddInParameter(cmd, "V_ADDRESS", DbType.String, Address);

            db.ExecuteNonQuery(cmd);


            return output;
        }
        catch (Exception ex)
        {

            logger.LogError(ex.ToString());
            return false;
        }
    }

    [WebMethod]
    public Boolean CreateAdmin(String Name, String Email, String Phone, String Password, Int32 RoleId)
    {

        try
        {
            Boolean output = true;

            DbCommand cmd = db.GetStoredProcCommand("ADD_ADMIN");
            db.AddInParameter(cmd, "V_NAME", DbType.String, Name);
            db.AddInParameter(cmd, "V_EMAIL", DbType.String, Email);
            db.AddInParameter(cmd, "V_PHONE", DbType.String, Phone);
            db.AddInParameter(cmd, "V_PASSWORD", DbType.String, Password);
            db.AddInParameter(cmd, "V_ACTIVEFG", DbType.Int32, 1);
            db.AddInParameter(cmd, "V_ROLE", DbType.Int32, RoleId);

            db.ExecuteNonQuery(cmd);


            return output;
        }
        catch (Exception ex)
        {

            logger.LogError(ex.ToString());
            return false;
        }
    }


    [WebMethod]
    public Boolean DeleteEmployeeByNo(String EmployeeNo, Int32 OrganizationId)
    {

        try
        {
            Boolean output = true;

            DbCommand cmd = db.GetStoredProcCommand("DELETE_EMPLOYEE_BY_NO");
            db.AddInParameter(cmd, "V_EMPLOYEENO", DbType.String, EmployeeNo);
            db.AddInParameter(cmd, "V_ORGANIZATIONID", DbType.Int32, OrganizationId);
            db.ExecuteNonQuery(cmd);


            return output;
        }
        catch (Exception ex)
        {

            logger.LogError(ex.ToString());
            return false;
        }
    }


    [WebMethod]
    public Boolean DeleteCardByNumber(String CardNo)
    {

        try
        {
            Boolean output = true;

            DbCommand cmd = db.GetStoredProcCommand("DELETE_CARD_BY_NO");
            db.AddInParameter(cmd, "V_CARDNUMBER", DbType.String, CardNo);
            db.ExecuteNonQuery(cmd);


            return output;
        }
        catch (Exception ex)
        {

            logger.LogError(ex.ToString());
            return false;
        }
    }


    [WebMethod]
    public Boolean Rollback(Int32 OrganizationId)
    {

        try
        {
            Boolean output = true;

            DbCommand cmd = db.GetStoredProcCommand("ROLLBACK");
            db.AddInParameter(cmd, "V_ORGANIZATIONID", DbType.Int32, OrganizationId);
            db.ExecuteNonQuery(cmd);


            return output;
        }
        catch (Exception ex)
        {

            logger.LogError(ex.ToString());
            return false;
        }
    }



    [WebMethod]
    public Boolean DeleteOrganization(Int32 OrganizationId)
    {

        try
        {
            Boolean output = true;

            DbCommand cmd = db.GetStoredProcCommand("DELETE_ORGANIZATION");
            db.AddInParameter(cmd, "V_ORGANIZATIONID", DbType.Int32, OrganizationId);
            db.ExecuteNonQuery(cmd);


            return output;
        }
        catch (Exception ex)
        {

            logger.LogError(ex.ToString());
            return false;
        }
    }

    [WebMethod]
    public Boolean DeleteUser(Int32 UserId)
    {

        try
        {
            Boolean output = true;

            DbCommand cmd = db.GetStoredProcCommand("DELETE_USER");
            db.AddInParameter(cmd, "V_USERID", DbType.Int32, UserId);
            db.ExecuteNonQuery(cmd);


            return output;
        }
        catch (Exception ex)
        {

            logger.LogError(ex.ToString());
            return false;
        }
    }

    [WebMethod]
    public Boolean DeleteSource(Int32 SourceId)
    {

        try
        {
            Boolean output = true;

            DbCommand cmd = db.GetStoredProcCommand("DELETE_SOURCE");
            db.AddInParameter(cmd, "V_SOURCEID", DbType.Int32, SourceId);
            db.ExecuteNonQuery(cmd);


            return output;
        }
        catch (Exception ex)
        {

            logger.LogError(ex.ToString());
            return false;
        }
    }


    [WebMethod]
    public Boolean DeleteUserByOrganization(Int32 OrganizationId)
    {

        try
        {
            Boolean output = true;

            DbCommand cmd = db.GetStoredProcCommand("DELETE_USER_BY_ORGANIZATION");
            db.AddInParameter(cmd, "V_ORGANIZATIONID", DbType.Int32, OrganizationId);
            db.ExecuteNonQuery(cmd);


            return output;
        }
        catch (Exception ex)
        {

            logger.LogError(ex.ToString());
            return false;
        }
    }


    [WebMethod]
    public Boolean DeleteCreditByOrganization(Int32 OrganizationId)
    {

        try
        {
            Boolean output = true;

            DbCommand cmd = db.GetStoredProcCommand("DELETE_CREDITS_BY_ORG");
            db.AddInParameter(cmd, "V_USERID", DbType.Int32, OrganizationId);
            db.ExecuteNonQuery(cmd);


            return output;
        }
        catch (Exception ex)
        {

            logger.LogError(ex.ToString());
            return false;
        }
    }


    [WebMethod]
    public Boolean DeleteTranscriptRequest(Int32 RequestId)
    {

        try
        {
            Boolean output = true;

            DbCommand cmd = db.GetStoredProcCommand("DELETE_TRANSCRIPT");
            db.AddInParameter(cmd, "V_REQUESTID", DbType.Int32, RequestId);
            db.ExecuteNonQuery(cmd);


            return output;
        }
        catch (Exception ex)
        {

            logger.LogError(ex.ToString());
            return false;
        }
    }





    [WebMethod]
    public Boolean DeleteUserByEmail(String Email, Int32 OrganizationId)
    {

        try
        {
            Boolean output = true;

            DbCommand cmd = db.GetStoredProcCommand("DELETE_USER_BY_EMAIL");
            db.AddInParameter(cmd, "V_ORGANIZATIONID", DbType.Int32, OrganizationId);
            db.AddInParameter(cmd, "V_EMAIL", DbType.String, Email);

            db.ExecuteNonQuery(cmd);


            return output;
        }
        catch (Exception ex)
        {

            logger.LogError(ex.ToString());
            return false;
        }
    }


    [WebMethod]
    public Boolean DeleteAdminByEmail(String Email)
    {

        try
        {
            Boolean output = true;

            DbCommand cmd = db.GetStoredProcCommand("DELETE_ADMIN_BY_EMAIL");
            db.AddInParameter(cmd, "V_EMAIL", DbType.String, Email);

            db.ExecuteNonQuery(cmd);


            return output;
        }
        catch (Exception ex)
        {

            logger.LogError(ex.ToString());
            return false;
        }
    }


    [WebMethod]
    public DataSet GetAdminRoles()
    {
        try
        {
            DataSet ds = db.ExecuteDataSet("GET_ADMIN_ROLES", new Object() { });
            return ds;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.ToString());
            return null;


        }
    }

    [WebMethod]
    public DataSet GetAdmins()
    {
        try
        {
            DataSet ds = db.ExecuteDataSet("GET_ADMINS", new Object() { });


            return ds;

        }
        catch (Exception ex)
        {
            logger.LogError(ex.ToString());
            return null;


        }
    }


    [WebMethod]
    public DataSet GetAdminsByRoleId(Int32 RoleId)
    {
        try
        {
            DataSet ds = db.ExecuteDataSet("GET_ADMINS_BY_ROLEID", RoleId, new Object() { });


            return ds;

        }
        catch (Exception ex)
        {
            logger.LogError(ex.ToString());
            return null;


        }
    }

    [WebMethod]
    public DataSet GetCreditsTotal()
    {
        try
        {
            DataSet ds = db.ExecuteDataSet("GET_CREDITS_TOTAL", new Object() { });


            return ds;

        }
        catch (Exception ex)
        {
            logger.LogError(ex.ToString());
            return null;


        }
    }


    [WebMethod]
    public DataSet GetTranscripts()
    {
        try
        {
            DataSet ds = db.ExecuteDataSet("GET_TRANSCRIPTS", new Object() { });


            return ds;

        }
        catch (Exception ex)
        {
            logger.LogError(ex.ToString());
            return null;


        }
    }


    [WebMethod]
    public DataSet GetVarianceReport(Int32 OrgId, Int32 NoOfMonth)
    {
        try
        {
            DataSet ds = db.ExecuteDataSet("GET_VARIANCE_DATA", OrgId, NoOfMonth, new Object() { });
            return ds;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.ToString());
            return null;


        }
    }

    [WebMethod]
    public DataSet GetTranscript(Int32 RequestId)
    {
        try
        {
            DataSet ds = db.ExecuteDataSet("GET_TRANSCRIPT", RequestId, new Object() { });


            return ds;

        }
        catch (Exception ex)
        {
            logger.LogError(ex.ToString());
            return null;


        }
    }

    [WebMethod]
    public String GetTranscriptStatusById(Int32 Id)
    {
        try
        {
            DataSet ds = db.ExecuteDataSet("GET_TRANSCRIPT_STATUS_BY_ID", Id, new Object() { });


            return ds.Tables[0].Rows[0]["STATUS"].ToString();

        }
        catch (Exception ex)
        {
            logger.LogError(ex.ToString());
            return "Invalid Status Code";


        }
    }



    [WebMethod]
    public DataSet GetTranscriptsByUser(Int32 OrganizationId)
    {
        try
        {
            DataSet ds = db.ExecuteDataSet("GET_TRANSCRIPTS_BY_USERID", OrganizationId, new Object() { });


            return ds;

        }
        catch (Exception ex)
        {
            logger.LogError(ex.ToString());
            return null;


        }
    }


    [WebMethod]
    public DataSet GetTranscriptsByStatus(Int32 StatusId)
    {
        try
        {
            DataSet ds = db.ExecuteDataSet("GET_TRANSCRIPTS_BY_STATUS", StatusId, new Object() { });


            return ds;

        }
        catch (Exception ex)
        {
            logger.LogError(ex.ToString());
            return null;


        }
    }

    [WebMethod]
    public DataSet GetTranscriptsBySchool(String Institution)
    {
        try
        {
            DataSet ds = db.ExecuteDataSet("GET_TRANSCRIPTS_BY_SCHOOL", Institution, new Object() { });


            return ds;

        }
        catch (Exception ex)
        {
            logger.LogError(ex.ToString());
            return null;


        }
    }




    [WebMethod]
    public DataSet GetEmployeesByOrganization(Int32 OrgId)
    {
        try
        {
            DataSet ds = db.ExecuteDataSet("GET_EMPLOYEE_BY_ORGANIZATION", OrgId, new Object() { });


            return ds;

        }
        catch (Exception ex)
        {
            logger.LogError(ex.ToString());
            return null;


        }
    }


    [WebMethod]
    public DataSet GetOrganizationsForEmployeeVerification()
    {
        try
        {
            DataSet ds = db.ExecuteDataSet("GET_ORGANIZATIONS_FOR_EMP", new Object() { });


            return ds;

        }
        catch (Exception ex)
        {
            logger.LogError(ex.ToString());
            return null;


        }
    }
    [WebMethod]
    public DataSet GetOrganizationsIndividual()
    {
        try
        {
            DataSet ds = db.ExecuteDataSet("GET_INDIVIDUAL_ACTIVE", new Object() { });


            return ds;

        }
        catch (Exception ex)
        {
            logger.LogError(ex.ToString());
            return null;


        }
    }

    [WebMethod]
    public DataSet GetOrganizations()
    {
        try
        {
            DataSet ds = db.ExecuteDataSet("GET_ORGANIZATIONS_ACTIVE", new Object() { });


            return ds;

        }
        catch (Exception ex)
        {
            logger.LogError(ex.ToString());
            return null;


        }
    }

    [WebMethod]
    public DataSet GetSearches()
    {
        try
        {
            DataSet ds = db.ExecuteDataSet("GET_HISTORYS", new Object() { });


            return ds;

        }
        catch (Exception ex)
        {
            logger.LogError(ex.ToString());
            return null;


        }
    }

    [WebMethod]
    public DataSet GetPayments()
    {
        try
        {
            DataSet ds = db.ExecuteDataSet("GET_PAYMENTS", new Object() { });


            return ds;

        }
        catch (Exception ex)
        {
            logger.LogError(ex.ToString());
            return null;


        }
    }


    [WebMethod]
    public DataSet GetSearchesDashboard()
    {
        try
        {
            DataSet ds = db.ExecuteDataSet("GET_SEARCHES_DASHBOARD", new Object() { });


            return ds;

        }
        catch (Exception ex)
        {
            logger.LogError(ex.ToString());
            return null;


        }
    }


    [WebMethod]
    public DataSet GetPaymentsDashboard()
    {
        try
        {
            DataSet ds = db.ExecuteDataSet("GET_PAYMENTS_DASHBOARD", new Object() { });


            return ds;

        }
        catch (Exception ex)
        {
            logger.LogError(ex.ToString());
            return null;


        }
    }


    [WebMethod]
    public DataSet GetOrganizationByStatusByUserType(Int32 ActiveFg, Int32 UserTypeId)
    {
        try
        {
            DataSet ds = db.ExecuteDataSet("GET_ORG_BY_STATUS_BY_TYPE", ActiveFg, UserTypeId, new Object() { });
            return ds;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.ToString());
            return null;


        }
    }


    [WebMethod]
    public DataSet GetPaymentByTxnRef(string TxnRef)
    {
        try
        {
            DataSet ds = db.ExecuteDataSet("GET_PAYMENT_BY_TXNREF", TxnRef, new Object() { });


            return ds;

        }
        catch (Exception ex)
        {
            logger.LogError(ex.ToString());
            return null;


        }
    }


    [WebMethod]
    public DataSet GetNQRNotice(Int32 UniversityID)
    {
        try
        {
            DataSet ds = db.ExecuteDataSet("GET_NQR_NOTICE", UniversityID, new Object() { });


            return ds;

        }
        catch (Exception ex)
        {
            logger.LogError(ex.ToString());
            return null;


        }
    }

    [WebMethod]
    public DataSet GetWallet()
    {
        try
        {
            DataSet ds = db.ExecuteDataSet("GET_WALLET_BALANCE", new Object() { });


            return ds;

        }
        catch (Exception ex)
        {
            logger.LogError(ex.ToString());
            return null;


        }
    }


    [WebMethod]
    public DataSet GetOrganizationsActive()
    {
        try
        {
            DataSet ds = db.ExecuteDataSet("GET_ORGANIZATIONS_ACTIVE", new Object() { });


            return ds;

        }
        catch (Exception ex)
        {
            logger.LogError(ex.ToString());
            return null;


        }
    }




    [WebMethod]
    public DataSet GetEmployeeByEmployeeNumber(String EmployeeNumber, Int32 OrganizationId)
    {
        try
        {
            DataSet ds = db.ExecuteDataSet("GET_EMPLOYEE_BY_EMPLOYEENO", EmployeeNumber, OrganizationId, new Object() { });


            return ds;

        }
        catch (Exception ex)
        {
            logger.LogError(ex.ToString());
            return null;


        }
    }



    [WebMethod]
    public Boolean GetUserLogin(String Email, String Password)
    {
        try
        {
            DataSet ds = db.ExecuteDataSet("GET_USERLOGIN", Email, Password, new Object() { });
            if (ds.Tables[0].Rows.Count > 0)
            {

                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex.ToString());
            return false;


        }
    }


    [WebMethod]
    public Boolean IsCardActive(String CardNo)
    {
        try
        {
            DataSet ds = db.ExecuteDataSet("CHECK_CARD_ACTIVE", CardNo, new Object() { });
            if (ds.Tables[0].Rows.Count > 0)
            {

                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex.ToString());
            return false;


        }
    }

    [WebMethod]
    public Boolean IsCompanyActive(Int32 OrganizationId)
    {
        try
        {
            DataSet ds = db.ExecuteDataSet("CHECK_ORGANIZATION_ACTIVE", OrganizationId, new Object() { });
            if (ds.Tables[0].Rows.Count > 0)
            {

                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex.ToString());
            return false;


        }
    }


    [WebMethod]
    public Boolean GetAdminLogin(String Email, String Password)
    {
        try
        {
            DataSet ds = db.ExecuteDataSet("GET_ADMINLOGIN", Email, Password, new Object() { });
            if (ds.Tables[0].Rows.Count > 0)
            {

                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex.ToString());
            return false;


        }
    }

    [WebMethod]
    public DataSet GetUserLoginDetails(String Email, String Password)
    {
        try
        {
            DataSet ds = db.ExecuteDataSet("GET_USERLOGIN", Email, Password, new Object() { });
            return ds;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.ToString());
            return null;


        }
    }

    [WebMethod]
    public DataSet GetAdminLoginDetails(String Email, String Password)
    {
        try
        {
            DataSet ds = db.ExecuteDataSet("GET_ADMINLOGIN", Email, Password, new Object() { });
            return ds;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.ToString());
            return null;


        }
    }


    [WebMethod]
    public DataSet GetUserByEmail(String Email, Int32 OrganizationId)
    {
        try
        {
            DataSet ds = db.ExecuteDataSet("GET_USERS_BY_EMAIL", Email, OrganizationId, new Object() { });
            return ds;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.ToString());
            return null;


        }
    }


    [WebMethod]
    public DataSet GetAdminByEmail(String Email)
    {
        try
        {
            DataSet ds = db.ExecuteDataSet("GET_ADMINS_BY_EMAIL", Email, new Object() { });
            return ds;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.ToString());
            return null;


        }
    }


    [WebMethod]
    public DataSet GetUserByOrganization(Int32 OrganizationId)
    {
        try
        {
            DataSet ds = db.ExecuteDataSet("GET_USERS_BY_ORGANIZATION", OrganizationId, new Object() { });
            return ds;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.ToString());
            return null;


        }
    }


    [WebMethod]
    public DataSet GetPaymentsByOrganizationByDate(Int32 OrgId, DateTime StartDate, DateTime Enddate)
    {
        try
        {
            DataSet ds = db.ExecuteDataSet("GET_PAYMENTS_BY_ORG_BY_DATE", OrgId, StartDate, Enddate, new Object() { });
            return ds;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.ToString());
            return null;


        }
    }


    [WebMethod]
    public DataSet GetNQRByOrganizationByDate(Int32 OrgId, String Institution, DateTime StartDate, DateTime Enddate)
    {
        try
        {
            DataSet ds = db.ExecuteDataSet("GET_NQR_BY_DATE", OrgId, Institution, StartDate, Enddate, new Object() { });
            return ds;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.ToString());
            return null;


        }
    }



    [WebMethod]
    public DataSet GetNQRByDateByCount(DateTime StartDate, DateTime Enddate)
    {
        try
        {
            DataSet ds = db.ExecuteDataSet("GET_NQR_BY_DATE_COUNT", StartDate, Enddate, new Object() { });
            return ds;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.ToString());
            return null;


        }
    }

    [WebMethod]
    public DataSet GetNQRSchoolReport(String StartDate, String Enddate)
    {
        try
        {
            DataSet ds = db.ExecuteDataSet("GET_NQR_SCHOOL_REPORT", StartDate, Enddate, new Object() { });
            return ds;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.ToString());
            return null;


        }
    }

    #region Financial Reports
    [WebMethod]
    public DataSet GetNQRCustomerReport(String StartDate, String Enddate)
    {
        try
        {
            DataSet ds = db.ExecuteDataSet("GET_NQR_CUSTOMER_REPORT", StartDate, Enddate, new Object() { });
            return ds;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.ToString());
            return null;


        }
    }


    [WebMethod]
    public DataSet GetFinancialDetailReportAll(String StartDate, String Enddate)
    {
        try
        {
            DataSet ds = db.ExecuteDataSet("REPORT_FIN_DETAIL_ALL", StartDate, Enddate, new Object() { });
            return ds;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.ToString());
            return null;


        }
    }


    [WebMethod]
    public DataSet GetFinancialDetailReportByInstitution(Int32 SourceId, String Institution, String StartDate, String Enddate)
    {
        try
        {
            DataSet ds = db.ExecuteDataSet("REPORT_FIN_DETAIL_INST", SourceId, Institution, StartDate, Enddate, new Object() { });
            return ds;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.ToString());
            return null;


        }
    }


    [WebMethod]
    public DataSet GetFinancialDetailReportBySource(Int32 SourceId, String StartDate, String Enddate)
    {
        try
        {
            DataSet ds = db.ExecuteDataSet("REPORT_FIN_DETAIL_SRCID", SourceId, StartDate, Enddate, new Object() { });
            return ds;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.ToString());
            return null;


        }
    }


    [WebMethod]
    public DataSet GetFinancialSummaryReportAll(String StartDate, String Enddate)
    {
        try
        {
            DataSet ds = db.ExecuteDataSet("REPORT_FIN_SUMMARY_ALL", StartDate, Enddate, new Object() { });
            return ds;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.ToString());
            return null;


        }
    }


    [WebMethod]
    public DataSet GetFinancialSummaryReportByInstitution(Int32 SourceId, String Institution, String StartDate, String Enddate)
    {
        try
        {
            DataSet ds = db.ExecuteDataSet("REPORT_FIN_SUMMARY_INST", SourceId, Institution, StartDate, Enddate, new Object() { });
            return ds;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.ToString());
            return null;


        }
    }


    [WebMethod]
    public DataSet GetFinancialSummaryReportBySource(Int32 SourceId, String StartDate, String Enddate)
    {
        try
        {
            DataSet ds = db.ExecuteDataSet("REPORT_FIN_SUMMARY_SRCID", SourceId, StartDate, Enddate, new Object() { });
            return ds;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.ToString());
            return null;


        }
    }

    #endregion


    #region Transactional Reports
    [WebMethod]
    public DataSet GetTransactionalDetailReportAll(String StartDate, String Enddate)
    {
        try
        {
            DataSet ds = db.ExecuteDataSet("REPORT_TRANS_DETAIL_ALL", StartDate, Enddate, new Object() { });
            return ds;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.ToString());
            return null;


        }
    }


    [WebMethod]
    public DataSet GetTransactionalDetailReportByInstitution(Int32 SourceId, String OrganisationId, String StartDate, String Enddate)
    {
        try
        {
            DataSet ds = db.ExecuteDataSet("REPORT_TRANS_DETAIL_INST", SourceId, OrganisationId, StartDate, Enddate, new Object() { });
            return ds;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.ToString());
            return null;
        }
    }

    [WebMethod]
    public DataSet GetTransactionalDetailReportByOrganisation(Int32 SourceId, String OrganisationId, String StartDate, String Enddate)
    {
        try
        {
            DataSet ds = db.ExecuteDataSet("REPORT_TRANS_DETAIL_ORG", SourceId, OrganisationId, StartDate, Enddate, new Object() { });
            return ds;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.ToString());
            return null;
        }
    }



    [WebMethod]
    public DataSet GetTransactionalDetailReportBySource(Int32 SourceId, String StartDate, String Enddate)
    {
        try
        {
            DataSet ds = db.ExecuteDataSet("REPORT_TRANS_DETAIL_SRCID", SourceId, StartDate, Enddate, new Object() { });
            return ds;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.ToString());
            return null;


        }
    }

    [WebMethod]
    public DataSet GetTransactionalDetailReportBySourceRange(Int32 SourceId, String StartDate, String Enddate, string OrgId)
    {
        try
        {
            DataSet ds = db.ExecuteDataSet("REPORT_TRANS_DETAIL_RANGE", SourceId, StartDate, Enddate, OrgId, new Object() { });
            return ds;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.ToString());
            return null;


        }
    }


    [WebMethod]
    public DataSet GetTransactionalSummaryReportAll(String StartDate, String Enddate)
    {
        try
        {
            DataSet ds = db.ExecuteDataSet("REPORT_TRANS_SUMMARY_ALL", StartDate, Enddate, new Object() { });
            return ds;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.ToString());
            return null;


        }
    }


    [WebMethod]
    public DataSet GetTransactionalSummaryReportByInstitution(Int32 SourceId, String Institution, String StartDate, String Enddate)
    {
        try
        {
            DataSet ds = db.ExecuteDataSet("REPORT_TRANS_SUMMARY_INST", SourceId, Institution, StartDate, Enddate, new Object() { });
            return ds;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.ToString());
            return null;


        }
    }


    [WebMethod]
    public DataSet GetTransactionalSummaryReportBySource(Int32 SourceId, String StartDate, String Enddate)
    {
        try
        {
            DataSet ds = db.ExecuteDataSet("REPORT_TRANS_SUMMARY_SRCID", SourceId, StartDate, Enddate, new Object() { });
            return ds;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.ToString());
            return null;


        }
    }

    #endregion



    [WebMethod]
    public Boolean CreateReport(Int32 Source, String Institution, String Parameter, String ReferenceNo, double Rate, Int32 Unit, String Response, Int32 OrganizationId, String BatchNo, Int32 PayChannelId)
    {

        try
        {
            Boolean output = true;

            DbCommand cmd = db.GetStoredProcCommand("ADD_REPORT");
            db.AddInParameter(cmd, "V_SOURCEID", DbType.Int32, Source);
            db.AddInParameter(cmd, "V_INSTITUTION", DbType.String, Institution);
            db.AddInParameter(cmd, "V_PARAMETER", DbType.String, Parameter);
            db.AddInParameter(cmd, "V_REFERENCENO", DbType.String, ReferenceNo);
            db.AddInParameter(cmd, "V_RATE", DbType.Double, Rate);
            db.AddInParameter(cmd, "V_UNIT", DbType.Int32, Unit);
            db.AddInParameter(cmd, "V_RESPONSE", DbType.String, Response);
            db.AddInParameter(cmd, "V_ORGANIZATIONID", DbType.Int32, OrganizationId);
            db.AddInParameter(cmd, "V_BATCHNO", DbType.String, BatchNo);
            db.AddInParameter(cmd, "V_PAYCHANNELID", DbType.Int32, PayChannelId);
            db.ExecuteNonQuery(cmd);


            return output;
        }
        catch (Exception ex)
        {

            logger.LogError(ex.ToString());
            return false;
        }
    }

    [WebMethod]
    public DataSet VerifyLocal(Int32 SourceId, Int32 OrganizationId, String RefNo, String Response)
    {
        try
        {
            DataSet ds = db.ExecuteDataSet("VERIFY_LOCAL", SourceId, OrganizationId, RefNo, Response, new Object() { });
            return ds;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.ToString());
            return null;


        }
    }

    [WebMethod]
    public DataSet GetNQRChart()
    {
        try
        {
            DataSet ds = db.ExecuteDataSet("GET_NQR_CHART", new Object() { });
            return ds;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.ToString());
            return null;


        }
    }

    [WebMethod]
    public String WebPageToCode(string Url)
    {

        HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(Url);
        myRequest.Method = "GET";
        WebResponse myResponse = myRequest.GetResponse();
        StreamReader sr = new StreamReader(myResponse.GetResponseStream(), System.Text.Encoding.UTF8);
        string result = sr.ReadToEnd();
        sr.Close();
        myResponse.Close();

        return result;
    }



    [WebMethod]
    public DataSet GetActivityByDate(String StartDate, String Enddate)
    {
        try
        {
            DataSet ds = db.ExecuteDataSet("GET_ACTIVITY_BY_DATE", StartDate, Enddate, new Object() { });
            return ds;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.ToString());
            return null;


        }
    }


    [WebMethod]
    public DataSet GetHistoryByOrganizationByDate(Int32 OrgId, DateTime StartDate, DateTime Enddate)
    {
        try
        {
            DataSet ds = db.ExecuteDataSet("GET_HISTORY_BY_ORG_BY_DATE", OrgId, StartDate, Enddate, new Object() { });
            return ds;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.ToString());
            return null;


        }
    }


    [WebMethod]
    public DataSet GetCreditsByOrganization(Int32 OrgId)
    {
        try
        {
            DataSet ds = db.ExecuteDataSet("GET_CREDITS_BY_ORGANIZATION", OrgId, new Object() { });
            return ds;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.ToString());
            return null;


        }
    }



    [WebMethod]
    public DataSet VerifyEmployees(Int32 OrgId, String EmpNo, String Surname, String Firstname)
    {
        try
        {
            DataSet ds = db.ExecuteDataSet("VERIFY_EMPLOYEE", OrgId, EmpNo, Surname, Firstname, new Object() { });
            return ds;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.ToString());
            return null;


        }
    }

    [WebMethod]
    public DataSet GetSearchesByOrganization(Int32 OrgId)
    {
        try
        {
            DataSet ds = db.ExecuteDataSet("GET_HISTORY_BY_ORGANIZATION", OrgId, new Object() { });
            return ds;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.ToString());
            return null;


        }
    }


    [WebMethod]
    public DataSet GetPaymentsByOrganization(Int32 OrgId)
    {
        try
        {
            DataSet ds = db.ExecuteDataSet("GET_PAYMENTS_BY_ORGANIZATION", OrgId, new Object() { });
            return ds;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.ToString());
            return null;


        }
    }

    [WebMethod]
    public DataSet GetOrganization(Int32 OrgId)
    {
        try
        {
            DataSet ds = db.ExecuteDataSet("GET_ORGANIZATION", OrgId, new Object() { });
            return ds;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.ToString());
            return null;


        }
    }


    [WebMethod]
    public DataSet GetOrganizationByStatus(Int32 ActiveFg)
    {
        try
        {
            DataSet ds = db.ExecuteDataSet("GET_ORGANIZATIONS_BY_STATUS", ActiveFg, new Object() { });
            return ds;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.ToString());
            return null;


        }
    }


    [WebMethod]
    public DataSet GetOrganizationByEmail(String Email)
    {
        try
        {
            DataSet ds = db.ExecuteDataSet("GET_ORGANIZATION_BY_EMAIL", Email, new Object() { });
            return ds;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.ToString());
            return null;


        }
    }



    [WebMethod]
    public DataSet GetCardByNumber(String CardNo)
    {
        try
        {
            DataSet ds = db.ExecuteDataSet("GET_CARDS_BY_NO", CardNo, new Object() { });
            return ds;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.ToString());
            return null;


        }
    }


    [WebMethod]
    public DataSet GetCardsByStatus(Int32 Status)
    {
        try
        {
            DataSet ds = db.ExecuteDataSet("GET_CARDS_BY_STATUS", Status, new Object() { });
            return ds;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.ToString());
            return null;


        }
    }





    [WebMethod]
    public DataSet GetReportByOrganization(Int32 OrgId)
    {
        try
        {
            DataSet ds = db.ExecuteDataSet("GET_REPORT_ALL_BY_ORGID", OrgId, new Object() { });
            return ds;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.ToString());
            return null;


        }
    }


    [WebMethod]
    public DataSet GetReportByBatchNo(String BatchNo)
    {
        try
        {
            DataSet ds = db.ExecuteDataSet("GET_REPORT_ALL_BY_BATCHNO", BatchNo, new Object() { });
            return ds;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.ToString());
            return null;


        }
    }


    [WebMethod]
    public DataSet GetSources()
    {
        try
        {
            DataSet ds = db.ExecuteDataSet("GET_SOURCES", new Object() { });
            return ds;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.ToString());
            return null;


        }
    }




    [WebMethod]
    public void CreatePdf(string htmlText, string filename)
    {

        Document document = new Document(PageSize.A4);
        using (document)
        {
            document.SetPageSize(PageSize.A4);
            PdfWriter.GetInstance(document, new FileStream(filename, FileMode.Create));
            document.Open();
            iTextSharp.text.html.simpleparser.HTMLWorker hw =
                         new iTextSharp.text.html.simpleparser.HTMLWorker(document);
            hw.Parse(new StringReader(htmlText));
            document.Close();
            document.Dispose();
        }
    }






    [WebMethod]
    public string _hashValues(string txn_ref, string product_id, string pay_item_id, string amount, string site_redirect_url, string mac)
    {

        SHA512CryptoServiceProvider HashTool = new SHA512CryptoServiceProvider();
        string Password = string.Concat(txn_ref, product_id, pay_item_id, amount, site_redirect_url, mac);
        return BitConverter.ToString(new SHA512CryptoServiceProvider().ComputeHash(Encoding.Default.GetBytes(Password))).Replace("-", string.Empty).ToLower();
    }


    [WebMethod]
    public string hashValues(string txn_ref, string product_id, string mac)
    {

        SHA512CryptoServiceProvider HashTool = new SHA512CryptoServiceProvider();
        string Password = string.Concat(product_id, txn_ref, mac);
        return BitConverter.ToString(new SHA512CryptoServiceProvider().ComputeHash(Encoding.Default.GetBytes(Password))).Replace("-", string.Empty).ToLower();
    }





    /// <summary>
    /// Account details for Chams Collection
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public string[] GetChamsCollectionsBankDetails()
    {
        return new string[] { "0171838609", "10" };
    }


    /// <summary>
    /// Account Details for Chams Profit
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public string[] GetChamsBankDetails()
    {
        return new string[] { "0628542017", "76" };
    }


    [WebMethod]
    public DataSet GetConvenienceFee(int CONVENIENCEID)
    {
        try
        {
            DataSet ds = db.ExecuteDataSet("GET_CONVENIENCE_FEE", CONVENIENCEID, new Object() { });
            return ds;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.ToString());
            return null;


        }
    }

    [WebMethod]
    public DataSet GetSource(int SOURCEID)
    {
        try
        {
            DataSet ds = db.ExecuteDataSet("GET_SOURCE", SOURCEID, new Object() { });
            return ds;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.ToString());
            return null;


        }
    }

    [WebMethod]
    public DataSet GetCreditRegistry(int SOURCEID)
    {
        try
        {
            DataSet ds = db.ExecuteDataSet("GET_CREDITREGISTRY", SOURCEID, new Object() { });
            return ds;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.ToString());
            return null;


        }
    }


    [WebMethod]
    public DataSet GetCards()
    {
        try
        {
            DataSet ds = db.ExecuteDataSet("GET_CARDS", new Object() { });
            return ds;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.ToString());
            return null;


        }
    }


    [WebMethod]
    public DataSet GetHistoryBySource(Int32 OrganizationId, Int32 SourceId, String ReferenceNo)
    {
        try
        {
            DataSet ds = db.ExecuteDataSet("GET_HISTORY_BY_ORG_SOURCE", OrganizationId, SourceId, ReferenceNo, new Object() { });
            return ds;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.ToString());
            return null;


        }
    }




    [WebMethod]
    public DataSet GetBanks()
    {
        try
        {
            DataSet ds = db.ExecuteDataSet("GET_BANKS", new Object() { });
            return ds;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.ToString());
            return null;


        }
    }



    [WebMethod]
    public DataSet GetAccounts()
    {
        try
        {
            DataSet ds = db.ExecuteDataSet("GET_ACCOUNTS", new Object() { });
            return ds;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.ToString());
            return null;


        }
    }


    [WebMethod]
    public DataSet GetAccount(Int32 AccountID)
    {
        try
        {
            DataSet ds = db.ExecuteDataSet("GET_ACCOUNT", AccountID, new Object() { });
            return ds;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.ToString());
            return null;


        }
    }


    [WebMethod]
    public DataSet GetAccountBySourceID(Int32 SourceId)
    {
        try
        {
            DataSet ds = db.ExecuteDataSet("GET_ACCOUNT_BY_SOURCEID", SourceId, new Object() { });
            return ds;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.ToString());
            return null;


        }
    }


    [WebMethod]
    public DataSet GetAccountsNQR()
    {
        try
        {
            DataSet ds = db.ExecuteDataSet("GET_ACCOUNTS_NQR", new Object() { });
            return ds;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.ToString());
            return null;


        }
    }


    [WebMethod]
    public DataSet GetAccountNQR(Int32 AccountID)
    {
        try
        {
            DataSet ds = db.ExecuteDataSet("GET_ACCOUNT_NQR", AccountID, new Object() { });
            return ds;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.ToString());
            return null;


        }
    }


    [WebMethod]
    public DataSet GetAccountByInstitutionID(Int32 InstId)
    {
        try
        {
            DataSet ds = db.ExecuteDataSet("GET_ACCOUNT_BY_INSTITUTIONID", InstId, new Object() { });
            return ds;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.ToString());
            return null;


        }
    }



    [WebMethod]
    public DataSet GetAgents()
    {
        try
        {
            DataSet ds = db.ExecuteDataSet("GET_AGENTS", new Object() { });
            return ds;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.ToString());
            return null;


        }
    }

    [WebMethod]
    public DataSet GetAgent(Int32 AgentID)
    {
        try
        {
            DataSet ds = db.ExecuteDataSet("GET_AGENT", AgentID, new Object() { });
            return ds;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.ToString());
            return null;


        }
    }

    [WebMethod]
    public DataSet GetAgentByOrgId(Int32 OrgID)
    {
        try
        {
            DataSet ds = db.ExecuteDataSet("GET_AGENT_BY_ORGID", OrgID, new Object() { });
            return ds;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.ToString());
            return null;


        }
    }


    [WebMethod]
    public Boolean CreateAccount(String ACCOUNTNAME, String ACCOUNTNUMBER, String BANKCODE, Int32 SourceId, Double Fee)
    {

        try
        {
            Boolean output = true;

            DbCommand cmd = db.GetStoredProcCommand("ADD_ACCOUNT");
            db.AddInParameter(cmd, "V_ACCOUNTNAME", DbType.String, ACCOUNTNAME);
            db.AddInParameter(cmd, "V_ACCOUNTNUMBER", DbType.String, ACCOUNTNUMBER);
            db.AddInParameter(cmd, "V_BANKCODE", DbType.String, BANKCODE);
            db.AddInParameter(cmd, "V_SOURCEID", DbType.Int32, SourceId);
            db.AddInParameter(cmd, "V_FEE", DbType.Double, Fee);

            db.ExecuteNonQuery(cmd);


            return output;
        }
        catch (Exception ex)
        {

            logger.LogError(ex.ToString());
            return false;
        }
    }




    [WebMethod]
    public Boolean CreateSettlementReport(Int32 SourceId, Int32 PayChannelId, Int32 OrganizationId, Double Total_Fee, Double Partner_Fee, Double Chams_Fee, String Parameter, String InstitutionNQR)
    {

        try
        {
            Boolean output = true;

            DbCommand cmd = db.GetStoredProcCommand("ADD_SETTLEMENT");
            db.AddInParameter(cmd, "V_SOURCEID", DbType.Int32, SourceId);
            db.AddInParameter(cmd, "V_PAYCHANNELID", DbType.Int32, PayChannelId);
            db.AddInParameter(cmd, "V_ORGANIZATIONID", DbType.Int32, OrganizationId);
            db.AddInParameter(cmd, "V_TOTAL_FEE", DbType.Double, Total_Fee);
            db.AddInParameter(cmd, "V_PARTNER_FEE", DbType.Double, Partner_Fee);
            db.AddInParameter(cmd, "V_CHAMS_FEE", DbType.Double, Chams_Fee);
            db.AddInParameter(cmd, "V_PARAMETER", DbType.String, Parameter);
            db.AddInParameter(cmd, "V_INSTITUTION", DbType.String, InstitutionNQR);

            db.ExecuteNonQuery(cmd);


            return output;
        }
        catch (Exception ex)
        {

            logger.LogError(ex.ToString());
            return false;
        }
    }


    [WebMethod]
    public DataSet GetSettlementReport(String SourceID, String PayChannelId, String StartDate, String EndDate)
    {
        try
        {
            DataSet ds = db.ExecuteDataSet("GET_SETTLEMENT", SourceID, PayChannelId, StartDate, EndDate, new Object() { });
            return ds;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.ToString());
            return null;


        }
    }


    [WebMethod]
    public DataSet GetSettlementReportBySource(String SourceID, String PayChannelId, String StartDate, String EndDate)
    {
        try
        {
            DataSet ds = db.ExecuteDataSet("GET_SETTLEMENT_BY_SOURCE", SourceID, PayChannelId, StartDate, EndDate, new Object() { });
            return ds;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.ToString());
            return null;


        }
    }


    [WebMethod]
    public Boolean CreateAccountNQR(String ACCOUNTNUMBER, String BANKCODE, String School, Int32 InstId)
    {

        try
        {
            Boolean output = true;

            DbCommand cmd = db.GetStoredProcCommand("ADD_ACCOUNT_NQR");
            db.AddInParameter(cmd, "V_ACCOUNTNUMBER", DbType.String, ACCOUNTNUMBER);
            db.AddInParameter(cmd, "V_BANKCODE", DbType.String, BANKCODE);
            db.AddInParameter(cmd, "V_INSTITUTION", DbType.String, School);
            db.AddInParameter(cmd, "V_INSTITUTIONID", DbType.Int32, InstId);

            db.ExecuteNonQuery(cmd);


            return output;
        }
        catch (Exception ex)
        {

            logger.LogError(ex.ToString());
            return false;
        }
    }


    [WebMethod]
    public Boolean CreateAgent(String EMAILID, String PASSWORD, String SUBSCRIBERID, Int32 ORGID)
    {

        try
        {
            Boolean output = true;

            DbCommand cmd = db.GetStoredProcCommand("ADD_AGENT");
            db.AddInParameter(cmd, "V_EMAILID", DbType.String, EMAILID);
            db.AddInParameter(cmd, "V_PASSWORD", DbType.String, PASSWORD);
            db.AddInParameter(cmd, "V_SUBSCRIBERID", DbType.String, SUBSCRIBERID);
            db.AddInParameter(cmd, "V_ORGID", DbType.Int32, ORGID);
            db.ExecuteNonQuery(cmd);


            return output;
        }
        catch (Exception ex)
        {

            logger.LogError(ex.ToString());
            return false;
        }
    }


    [WebMethod]
    public Boolean UpdateAccount(Int32 ACCOUNTID, String ACCOUNTNAME, String ACCOUNTNUMBER, String BANKCODE, Int32 ActiveFG, Double Fee)
    {

        try
        {
            Boolean output = true;

            DbCommand cmd = db.GetStoredProcCommand("UPDATE_ACCOUNT");
            db.AddInParameter(cmd, "V_ACCOUNTID", DbType.Int32, ACCOUNTID);
            db.AddInParameter(cmd, "V_ACCOUNTNAME", DbType.String, ACCOUNTNAME);
            db.AddInParameter(cmd, "V_ACCOUNTNUMBER", DbType.String, ACCOUNTNUMBER);
            db.AddInParameter(cmd, "V_BANKCODE", DbType.String, BANKCODE);
            db.AddInParameter(cmd, "V_ACTIVEFG", DbType.Int32, ActiveFG);
            db.AddInParameter(cmd, "V_FEE", DbType.Double, Fee);
            db.ExecuteNonQuery(cmd);


            return output;
        }
        catch (Exception ex)
        {

            logger.LogError(ex.ToString());
            return false;
        }
    }


    [WebMethod]
    public Boolean UpdateAccountNQR(Int32 ACCOUNTID, String ACCOUNTNUMBER, String BANKCODE, Int32 ActiveFG)
    {

        try
        {
            Boolean output = true;

            DbCommand cmd = db.GetStoredProcCommand("UPDATE_ACCOUNT_NQR");
            db.AddInParameter(cmd, "V_ACCOUNTID", DbType.Int32, ACCOUNTID);
            db.AddInParameter(cmd, "V_ACCOUNTNUMBER", DbType.String, ACCOUNTNUMBER);
            db.AddInParameter(cmd, "V_BANKCODE", DbType.String, BANKCODE);
            db.AddInParameter(cmd, "V_ACTIVEFG", DbType.Int32, ActiveFG);
            db.ExecuteNonQuery(cmd);


            return output;
        }
        catch (Exception ex)
        {

            logger.LogError(ex.ToString());
            return false;
        }
    }


    [WebMethod]
    public Boolean DeleteAccount(Int32 ACCOUNTID)
    {

        try
        {
            Boolean output = true;

            DbCommand cmd = db.GetStoredProcCommand("DELETE_ACCOUNT");
            db.AddInParameter(cmd, "V_ACCOUNTID", DbType.Int32, ACCOUNTID);
            db.ExecuteNonQuery(cmd);


            return output;
        }
        catch (Exception ex)
        {

            logger.LogError(ex.ToString());
            return false;
        }
    }


    [WebMethod]
    public Boolean DeleteAccountNQR(Int32 ACCOUNTID)
    {

        try
        {
            Boolean output = true;

            DbCommand cmd = db.GetStoredProcCommand("DELETE_ACCOUNT_NQR");
            db.AddInParameter(cmd, "V_ACCOUNTID", DbType.Int32, ACCOUNTID);
            db.ExecuteNonQuery(cmd);


            return output;
        }
        catch (Exception ex)
        {

            logger.LogError(ex.ToString());
            return false;
        }
    }


    [WebMethod]
    public Boolean UpdateAgent(Int32 AGENTID, String EMAILID, String PASSWORD, String SUBSCRIBERID, Int32 ORGID)
    {

        try
        {
            Boolean output = true;

            DbCommand cmd = db.GetStoredProcCommand("UPDATE_AGENT");
            db.AddInParameter(cmd, "V_AGENTID", DbType.Int32, AGENTID);
            db.AddInParameter(cmd, "V_EMAILID", DbType.String, EMAILID);
            db.AddInParameter(cmd, "V_PASSWORD", DbType.String, PASSWORD);
            db.AddInParameter(cmd, "V_SUBSCRIBERID", DbType.String, SUBSCRIBERID);
            db.ExecuteNonQuery(cmd);


            return output;
        }
        catch (Exception ex)
        {

            logger.LogError(ex.ToString());
            return false;
        }
    }


    [WebMethod]
    public Boolean DeleteAgent(Int32 AGENTID)
    {

        try
        {
            Boolean output = true;

            DbCommand cmd = db.GetStoredProcCommand("DELETE_AGENT");
            db.AddInParameter(cmd, "V_AGENTID", DbType.Int32, AGENTID);

            db.ExecuteNonQuery(cmd);


            return output;
        }
        catch (Exception ex)
        {

            logger.LogError(ex.ToString());
            return false;
        }
    }



    [WebMethod]
    public Boolean sendmail(string to, string subject, string body)
    {
        MailMessage objeto_mail = new MailMessage();
        SmtpClient client = new SmtpClient();


        //  client.Host = "mail3.chams.com";
        // client.Port = 1025;


        client.Host = "smtp.chams.com";
        client.Port = 2525;

        System.Net.NetworkCredential nc = new System.Net.NetworkCredential();
        //nc.UserName = "elggldap@chams.com";
        //  nc.Password = "admin*01";

        nc.UserName = "services@chams.com";
        nc.Password = "welcome12@";

        client.Credentials = nc;

        objeto_mail.From = new MailAddress("confirmme@chams.com", "ConfirmMe");


        // to = "uokpagu@chams.com,uch_polyphemus@yahoo.com";

        if (to.Contains(","))
        {

            foreach (string add in to.Split(','))
            {
                objeto_mail.To.Add(new MailAddress(add));
            }

        }
        else
        {
            objeto_mail.To.Add(new MailAddress(to));

        }
        objeto_mail.Subject = subject;
        objeto_mail.Body = body;
        objeto_mail.IsBodyHtml = true;
        client.Send(objeto_mail);
        return true;
    }

    [WebMethod(MessageName = "sendmailWithAttachment")]

    public Boolean sendmail(string to, string subject, string body, string Path)
    {
        MailMessage objeto_mail = new MailMessage();
        SmtpClient client = new SmtpClient();

        //  client.Host = "mail3.chams.com";
        // client.Port = 1025;

        client.Host = "smtp.chams.com";
        client.Port = 2525;

        System.Net.NetworkCredential nc = new System.Net.NetworkCredential();
        //nc.UserName = "elggldap@chams.com";
        //  nc.Password = "admin*01";

        nc.UserName = "services@chams.com";
        nc.Password = "welcome12@";

        client.Credentials = nc;

        objeto_mail.From = new MailAddress("confirmme@chams.com", "ConfirmMe");


        Attachment att = new Attachment(Path);
        objeto_mail.Attachments.Add(att);

        // to = "uokpagu@chams.com,uch_polyphemus@yahoo.com";

        if (to.Contains(","))
        {

            foreach (string add in to.Split(','))
            {
                objeto_mail.To.Add(new MailAddress(add));
            }

        }
        else
        {
            objeto_mail.To.Add(new MailAddress(to));

        }
        objeto_mail.Subject = subject;
        objeto_mail.Body = body;
        objeto_mail.IsBodyHtml = true;
        client.Send(objeto_mail);
        return true;
    }


    [WebMethod]
    public Boolean SendSMS(String to, String body, String SenderId)
    {

        WebClient wc = new WebClient();
        string resp;
        using (wc)
        {
            resp = wc.DownloadString("http://www.mytxtporta.com/API/api.php?username=uchenext&password=uchex2q&recipient=" + to + "&sender=" + SenderId + "&message=" + body);

        }

        if (resp == "0:Accepted for Delivery")
        {

            return true;
        }
        else
        {

            return false;

        }

    }





    [WebMethod]
    public string TripleDESEncode(string value, string key)
    {
        TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();
        des.IV = new byte[8];
        PasswordDeriveBytes pdb = new PasswordDeriveBytes(key, new byte[-1 + 1]);
        des.Key = pdb.CryptDeriveKey("RC2", "MD5", 128, new byte[8]);
        MemoryStream ms = new MemoryStream((value.Length * 2) - 1);
        CryptoStream encStream = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
        byte[] plainBytes = Encoding.UTF8.GetBytes(value);
        encStream.Write(plainBytes, 0, plainBytes.Length);
        encStream.FlushFinalBlock();
        byte[] encryptedBytes = new byte[Convert.ToInt32(ms.Length - 1) + 1];
        ms.Position = 0;
        ms.Read(encryptedBytes, 0, Convert.ToInt32(ms.Length));
        encStream.Close();
        return Convert.ToBase64String(encryptedBytes);
    }

    [WebMethod]
    public string TripleDESDecode(string value, string key)
    {
        TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();
        des.IV = new byte[8];
        PasswordDeriveBytes pdb = new PasswordDeriveBytes(key, new byte[-1 + 1]);
        des.Key = pdb.CryptDeriveKey("RC2", "MD5", 128, new byte[8]);
        byte[] encryptedBytes = Convert.FromBase64String(value);
        MemoryStream ms = new MemoryStream(value.Length);
        CryptoStream decStream = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
        decStream.Write(encryptedBytes, 0, encryptedBytes.Length);
        decStream.FlushFinalBlock();
        byte[] plainBytes = new byte[Convert.ToInt32(ms.Length - 1) + 1];
        ms.Position = 0;
        ms.Read(plainBytes, 0, Convert.ToInt32(ms.Length));
        decStream.Close();
        return Encoding.UTF8.GetString(plainBytes);
    }


    [WebMethod]
    public string EncryptText(string PlainText)
    {

        return TripleDESEncode(PlainText, "chams123#");
    }

    [WebMethod]
    public string DecryptText(string CipherText)
    {

        return TripleDESDecode(CipherText, "chams123#");
    }


    [WebMethod]
    public Boolean CreateForgotPwd(string Guid, string Email)
    {

        try
        {
            Boolean output = true;
            DbCommand cmd = db.GetStoredProcCommand("ADD_FORGOTPASSWORD");
            db.AddInParameter(cmd, "p_GeneratedGuid", DbType.String, Guid);
            db.AddInParameter(cmd, "p_Email", DbType.String, Email);
           
            db.ExecuteNonQuery(cmd);


            return output;
        }
        catch (Exception ex)
        {

            logger.LogError(ex.ToString());
            return false;
        }
    }

    [WebMethod]
    public DataSet ValidateLink(string Guid)
    {
        try
        {
            DataSet ds = db.ExecuteDataSet("GET_FORGOTPASSWORD", Guid, new Object() { });
            return ds;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.ToString());
            return null;


        }
    }

    #region Address_verification 
    [WebMethod]
    public Boolean SaveAddressVerification(string CandidateId, string Name, string Status, string Reason, string OrgId, string Address, string Description)
    {
        try
        {
            Boolean output = true;
            DbCommand cmd = db.GetStoredProcCommand("ADD_ADDRESS_VERIFICATION");
            db.AddInParameter(cmd, "p_CandidateId", DbType.String,  CandidateId);
            db.AddInParameter(cmd, "p_Name", DbType.String, Name);
            db.AddInParameter(cmd, "p_Status", DbType.String, Status );
            db.AddInParameter(cmd, "p_Reason", DbType.String, Reason);
            db.AddInParameter(cmd, "p_OrgId", DbType.String, OrgId);
            db.AddInParameter(cmd, "p_Address", DbType.String, Address);
            db.AddInParameter(cmd, "p_Description", DbType.String, Description);
            db.ExecuteNonQuery(cmd);
            return output;
        }
        catch (Exception ex)
        {

            logger.LogError(ex.ToString());
            return false;
        }
    }

    [WebMethod]
    public DataSet GetVerificationDetails(string Status, string OrgId)
    {
        try
        {
            DataSet ds = db.ExecuteDataSet("GET_ADDRESS_VERIFICATION", Status, OrgId, new Object() { });
            return ds;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.ToString());
            return null;


        }
    }

    [WebMethod]
    public DataSet GetOrganisationIdByCandidateId(string CandidateId)
    {
        try
        {
            DataSet ds = db.ExecuteDataSet("GET_ORAGNIZATIONID_ADDRESS", CandidateId, new Object() { });
            return ds;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.ToString());
            return null;
        }
    }


    [WebMethod]
    public Boolean AddressVerificationCallback(string Status, string Reason, string CandidateId, string ReporableId)
    {

        try
        {
            Boolean output = true;

            DbCommand cmd = db.GetStoredProcCommand("UPDATE_ADDRESS_VERIFICATION");
            db.AddInParameter(cmd, "p_Status", DbType.String, Status);
            db.AddInParameter(cmd, "p_Reason", DbType.String, Reason);
            db.AddInParameter(cmd, "p_CandidateId", DbType.String, CandidateId);
            db.AddInParameter(cmd, "p_ReporableId", DbType.String, ReporableId);
 
            db.ExecuteNonQuery(cmd);

            return output;
        }
        catch (Exception ex)
        {

            logger.LogError(ex.ToString());
            return false;
        }
    }



     [WebMethod]
    public DataSet GetAddressVerificationAll(string OrganizationId)
    {
        try
        {
            DataSet ds = db.ExecuteDataSet("GET_ADDRESS_VER_ALL", OrganizationId, new Object() { });
            return ds;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.ToString());
            return null;
        }
    }
   

    #endregion

    #region Audit Logging 

     [WebMethod]
     public Boolean SaveAuditLog(string UserId, string OrgId, string Name, string Page, string IP, string Country, string City, string Latitude, string Longitude, string RegionName, string Timezone, string Source)
     {
         try
         {
             Boolean output = true;
             DbCommand cmd = db.GetStoredProcCommand("ADD_AUDIT_LOG");
             db.AddInParameter(cmd, "p_UserId", DbType.String, UserId);
             db.AddInParameter(cmd, "p_OrgId", DbType.String, OrgId);
             db.AddInParameter(cmd, "p_Name", DbType.String, Name);
             db.AddInParameter(cmd, "p_Page", DbType.String, Page);
             db.AddInParameter(cmd, "p_IP", DbType.String, IP);
             db.AddInParameter(cmd, "p_Country", DbType.String, Country);
             db.AddInParameter(cmd, "p_City", DbType.String, City);
             db.AddInParameter(cmd, "p_Latitude", DbType.String, Latitude);
             db.AddInParameter(cmd, "p_Longitude", DbType.String, Longitude);
             db.AddInParameter(cmd, "p_RegionName", DbType.String, RegionName);
             db.AddInParameter(cmd, "p_TimeZone", DbType.String, Timezone);
	     db.AddInParameter(cmd, "p_Source", DbType.String, Source);
             db.ExecuteNonQuery(cmd);
             return output;
         }
         catch (Exception ex)
         {

             logger.LogError(ex.ToString());
             return false;
         }
     }

     [WebMethod]
     public DataSet GetAuditLogByOrgId(string OrganizationId)
     {
         try
         {
             DataSet ds = db.ExecuteDataSet("GET_AUDIT_LOG_BYORGID", OrganizationId, new Object() { });
             return ds;
         }
         catch (Exception ex)
         {
             logger.LogError(ex.ToString());
             return null;
         }
     }

     [WebMethod]
     public DataSet GetAuditLogs()
     {
         try
         {
             DataSet ds = db.ExecuteDataSet("GET_AUDIT_LOG", new Object() { });
             return ds;
         }
         catch (Exception ex)
         {
             logger.LogError(ex.ToString());
             return null;
         }
     }
    #endregion

    #region Report_Log
     [WebMethod]
     public DataSet GetVisits(string rptsrc)
     {
         try
         {
             DataSet ds = db.ExecuteDataSet("GET_CUSTOMER_WEB_VISITS", rptsrc, new Object() { });
             return ds;
         }
         catch (Exception ex)
         {
             logger.LogError(ex.ToString());
             return null;
         }
     }
    #endregion

    #region NEGOTIATE_FEE

     [WebMethod]
     public Boolean SaveNegotiatedFee(string OrganisationId, int NegotiatedFee, int SourceId, int Status)
     {
         try
         {
             Boolean output = true;
             DbCommand cmd = db.GetStoredProcCommand("ADD_NEGOTIATED_FEE");
             db.AddInParameter(cmd, "p_OrganisationID", DbType.String, OrganisationId);
             db.AddInParameter(cmd, "p_NegotiatedFee", DbType.Int32, NegotiatedFee);
             db.AddInParameter(cmd, "p_SourceId", DbType.Int32, SourceId);
             db.AddInParameter(cmd, "p_Status", DbType.Int32, Status);
             db.ExecuteNonQuery(cmd);
             return output;
         }
         catch (Exception ex)
         {

             logger.LogError(ex.ToString());
             return false;
         }
     }

     [WebMethod]
     public Boolean UpdateNegotiatedFee(string OrganisationId, int NegotiatedFee, int SourceId, int Status)
     {
         try
         {
             Boolean output = true;
             DbCommand cmd = db.GetStoredProcCommand("UPDATE_NEGOTIATED_FEE");
             db.AddInParameter(cmd, "p_OrganisationID", DbType.String, OrganisationId);
             db.AddInParameter(cmd, "p_NegotiatedFee", DbType.Int32, NegotiatedFee);
             db.AddInParameter(cmd, "p_SourceId", DbType.Int32, SourceId);
             db.AddInParameter(cmd, "p_Status", DbType.Int32, Status);
             db.ExecuteNonQuery(cmd);
             return output;
         }
         catch (Exception ex)
         {

             logger.LogError(ex.ToString());
             return false;
         }
     }


     [WebMethod]
     public DataSet GetNegotiationFee(int SourceId, string OrganisationId)
     {
         try
         {
             DataSet ds = db.ExecuteDataSet("GET_NEGOTIATED_FEE", OrganisationId, SourceId, new Object() { });
             return ds;
         }
         catch (Exception ex)
         {
             logger.LogError(ex.ToString());
             return null;
         }
     }

     [WebMethod]
     public DataSet GetNegotiationFeeAll()
     {
         try
         {
             DataSet ds = db.ExecuteDataSet("GET_NEGOTIATED_FEE_ALL", new Object() { });
             return ds;
         }
         catch (Exception ex)
         {
             logger.LogError(ex.ToString());
             return null;
         }
     }

     #endregion

    #region WIDGET SETUP

     [WebMethod]
     public Boolean SaveWidgetServices(string OrganisationId, string RegisteredServices)
     {
         try
         {
             Boolean output = true;
             DbCommand cmd = db.GetStoredProcCommand("ADD_WIDGET_SERVICES");
             db.AddInParameter(cmd, "p_OrgId", DbType.String, OrganisationId);
             db.AddInParameter(cmd, "p_RegisteredServices", DbType.String, RegisteredServices);
             db.ExecuteNonQuery(cmd);
             return output;
         }
         catch (Exception ex)
         {

             logger.LogError(ex.ToString());
             return false;
         }
     }

     [WebMethod]
     public Boolean UpdateWidgetService(string OrganisationId, string RegisteredServices)
     {
         try
         {
             Boolean output = true;
             DbCommand cmd = db.GetStoredProcCommand("UPDATE_WIDGET_SETUP");
             db.AddInParameter(cmd, "p_OrganisationID", DbType.String, OrganisationId);
             db.AddInParameter(cmd, "p_SelectedServices", DbType.String, RegisteredServices);

             db.ExecuteNonQuery(cmd);
             return output;
         }
         catch (Exception ex)
         {

             logger.LogError(ex.ToString());
             return false;
         }
     }


     [WebMethod]
     public DataSet GetWidgetService(String OrganisationId)
     {
         try
         {
             DataSet ds = db.ExecuteDataSet("GET_WIDGET_SERVICES_BYORGID", OrganisationId, new Object() { });
             return ds;
         }
         catch (Exception ex)
         {
             logger.LogError(ex.ToString() + ex.InnerException);
             return null;
         }
     }


     [WebMethod]
     public Boolean DeleteWidget(String OrganisationId)
     {

         try
         {
             Boolean output = false;
             DbCommand cmd = db.GetStoredProcCommand("DELETE_WIDGET_SETUP");
             db.AddInParameter(cmd, "p_OrganisationId", DbType.String, OrganisationId);

             db.ExecuteNonQuery(cmd);


             return true;
         }
         catch (Exception ex)
         {

             logger.LogError(ex.ToString());
             return false;
         }
     }
     #endregion

     [WebMethod]
     public Boolean ValidateID(String FirstName, String MiddleName, String LastName, String IdNumber, String IdType, String Country, String PhoneNumber, String DOB)
     {

         try
         {
             // Int32 output = 0;
             Boolean output = true;

             DbCommand cmd = db.GetStoredProcCommand("VALIDATE_ID");
             db.AddInParameter(cmd, "V_FIRSTNAME", DbType.String, FirstName);
             db.AddInParameter(cmd, "V_MIDDLENAME", DbType.String, MiddleName);
             db.AddInParameter(cmd, "V_LASTNAME", DbType.String, LastName);
             db.AddInParameter(cmd, "V_IDNUMBER", DbType.String, IdNumber);
             db.AddInParameter(cmd, "V_IDTYPE", DbType.String, IdType);
             db.AddInParameter(cmd, "V_PHONENUMBER", DbType.String, PhoneNumber);
             db.AddInParameter(cmd, "V_COUNTRY", DbType.String, Country);
             db.AddInParameter(cmd, "V_DOB", DbType.String, DOB);

             db.ExecuteNonQuery(cmd);
             //  output = int.Parse(db.GetParameterValue(cmd, "RETVAL").ToString());

             return output;
         }
         catch (Exception ex)
         {

             logger.LogError(ex.ToString());
             return false;
         }
     }
}
