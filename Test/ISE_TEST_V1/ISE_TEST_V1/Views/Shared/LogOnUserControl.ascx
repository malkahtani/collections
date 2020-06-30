<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%
    if (Request.IsAuthenticated) {
%>
        Welcome <b><%: Page.User.Identity.Name %></b>!
        [ <%: Html.ActionLink("Log Off", "LogOff", "Account")%> |
              <%: Html.ActionLink("Change Password", "ChangePassword", "Account")
                  %> |
                  <% ISE_TEST_V1.Models.Roles r = new ISE_TEST_V1.Models.Roles();
                     if (r.get_role(Page.User.Identity.Name) != "")
                     {  %>
         <%: Html.ActionLink("Add User", "AddUser", "Account")%> |
         <% if (r.get_role(Page.User.Identity.Name)=="Admin")
                     {%><%: Html.ActionLink("Add Role Manager", "RM/1", "Account")%> |
                     <%: Html.ActionLink("Add Role", "New_E_Role", "Account")%>]
<%
}}
    }
    else {
%> 
        [ <%: Html.ActionLink("Log On", "LogOn", "Account") %> ]
<%
    }
%>
