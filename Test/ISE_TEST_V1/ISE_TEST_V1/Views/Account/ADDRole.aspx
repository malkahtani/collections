<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ISE_TEST_V1.Models.ur>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	ADDRole
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Add user to Role</h2>
    
   <% using (Html.BeginForm("Added", "Account",FormMethod.Post))
        {
            %>
           
        <%: Html.ValidationSummary(true)%>
        <%: Html.HiddenFor(m => m.username) %>
       
        <%=Html.DropDownListFor(m => m.rs.Keys, new SelectList(Model.rs, "Key", "Value"))%>
       
    <p>
   <input type="submit" value="Add User to Role" />
            </p>
     <%
         
          Html.EndForm();
        }%> 
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="headerPlaceHolder" runat="server">
</asp:Content>
