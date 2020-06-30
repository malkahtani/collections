<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ISE_TEST_V1.Models.PagedList<ISE_TEST_V1.Models.C_T_R>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Candidate and Tests
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<table>
        <tr>
            <th></th>
            <th>
                ID
            </th>
            <th>
                Candidate Name
            </th>
            
            <th>
                Test Name
            </th>
            <th>
                Date to set the test
            </th>
            <th>
                Date the Test has been Isued
            </th>
        </tr>

    <% ISE_TEST_V1.Models.C_T_R_Sup ct = new ISE_TEST_V1.Models.C_T_R_Sup();
        foreach (var item in Model) {
            ISE_TEST_V1.Models.Test t = ct.get_test(item.T_ID);
            ISE_TEST_V1.Models.Candidate c = ct.get_can(item.C_ID);
           %>
    
        <tr>
            <td>
                
                <%: Html.ActionLink("Delete", "Delete", new { id=item.ID })%>  
            </td>
            <td>
                <%: item.ID %>
            </td>
            <td>
                <%: c.Name %>
            </td>
           
            <td>
                <%: t.Name %>
            </td>
            <td>
                <%: String.Format("{0:g}", item.Date_to_Set)%>
            </td>
            <td>
                <%: String.Format("{0:g}", item.Date_Isu)%>
            </td>
        </tr>
    
    <% } %>

    </table>

    

 <% ISE_TEST_V1.Models.HtmlHelpers.ShowPagerControl(this, Model, "C_T_R", "Page"); %>
</asp:Content>