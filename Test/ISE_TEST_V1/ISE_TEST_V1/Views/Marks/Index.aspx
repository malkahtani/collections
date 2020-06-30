<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ISE_TEST_V1.ViewModels.MarksIndexView>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Index</h2>
         <table>
        <tr>
            <th></th>
            <th>
                Candidate Name
            </th>
            <th>
               Test Name
            </th>
            <th>
                Number of Questions need revision
            </th>
            <th>
                Number of Questions need to be marked
                
            </th>
            
        </tr>
    <% 
       
       for (int i = 0; i < Model.count; i++)
    {
           String c_name = Model.can.ElementAt(i).Name;
           String t_name = Model.test.ElementAt(i).Name;
           
           int need_rev = Model.answers_log.ElementAt(i).need_rev.Count();
           int need_mark = Model.answers_log.ElementAt(i).not_marked.Count();
           int cid = Model.ctr.ElementAt(i).ID; 
        %>
          <tr>
            <td>
               
                <%: Html.ActionLink("Mark this test", "Mark", new { id= cid}) %> 
            </td>
            <td>
                <%: c_name %>
            </td>
            <td>
                <%: t_name %>
            </td>
            <td>
                <%: need_rev %>
            </td>
            <td>
                <%: need_mark %>
            </td>
            
               
        </tr>
    
    

    
           
     <%  }%>
       </table>
      
</asp:Content>