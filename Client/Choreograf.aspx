<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Choreograf.aspx.cs" Inherits="HelpSTAR.Web.Choreograf" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <meta http-equiv="Pragma" content="no-cache" />
    <meta http-equiv="X-UA-Compatible" content="IE=9" />
    <title>AutoDesk</title>
    <style type="text/css">
    html, body {
	    height: 100%;
	    overflow: auto;
    }
    body {
	    padding: 0;
	    margin: 0;
    }
    #silverlightControlHost {
	    height: 100%;
	    text-align:center;
    }
    </style>
    <script type="text/javascript" src="Silverlight.js"></script>
    <script type="text/javascript">
        function onSilverlightError(sender, args) {
            var appSource = "";
            if (sender != null && sender != 0) {
                appSource = sender.getHost().Source;
            }

            var errorType = args.ErrorType;
            var iErrorCode = args.ErrorCode;

            if (errorType == "ImageError" || errorType == "MediaError") {
                return;
            }

            var errMsg = "Unhandled Error in Silverlight Application " + appSource + "\n";

            errMsg += "Code: " + iErrorCode + "    \n";
            errMsg += "Category: " + errorType + "       \n";
            errMsg += "Message: " + args.ErrorMessage + "     \n";

            if (errorType == "ParserError") {
                errMsg += "File: " + args.xamlFile + "     \n";
                errMsg += "Line: " + args.lineNumber + "     \n";
                errMsg += "Position: " + args.charPosition + "     \n";
            }
            else if (errorType == "RuntimeError") {
                if (args.lineNumber != 0) {
                    errMsg += "Line: " + args.lineNumber + "     \n";
                    errMsg += "Position: " + args.charPosition + "     \n";
                }
                errMsg += "MethodName: " + args.methodName + "     \n";
            }

            throw new Error(errMsg);
        }
        var isFirstTime = true;
        function gotFocus(msg) { 
            alert(msg);
        }

        function get_browser() {
            var N = navigator.appName, ua = navigator.userAgent, tem;
            var M = ua.match(/(opera|chrome|safari|firefox|msie)\/?\s*(\.?\d+(\.\d+)*)/i);
            if (M && (tem = ua.match(/version\/([\.\d]+)/i)) != null) M[2] = tem[1];
            M = M ? [M[1], M[2]] : [N, navigator.appVersion, '-?'];
            return M[0];
        }

        function activateMe() {
            window.focus();
        }

        function get_browser_version() {
            var N = navigator.appName, ua = navigator.userAgent, tem;
            var M = ua.match(/(opera|chrome|safari|firefox|msie)\/?\s*(\.?\d+(\.\d+)*)/i);
            if (M && (tem = ua.match(/version\/([\.\d]+)/i)) != null) M[2] = tem[1];
            M = M ? [M[1], M[2]] : [N, navigator.appVersion, '-?'];
            return M[1];
        }

        function closeMe() {
            var browser = get_browser();
            var browser_version = get_browser_version();

            if (browser == "Firefox" || browser == "Chrome") {
                window.open('Choreograf.aspx', '_self');
            } else {
                window.open('', '_self'); 
                window.close();
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server" style="height:100%">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <div id="silverlightControlHost">
        <object data="data:application/x-silverlight-2," type="application/x-silverlight-2" width="100%" height="100%">
          <%
              string xapSource = @"ClientBin/AutoDesk.Client.Shell.xap";
              string xapPath = HttpContext.Current.Server.MapPath(@"") + @"\" + xapSource;
              DateTime xapCreationDate = System.IO.File.GetLastWriteTime(xapPath);
              string param = "<param name=\"source\" value=\"" + xapSource + "?VersionModifiedOn=" + xapCreationDate.ToString() + "\" />";
              Response.Write(param);
          %>
          <param name="enableHtmlAccess" value="true" />
		  <param name="onError" value="onSilverlightError" />
		  <param name="background" value="white" />
		  <param name="minRuntimeVersion" value="5.1.20513.0" />
		  <param name="autoUpgrade" value="true" />
          <param name="enableGPUAcceleration" value="false" />
          <%--<param name="Initparams" value="UserAccount=<%=HttpContext.Current.User.Identity.Name%>, ServiceUri=<%=ConfigurationSettings.AppSettings["ServiceUri"]%>" />--%>
		  <a href="https://go.microsoft.com/fwlink/?LinkID=149156&v=5.1.20513.0" style="text-decoration:none">
 			  <img src="Images/SLMedallion_ENU.png" alt="Get Microsoft Silverlight" style="border-style:none"/>
		  </a>
	    </object><iframe id="_sl_historyFrame" style="visibility:hidden;height:0px;width:0px;border:0px"></iframe></div>
    </form>
</body>
</html>
                        