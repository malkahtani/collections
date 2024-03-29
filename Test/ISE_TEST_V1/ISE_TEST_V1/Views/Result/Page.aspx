﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ISE_TEST_V1.Models.PagedList<ISE_TEST_V1.Models.Candidate>>" %>




<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Candidates' Result
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<script type="text/javascript">
<!--

    function validate_form() {
        valid = true;

        if (document.searchForm.by.value == 0 || document.searchForm.by.value == 3) {
            if (IsNumeric(document.searchForm.searchTerm.value) == false) {
                alert("Please Enter Numbers Only");
                valid = false;
            }
        }

        return valid;
    }

    function IsNumeric(sText) {
        var ValidChars = "0123456789";
        var IsNumber = true;
        var Char;


        for (i = 0; i < sText.length && IsNumber == true; i++) {
            Char = sText.charAt(i);
            if (ValidChars.indexOf(Char) == -1) {
                IsNumber = false;
            }
        }
        return IsNumber;

    }





//-->
</script>
    <form action="../Search" name="searchForm" method="post" id="searchForm" onsubmit="return validate_form ( );">

      <table><tr><td><input type="text" name="searchTerm" id="searchTerm" value="" size="10" maxlength ="30" /></td>
      <td>
      <select id="by" name="by">
      <option value = "0">Search by ID</option>
      <option value = "1">Search by Username</option>
      <option value = "2">Search by Name</option>
      <option value = "3">Search by Mobile</option>
      <option value = "4">Search by email</option>
      </select>
      </td>
      </tr>
      </table>
      <input type ="submit" value="Search" />
    </form>
     <table>
        <tr>
            <th></th>
            <th>
                ID
            </th>
            <th>
                Username
            </th>
            
            <th>
                Name
            </th>
            <th>
                Mobile
            </th>
            <th>
                Email
            </th>
        </tr>

    <% foreach (var item in Model) { %>
    
        <tr>
            <td>
                <%: Html.ActionLink("Check The Candidate Result", "C_Details", new { id=item.ID }) %> 
            </td>
            <td>
                <%: item.ID %>
            </td>
            <td>
                <%: item.Uname %>
            </td>
           
            <td>
                <%: item.Name %>
            </td>
            <td>
                <%: item.Mobile %>
            </td>
            <td>
                <%: item.Email %>
            </td>
        </tr>
    
    <% } %>

    </table>
    <% ISE_TEST_V1.Models.HtmlHelpers.ShowPagerControl(this, Model, "Candidate", "Page"); %>
</asp:Content>