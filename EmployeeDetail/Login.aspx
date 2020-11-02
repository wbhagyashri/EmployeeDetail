<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="EmployeeAccess.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<meta charset="utf-8" content="" />
	<meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
	<meta name="description" content="" />
	<meta name="author" content="" />
	<title>Employee Application</title>
	<link href="CSS/appStyle.css" rel="stylesheet" />
	<script src="jquery/jquery.min.js"></script>
	<script type="text/javascript">
		function validate() {
			var filter = /^((\+[1-9]{1,4}[ \-]*)|(\([0-9]{2,3}\)[ \-]*)|([0-9]{2,4})[ \-]*)*?[0-9]{3,4}?[ \-]*[0-9]{3,4}?$/;
			var phoneNumber = $("#mobielNo").val();
			var isValid = false;
			if (filter.test(phoneNumber)) {
				if (phoneNumber.length == 10) {
					isValid = true;
				} else {
					alert('Please put 10  digit mobile number');
					isValid = false;
				}
			}
			else {
				alert('Not a valid number');
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
			<br />
			<asp:TextBox runat="server" ID="mobielNo" autocomplete="off" TextMode="Number" 
				CssClass="form-control" placeholder="Mobile Nunber" TabIndex="1" required="required"				
		        onKeyDown="if(this.value.length==10 && event.keyCode!=8) return false;"></asp:TextBox>
			<br />
			<asp:Button ID="Submit" runat="server" Text="Submit" OnClientClick="return validate();" OnClick="SendOTP" />
		</div>
	</form>
</body>
</html>
