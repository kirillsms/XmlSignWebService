﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;
using XmlSignLibrary;

namespace WpfApp
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    public VipNetCrytoProvider CryptoProvider { get; set; }
    //public static String Xml = "<S:Envelope xmlns:S=\"http://schemas.xmlsoap.org/soap/envelope/\"><S:Header></S:Header><S:Body></S:Body></S:Envelope>";
    public static String Xml = "<S:Envelope xmlns:S=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:ds=\"http://www.w3.org/2000/09/xmldsig#\" xmlns:wsse=\"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd\" xmlns:wsu=\"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd\"><S:Header><wsse:Security S:actor=\"http://smev.gosuslugi.ru/actors/smev\"><wsse:BinarySecurityToken EncodingType=\"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-soap-message-security-1.0#Base64Binary\" ValueType=\"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-x509-token-profile-1.0#X509v3\" wsu:Id=\"CertId\">MIIFtzCCBWagAwIBAgIDIk6iMAgGBiqFAwICAzCCAfYxGTAXBgNVBAQMENCc0LXQtNCy0LXQtNC10LIxLDAqBgNVBCoMI9CQ0L3QsNGC0L7Qu9C40Lkg0JHQvtGA0LjRgdC+0LLQuNGHMRkwFwYDVQQHDBDQsy4g0JzQvtGB0LrQstCwMSMwIQYDVQQJDBrRg9C7LiDQmNC70YzQuNC90LrQsCwg0LQuOTGBojCBnwYJKoZIhvcNAQkCDIGR0JTQsNC90L3Ri9C5INGB0LXRgNGC0LjRhNC40LrQsNGCINC+0YLQutGA0YvRgtC+0LPQviDQutC70Y7Rh9CwINC40YHQv9C+0LvRjNC30YPQtdGC0YHRjyDRgdC+INGB0YDQtdC00YHRgtCy0L7QvCDQodCa0JfQmCDQmtGA0LjQv9GC0L4g0J/RgNC+IENTUDELMAkGA1UEBhMCUlUxODA2BgNVBAoML9Ck0LXQtNC10YDQsNC70YzQvdC+0LUg0LrQsNC30L3QsNGH0LXQudGB0YLQstC+MX8wfQYDVQQDDHbQo9C/0L7Qu9C90L7QvNC+0YfQtdC90L3Ri9C5INGD0LTQvtGB0YLQvtCy0LXRgNGP0Y7RidC40Lkg0YbQtdC90YLRgCDQpNC10LTQtdGA0LDQu9GM0L3QvtCz0L4g0LrQsNC30L3QsNGH0LXQudGB0YLQstCwMB4XDTExMDMyNDEyMjUxOVoXDTEyMDMyMzEyMjUxOVowggFhMQswCQYDVQQGEwJSVTEZMBcGA1UECAwQ0LMuINCc0L7RgdC60LLQsDEVMBMGA1UEBwwM0JzQvtGB0LrQstCwMU4wTAYDVQQKDEXQpNCV0JTQldCg0JDQm9Cs0J3QntCVINCa0JDQl9Cd0JDQp9CV0JnQodCi0JLQniAo0KHQldCg0JLQldCgINCh0K3QlCkxJjAkBgNVBCoMHdCu0LvQuNGPINCT0LXQvtGA0LPQuNC10LLQvdCwMRkwFwYDVQQEDBDQmtC+0L/Ri9C70L7QstCwMSgwJgYDVQQMDB/QndCw0YfQsNC70YzQvdC40Log0L7RgtC00LXQu9CwMWMwYQYDVQQDDFrQmtC+0L/Ri9C70L7QstCwINCu0LvQuNGPINCT0LXQvtGA0LPQuNC10LLQvdCwICjQotC10YXQvdC+0LvQvtCz0LjRh9C10YHQutC40Lkg0LrQu9GO0YcgMSkwYzAcBgYqhQMCAhMwEgYHKoUDAgIkAAYHKoUDAgIeAQNDAARAy9OoVph+gGTflOV66d6sHiDwdANt+vNhXwx0KnDK2sbSl041RBV0S578k3fZrdIhkacz1AeDKyQHKxzyWs46zqOCAWowggFmMAwGA1UdEwEB/wQCMAAwGAYDVR0gBBEwDzANBgsqhQMDPZ7XNgECAjBjBgNVHREEXDBaoBIGA1UEDKALEwkzMjQxMzUzNTeGATCgDgYIKoUDA4EDAQGgAhMAoBAGCiqFAwM9ntc2AQegAhMAoBAGCiqFAwM9ntc2AQWgAhMAoA0GByqFAwHgOQGgAhMAMA4GA1UdDwEB/wQEAwIE0DAhBgNVHSUEGjAYBggqhQMCAQYIBQYMKoUDAz2e1zYBBgMCMB8GA1UdIwQYMBaAFAe1ebcBOgyBvSWBc6saQOlYw/NNMGQGA1UdHwRdMFswLqAsoCqGKGh0dHA6Ly9kYy51Yy1zZnNmay5sb2NhbC9jcmwvY3VycmVudC5jcmwwKaAnoCWGI2h0dHA6Ly9jcmwucm9za2F6bmEucnUvY3JsL2xhc3QuY3JsMB0GA1UdDgQWBBQ6BizcjJIKBIGuyz7OCdySYtSE4zAIBgYqhQMCAgMDQQDRsa8Xu7mdTyKxrsVEr9YqGo2iG7Bcj1v4ttgcY+ylS3o9htFVhqu/TVXG90tTiFbR5f4j69/5y9YCkXaiGHdV</wsse:BinarySecurityToken><ds:Signature><ds:SignedInfo><ds:CanonicalizationMethod Algorithm=\"http://www.w3.org/2001/10/xml-exc-c14n#\"></ds:CanonicalizationMethod><ds:SignatureMethod Algorithm=\"http://www.w3.org/2001/04/xmldsig-more#gostr34102001-gostr3411\"></ds:SignatureMethod><ds:Reference URI=\"#body\"><ds:Transforms><ds:Transform Algorithm=\"http://www.w3.org/2001/10/xml-exc-c14n#\"></ds:Transform></ds:Transforms><ds:DigestMethod Algorithm=\"http://www.w3.org/2001/04/xmldsig-more#gostr3411\"></ds:DigestMethod><ds:DigestValue>j/09e90aqAaJd3iNVIXAx1ZveytZFCI+VVPEFwoC4j4=</ds:DigestValue></ds:Reference></ds:SignedInfo><ds:SignatureValue>jyAnuWfHRiYtCykHJmFIqD1iId7dkqpiDPyGvrmRkuA0DqmGJOQeiZXs3IigRqYSwD4CoxQQh8u2Dg7AFQcN8A==</ds:SignatureValue><ds:KeyInfo><wsse:SecurityTokenReference><wsse:Reference URI=\"#CertId\" ValueType=\"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-x509-token-profile-1.0#X509v3\"></wsse:Reference></wsse:SecurityTokenReference></ds:KeyInfo></ds:Signature></wsse:Security></S:Header><S:Body wsu:Id=\"body\"><ns9:UnifoTransferMsg xmlns:ns10=\"http://roskazna.ru/xsd/ExportQuittanceResponse\" xmlns:ns11=\"http://roskazna.ru/xsd/ExportIncomesResponse\" xmlns:ns12=\"http://roskazna.ru/xsd/ExportPaymentsResponse\" xmlns:ns13=\"http://roskazna.ru/xsd/PGU_ChargesResponse\" xmlns:ns14=\"http://roskazna.ru/xsd/PaymentInfo\" xmlns:ns15=\"http://roskazna.ru/xsd/Charge\" xmlns:ns2=\"http://www.w3.org/2000/09/xmldsig#\" xmlns:ns3=\"http://smev.gosuslugi.ru/rev110801\" xmlns:ns4=\"http://roskazna.ru/xsd/PGU_ImportRequest\" xmlns:ns5=\"http://rosrazna.ru/xsd/SmevUnifoService\" xmlns:ns6=\"http://roskazna.ru/xsd/Ticket\" xmlns:ns7=\"http://roskazna.ru/xsd/PGU_DataRequest\" xmlns:ns8=\"http://www.w3.org/2004/08/xop/include\" xmlns:ns9=\"http://roskazna.ru/SmevUnifoService/\"><ns3:Message><ns3:Sender><ns3:Code>0000000001</ns3:Code><ns3:Name>External Organization</ns3:Name></ns3:Sender><ns3:Recipient><ns3:Code>0000000000</ns3:Code><ns3:Name>UNIFO</ns3:Name></ns3:Recipient><ns3:Originator><ns3:Code>0000000001фыва</ns3:Code><ns3:Name>External Organization</ns3:Name></ns3:Originator><ns3:TypeCode>Request</ns3:TypeCode><ns3:Date>2011-09-08T11:26:40.137+04:00</ns3:Date></ns3:Message><ns3:MessageData><ns3:AppData><ns5:exportData><ns7:DataRequest kind=\"CHARGESTATUS\"><PostBlock><ID>055aa777-b988-4503-8ad9-e4eed14e7a06</ID><TimeStamp>2011-09-08T11:26:41.247+04:00</TimeStamp><SenderIdentifier>044525225</SenderIdentifier></PostBlock><SupplierBillIDs><SupplierBillID>18800000000000122936</SupplierBillID></SupplierBillIDs></ns7:DataRequest></ns5:exportData></ns3:AppData></ns3:MessageData></ns9:UnifoTransferMsg></S:Body></S:Envelope>";

    public MainWindow()
    {
      InitializeComponent();
      CryptoProvider = new VipNetCrytoProvider();
      textBox1.Text = XDocument.Parse(Xml).ToString();
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
      try
      {
        var document = new SignedXmlDocument(textBox1.Text);
        CryptoProvider.SignDocument(document);
        textBox2.Text = document.Document.ToString();
      }
      catch (Exception)
      {       
      }
    }
  }
}
