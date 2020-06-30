<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ISE_TEST_V1.Models.RM>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	RM
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Add User to Role</h2>
  
           <% 

               if (Model.Users != null)
               {%>
                      <p>< 
               <%for(int x = 1; x <= Model.pages ; x++){
               if (x == Model.currentpage){
              %>
                  <%: x.ToString() %>-
                                
              
                   

                           
              <% }
               else{%>
                   <%: Html.ActionLink(Convert.ToString(x), "RM", new { id = x })%> - 
               <%}
               }    
                   %>
                   ></p>
                   <table><tr><td>Username</td><td>Add to Role</td>
    </tr>
                   <%
                   int uc = Model.Users.Count();
                   for (int i = 0; i < uc; i++)
                   {
              %>
             <tr>  <td><%: Model.Users.ElementAt(i).UserName%> <%= Html.HiddenFor(m => m.Users.ElementAt(i).UserName)%></td> <td> <%: Html.ActionLink("Add To Role", "ADDRole", new { username = Model.Users.ElementAt(i).UserName })%> </td></tr>
                
      <% }
               }
           %>  
           </table>  
	
  
</asp:Content>
