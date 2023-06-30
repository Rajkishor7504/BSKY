<%--'******************************************************************************************************
' File Name         :   DownLoad.ashx
' Created By        :   Arundhati Rana
'Created on         :   17-Feb-2021
' Description       :   To Download and view the documents
' Modification History:
'   <CR no.>         <Date>            <Modified by>              <Modification Summary>'                               
' *****************************************************************************************************'--%>
<%@ WebHandler Language="C#" Class="DownLoad" %>

using System;
using System.Web;
using System.Configuration;

public class DownLoad : IHttpHandler,System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest (HttpContext context)
    {
        string QueryString = "";
        //string pthSkill = ConfigurationManager.AppSettings["FileServerURL"].ToString();
        //string Doc = context.Request.QueryString["Doc"];
        //string FilePath = context.Request.QueryString["FilePath"];
        //string Fullfilepath = pthSkill + FilePath + Doc;
        //byte[] byteArray = System.IO.File.ReadAllBytes(Fullfilepath);


        //try//-------------For Download
        //{
        //context.Response.Clear();
        //context.Response.ContentType = "application/x-please-download-me"; // "application/x-unknown"
        //context.Response.AppendHeader("Content-Disposition", "attachment; filename=" + Doc);
        //context.Response.BinaryWrite(byteArray);
        //context.Response.Flush();
        //context.Response.Close();
        //byteArray = null;
        //}
        //catch(Exception ex)
        //{
        //    clsErrorHandler.WriteError(ex);
        //}

        string ServerPath = ConfigurationManager.AppSettings["PortalURL"].ToString();
        //string Doc = context.Request.QueryString["Doc"];
        //string FilePath = context.Request.QueryString["FilePath"];
        //string fullfilepath = ServerPath + FilePath + Doc;
        if (context.Request.QueryString["msg"] != null)
        {                
            QueryString = System.Web.HttpUtility.HtmlDecode(Convert.ToString(context.Request.QueryString["msg"]));
            string ext = System.IO.Path.GetExtension(QueryString);
            try
            {

                context.Response.Clear();
                if (ext == ".pdf" || ext==".PDF")
                {
                    context.Response.ContentType = "application/pdf";
                }
                if (ext == ".jpg" || ext==".JPG")
                {
                    context.Response.ContentType = "image/jpg";
                }
                if (ext == ".jpeg" || ext==".JPEG")
                {
                    context.Response.ContentType = "image/jpeg";
                }
                if (ext == ".png" || ext==".PNG")
                {
                    context.Response.ContentType = "image/png";
                }
                //context.Response.ContentType = "image/png,application/pdf,image/jpeg";
                context.Response.BinaryWrite(System.IO.File.ReadAllBytes(ServerPath + QueryString));
                context.Response.AddHeader("Content-Length", new System.IO.FileInfo(ServerPath + QueryString).Length.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //else
        //{
        //    try
        //    {
        //        context.Response.Clear();
        //        context.Response.ContentType = "image/png,application/pdf,image/jpeg";
        //        context.Response.BinaryWrite(System.IO.File.ReadAllBytes(fullfilepath));
        //        context.Response.AddHeader("Content-Length", new System.IO.FileInfo(ServerPath + QueryString).Length.ToString());
        //    }
        //    catch (Exception ex)
        //    {
        //        clsErrorHandler.WriteError(ex);
        //    }
        //}
    }


    public bool IsReusable {
        get {
            return false;
        }
    }

}