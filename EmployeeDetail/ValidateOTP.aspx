<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ValidateOTP.aspx.cs" Inherits="EmployeeAccess.ValidateOTP" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="CSS/appStyle.css" rel="stylesheet" />
	<script src="jquery/jquery.min.js"></script>
	<title>Employee Application</title>
	<script type="text/javascript">
		function validate() {
			var otpText = $("#otpText").val();
			var isValid = true;
			if (otpText.trim()=="") {
				alert('Please enter OTP!');
				isValid = false;
			}
			return isValid;
		}
	</script>
</head>
<body>
    <form id="form1" runat="server">
		<div class="appDiv">
		<div style="color: red; width: 100%; text-align: center;" id="errorMessage" runat="server"></div>
		<div style="color: green; width: 100%; text-align: center;" id="successMessage" runat="server"></div>	
        <br />
        <div  id="verfyDiv" runat="server">
	        <asp:TextBox runat="server" ID="otpText" autocomplete="off" TextMode="Number"
		        CssClass="form-control" placeholder="OTP" TabIndex="1" required="required"
		        onKeyDown="if(this.value.length==6 && event.keyCode!=8) return false;"></asp:TextBox>
	        <br />
	        <asp:Button ID="Submit" runat="server" Text="Submit" OnClientClick="return validate();" OnClick="VerifyOTP" />
        </div>
		</div>
	</form>
</body>
</html>
