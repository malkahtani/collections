<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ISE_TEST_V1.Models.addedr>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Added
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

   
    
    <h1><%: Model.username %></h1> <%: ViewData["com"] %> <h1><%: Model.role %></h1>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="headerPlaceHolder" runat="server">
</asp:Content>
