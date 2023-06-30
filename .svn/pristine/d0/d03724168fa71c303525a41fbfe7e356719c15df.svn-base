using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using com.ecs.asa.utils;
using com.ecs.aua.pidgen.support;
using ECSAsaApiEx;
using ECSAsaApiEx.com.ecs.asa.utils;
using ECSAsaApiEx.com.ecs.exception;
using In.gov.uidai.authentication.otp._1;
using In.gov.uidai.authentication.uid_auth_response._1;
using System.Xml;
using NLog;

namespace ECSAsaApiDemo
{
    public class OtpAuthenticationTest
    {
        ILogger log = LogManager.GetCurrentClassLogger();
        public bool PerformOtpAuthentication(Common settings, string aadhaarNumber)
        {
           // Console.Write("\n\nOTP : ");
            string otp = settings.OTP ;
            if (String.IsNullOrEmpty(otp) == true)
            {
                Console.WriteLine("\nOTP cannot be empty...!\n");
                return false;
            }
            byte[] uidaiEncryptionCert = File.ReadAllBytes(settings.UidaiEncryptionCertificate);
            byte[] uidaiVerifyCert = null;
            if (settings.UidaiSigningCertificate != null)
            {
                uidaiVerifyCert = File.ReadAllBytes(settings.UidaiSigningCertificate);
            }
            AuthProcessor pro = new AuthProcessor(uidaiEncryptionCert, uidaiVerifyCert);
            pro.Ac = settings.AuaCode;
            pro.Sa = settings.SubAuaCode;
            pro.Uid = aadhaarNumber;
            pro.Tid = AuthProcessor.TidType.None;
            pro.Rc = AuthProcessor.RcType.Y;
            pro.Lk = settings.AuaLicenseKey;
            pro.Txn = settings.TransactionID;
            //pro.Txn = otpres.txn;
            pro.PrepareOtpPIDBlock(otp, settings.UDC);
            //Console.WriteLine("Fet XML ****** : " + str);
            //byte[] auaSignCert = null;
            byte[] auaSignCert = File.ReadAllBytes(settings.AuaSigningCertificate);
            string signedXml = pro.GetSignedXml(auaSignCert,settings.AuaSigningPassword);            
           // File.WriteAllText("OtpAuthRequest121.txt", signedXml);
            string xmlString = signedXml;
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlString);  
            doc.DocumentElement["Signature"].RemoveAll();
            signedXml = doc.OuterXml.Replace("ver=" + '"' + "2.0" + '"', "ver=" + '"' +"2.5"+ '"');
            //signedXml = signedXml.Replace("ac="+'"'+ "0000000000"+ '"',"");
            //signedXml= signedXml.Replace("lk=" + '"' + "MEaMX8fkRa6PqsqK6wGMrEXcXFl_oXHA-YuknI2uf0gKgZ80HaZgG3A" + '"', "");
            //signedXml = signedXml.ToString().Replace("<Signature xmlns=" + '"' + "http://www.w3.org/2000/09/xmldsig#" + '"' + "></Signature>", ""); 
            string responseXml = HttpConnector.Instance.PostData(settings.AsaUrl, signedXml);
            try
            {                             
                XmlDocument docRes = new XmlDocument();
                docRes.LoadXml(responseXml);
                if (docRes.LastChild.Attributes["ret"].Value == "y")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch(AsaServerException ex)
            {
                log.Error(ex);
                // Console.WriteLine(ex.Message);
                return false;
            }catch (InvalidResponseException ex)
            {
                log.Error(ex);
                // Console.WriteLine(ex.Message);
                return false;
            }
            catch (UidaiSignatureVerificateException ex)
            {
                log.Error(ex);
                //Console.WriteLine(ex.Message);
                return false;
            }
            catch(Exception ex)
            {
                log.Error(ex);
                return false;
               // Console.WriteLine("Error deserializing Auth Response\n" + Utils.ToFullString(ex));
            }
        }

        public bool PerformDetailsAuthentication(Common settings, string aadhaarNumber)
        {
            DemoAuthData D = new DemoAuthData();
            D.PersonalIdentity.MatchingValue = 100;
            D.PersonalIdentity.MatchingStrategy = DemoAuthData.PiMatchingStrategy.E;
        
            D.PersonalIdentity.Name = settings.Name;
            byte[] uidaiEncryptionCert = File.ReadAllBytes(settings.UidaiEncryptionCertificate);
            byte[] uidaiVerifyCert = null;
            if (settings.UidaiSigningCertificate != null)
            {
                uidaiVerifyCert = File.ReadAllBytes(settings.UidaiSigningCertificate);
            }
            AuthProcessor pro = new AuthProcessor(uidaiEncryptionCert, uidaiVerifyCert);
            pro.Ac = settings.AuaCode;
            pro.Sa = settings.SubAuaCode;
            pro.Uid = aadhaarNumber;
            pro.Tid = AuthProcessor.TidType.None;
            pro.Rc = AuthProcessor.RcType.Y;
            pro.Lk = settings.AuaLicenseKey;
            pro.Txn = settings.TransactionID;
            pro.PrepareDemographicPIDBlock(D, settings.UDC);

           (pro.getUses()).Pi = AuthProcessor.YesNo.Yes;


            byte[] auaSignCert = File.ReadAllBytes(settings.AuaSigningCertificate);
            string signedXml = pro.GetSignedXml(auaSignCert, settings.AuaSigningPassword);



            string xmlString = signedXml;
            XmlDocument doc = new XmlDocument();

            doc.LoadXml(xmlString);

            doc.DocumentElement["Signature"].RemoveAll();

            signedXml = doc.OuterXml.Replace("pin=" + '"' + "n" + '"', "bt=" + '"' + '"' + " pin =" + '"' + "n" + '"');

            signedXml = signedXml.Replace("ac=" + '"' + "0000000000" + '"', "");

            signedXml = signedXml.Replace("lk=" + '"' + "MEaMX8fkRa6PqsqK6wGMrEXcXFl_oXHA-YuknI2uf0gKgZ80HaZgG3A" + '"', "");

            signedXml = signedXml.ToString().Replace("<Signature xmlns=" + '"' + "http://www.w3.org/2000/09/xmldsig#" + '"' + "></Signature>", "");

            string responseXml = HttpConnector.Instance.PostData(settings.AsaUrl, signedXml);

            AuthRes authres = null;

            try
            {
                authres = pro.Parse(responseXml);

                if (authres.ret == AuthResult.y)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (AsaServerException ex)
            {
                // Console.WriteLine(ex.Message);
                return false;
            }
            catch (InvalidResponseException ex)
            {
                // Console.WriteLine(ex.Message);
                return false;
            }
            catch (UidaiSignatureVerificateException ex)
            {
                //Console.WriteLine(ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                return false;
                // Console.WriteLine("Error deserializing Auth Response\n" + Utils.ToFullString(ex));
            }
        }
    }
}
