<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>


<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Index</h2>
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

        function IsNumeric(sText)
{
   var ValidChars = "0123456789";
   var IsNumber=true;
   var Char;

 
   for (i = 0; i < sText.length && IsNumber == true; i++) 
      { 
      Char = sText.charAt(i); 
      if (ValidChars.indexOf(Char) == -1) 
         {
         IsNumber = false;
         }
      }
   return IsNumber;
   
   }





//-->
</script>
    <form action="Result/Search" name="searchForm" method="post" id="searchForm" onsubmit="return validate_form ( );">

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
</asp:Content>


