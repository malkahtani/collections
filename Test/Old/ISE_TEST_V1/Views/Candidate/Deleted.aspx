<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Deleted
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Candidate Deleted</h2>
   
<p>The Candidate was successfully deleted.</p>
<p> <%: Html.ActionLink("Click here", "Index") %>
to return to the test list.
</p>
</asp:Content>