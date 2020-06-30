<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Deleted
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Test Deleted</h2>
   
<p>Your test was successfully deleted.</p>
<p> <%: Html.ActionLink("Click here", "Page", new { id = 1 })%>
to return to the test list.
</p>
</asp:Content>
