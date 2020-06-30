<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Create New Role</h2>
   <% using (Html.BeginForm()) { %>
    <%: Html.DisplayText("Role Name")%>
      <%:  Html.TextBox("Role") %>
      
                <p>
                    <input type="submit" value="Add Role" />
                </p>
 <% } %>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="headerPlaceHolder" runat="server">
</asp:Content>
