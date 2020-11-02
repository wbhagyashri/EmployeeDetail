<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserInfo.aspx.cs" Inherits="EmployeeAccess.UserInfo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<link href="CSS/appStyle.css" rel="stylesheet" />
	<script src="jquery/jquery.min.js"></script>
	<title>Employee Application</title>
	<script type="text/javascript">
		function validate() {
			var alertText = "";
			var isValid = true;
			if ($("#empFirstName").val().trim() == "") {
				alertText += "Please enter First Name \n";
				isValid = false;
			}
			if ($("#empLastName").val().trim() == "") {
				alertText += "Please enter Last Name\n";
				isValid = false;
			}
			if ($("#empEmail").val().trim() == "") {
				alertText += "Please enter Email \n";
				isValid = false;
			}

			var regex = /^([a-zA-Z0-9_\.\-\+])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
			if (!regex.test($("#empEmail").val().trim())) {
				alertText += "Please enter valid Email \n";
				isValid = false;
			}
			if (alertText != "") {
				alert(alertText);
			}
			return isValid;
		}
	</script>
</head>
<body>
	<form id="form1" runat="server">
		<div>
			<div class="appDiv">
				<div style="color: green; width: 100%; text-align: center;" id="successMessage" runat="server"></div>
				<div style="color: red; width: 100%; text-align: center;" id="errorMessage" runat="server"></div>
				<br />
				<div id="empDet" runat="server">
					Employee First Name :
				<asp:TextBox runat="server" ID="empFirstName" autocomplete="off" CssClass="form-control" placeholder="First Name" TabIndex="1" required="required"></asp:TextBox>
					<br />
					Employee Last Name :
				<asp:TextBox runat="server" ID="empLastName" autocomplete="off" CssClass="form-control" placeholder="Last Name" TabIndex="1" required="required"></asp:TextBox>
					<br />
					Employee Email :
				<asp:TextBox runat="server" ID="empEmail" autocomplete="off" CssClass="form-control" placeholder="Email" TabIndex="1" required="required"></asp:TextBox>
					<br />
					<asp:Button ID="Submit" runat="server" Text="Submit" OnClientClick="return validate();" OnClick="AddUserInfo" />
				</div>
			</div>
		</div>
	</form>
</body>
</html>
